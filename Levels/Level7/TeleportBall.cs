using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBall : AINeutral
{
    protected override bool AimDetected(GameObject aim)
    {
        throw new System.NotImplementedException();
    }

    protected override bool AimDetected()
    {
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        currentSpawnPointAim = GetRandomSpawnPoint();
        navmeshAgent.speed = speed;
        timeToWait = timeToWaitInit;

    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.isPaused || GameManager.gameOver) { return; }


        if (canChangeSpawnPoint){

            canChangeSpawnPoint = false;

            currentSpawnPointAim = GetRandomSpawnPoint();

            MoveToRandomSpawnPoint(currentSpawnPointAim);

        }

        if (Vector3.Distance(navmeshAgent.transform.position, currentSpawnPointAim) <= navmeshAgent.stoppingDistance)
        {

            timeToWait -= Time.deltaTime;

            if (timeToWait <= 0){

                currentSpawnPointAim = GetRandomSpawnPoint();
                canChangeSpawnPoint = true;
                timeToWait = timeToWaitInit;

            }
        }

    }

    private void OnCollisionEnter(Collision collision){

        if (collision.gameObject.GetComponent<BasicHealth>()) {

            Teleport(true, collision.gameObject);

        } else if (!collision.gameObject.GetComponent<Rigidbody>().isKinematic &&
            collision.gameObject.GetComponent<Rigidbody>().mass < 500) {

            Teleport(false, collision.gameObject);

        }

    }

    void Teleport(bool isAlive,GameObject go) {

        Vector3 pos;

        if (isAlive){

            pos = GetRandomSpawnPoint();

        }
        else {

            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPointEnemy");

            pos = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

        }

        go.transform.position = pos;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MychalkinFree : AINeutral
{
    IDemagable health;

    protected override bool AimDetected(GameObject aim)
    {
        throw new System.NotImplementedException();
    }

    protected override bool AimDetected()
    {
        throw new System.NotImplementedException();
    }

    private void Start(){
        health = GetComponent<IDemagable>();
    }

    void OnEnable(){

        navmeshAgent.speed = speed;

    }

    // Update is called once per frame
    void Update(){

        if (GameManager.isPaused) {

            navmeshAgent.speed = 0;return; }

        if (health.GetCurrentHealth() <= 0)
        {
            navmeshAgent.speed = 0; 
            return;
        }
        else {
            navmeshAgent.speed = speed;
        }
     
        if (canChangeSpawnPoint){

            canChangeSpawnPoint = false;

            currentSpawnPointAim = GetRandomSpawnPoint();

            MoveToRandomSpawnPoint(currentSpawnPointAim);

        }

        if (Vector3.Distance(navmeshAgent.transform.position, currentSpawnPointAim) <= stopDistance)
        {

            timeToWait -= Time.deltaTime;

            if (timeToWait <= 0){

                currentSpawnPointAim = GetRandomSpawnPoint();
                canChangeSpawnPoint = true;
                timeToWait = timeToWaitInit;

            }
        }

    }
}

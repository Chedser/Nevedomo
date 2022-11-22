using UnityEngine;

public class BomberShere : AIMoveableMissiled{

    protected override bool AimDetected(GameObject aim)
    {
        throw new System.NotImplementedException();
    }

    protected override bool AimDetected()
    {
        throw new System.NotImplementedException();
    }

  new  Vector3 GetRandomSpawnPoint()
    {

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPointEnemy");

        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

    }

    new void ChangeSpawnPoint()
    {

        currentSpawnPointAim = GetRandomSpawnPoint();

    }

    new void MoveToRandomSpawnPoint(Vector3 dir)
    {

        navmeshAgent.destination = dir;

    }

    // Start is called before the first frame update
    void Awake(){
        currentSpawnPointAim = GetRandomSpawnPoint();
        navmeshAgent.speed = speed;
        timeToWait = timeToWaitInit;

    }

    new void Shoot()
    {

        GameObject missileGo = Instantiate(missile, shootPlace.position, shootPlace.rotation);
        Missile missileScript = missileGo.GetComponent<Missile>();
        missileScript.owner = spawnInfo.heroType;
        missileScript.ownerGo = spawnInfo.gameObject;

    }

    // Update is called once per frame
    void Update(){

        if (GameManager.isPaused || GameManager.gameOver) { return; }


        if (canChangeSpawnPoint){

            canChangeSpawnPoint = false;

            currentSpawnPointAim = GetRandomSpawnPoint();

                MoveToRandomSpawnPoint(currentSpawnPointAim);

            }

            if (Vector3.Distance(navmeshAgent.transform.position, currentSpawnPointAim) <= navmeshAgent.stoppingDistance) {
               
                timeToWait -= Time.deltaTime;

                if (timeToWait <= 0){

                    currentSpawnPointAim = GetRandomSpawnPoint();
                    canChangeSpawnPoint = true;
                    timeToWait = timeToWaitInit;

                }
            }

        if (!ChupisHealth.isDead) { return; }

        shootTime -= Time.deltaTime;

        if (shootTime <= 0){

            Shoot();

            shootTime = shootTimeInit;
        }

    }

}


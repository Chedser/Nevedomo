using UnityEngine;
using UnityEngine.AI;

public class BarrelSphere : MonoBehaviour
{

    GameObject[] spawnPoints;
    GameObject currentSpawnPointAim;
    [SerializeField]
    NavMeshAgent navmeshAgent;
    float timeToWait;
    public float timeToWaitInit;
    public float speed = 10.0f;
    bool canChangeSpawnPoint = true;


    // Start is called before the first frame update
    void Awake(){

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPointEnemy");
        currentSpawnPointAim = GetRandomSpawnPoint();
        navmeshAgent.speed = speed;
        timeToWait = timeToWaitInit;

    }

    // Update is called once per frame
    void Update(){

        if (GameManager.isPaused) { return; }


        if (canChangeSpawnPoint){

            canChangeSpawnPoint = false;

            currentSpawnPointAim = GetRandomSpawnPoint();

            MoveToRandomSpawnPoint(currentSpawnPointAim.transform.position);

        }

        if (Vector3.Distance(navmeshAgent.transform.position, currentSpawnPointAim.transform.position) <= navmeshAgent.stoppingDistance)
        {

            timeToWait -= Time.deltaTime;

            if (timeToWait <= 0){

                currentSpawnPointAim = GetRandomSpawnPoint();
                canChangeSpawnPoint = true;
                timeToWait = timeToWaitInit;

            }
        }

   
    }

   new GameObject GetRandomSpawnPoint(){

        return spawnPoints[Random.Range(0, spawnPoints.Length)];

    }

    new void MoveToRandomSpawnPoint(Vector3 dir){

        navmeshAgent.destination = dir;

    }

}

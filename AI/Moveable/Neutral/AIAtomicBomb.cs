using UnityEngine;
using UnityEngine.AI;

public class AIAtomicBomb : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent navmeshAgent;
    protected Vector3 currentSpawnPointAim;
    public float stopDistance;

    public float timeToWaitInit;
    protected float timeToWait;
    protected bool canChangeSpawnPoint = true;

    public float speed;

    // Start is called before the first frame update
    void Awake(){
        
    }

    // Update is called once per frame
    void Update(){

        if (GameManager.isPaused || GameManager.gameOver)
        {
            navmeshAgent.speed = 0;
            return;
        }
        else {
            navmeshAgent.speed = speed;
        }

        if (canChangeSpawnPoint)
        {

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

    Vector3 GetRandomSpawnPoint(){

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

    }

    protected void MoveToRandomSpawnPoint(Vector3 dir)
    {

        navmeshAgent.destination = dir;

    }

}

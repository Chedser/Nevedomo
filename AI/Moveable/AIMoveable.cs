using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public abstract class AIMoveable : AI{

    [SerializeField] protected NavMeshAgent navmeshAgent;
    protected Vector3 currentSpawnPointAim;
    public float stopDistance;
    public float findDistance;
    public float rotationSpeed;
    
    public float timeToWaitInit;
    protected float timeToWait;
    protected bool canChangeSpawnPoint = true;
    [Range(2, 4)]
    public int kickChance;
    public int demage;

    public bool awakeFromMissile;

    public float speed;

    private void Awake(){

        state = State.Searching;
        currentSpawnPointAim = GetRandomSpawnPoint();
        navmeshAgent.speed = speed;
        timeToWait = timeToWaitInit;
        awakeFromMissile = false;
    }

    protected Vector3 GetRandomSpawnPoint() {

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint"); 

        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

    }

    protected void MoveToAim(GameObject aim){

        navmeshAgent.destination = aim.transform.position;

    }

    protected void MoveToRandomSpawnPoint(Vector3 dir){

        navmeshAgent.destination = dir;

    }

    protected void RotateToAim(GameObject aim) {

        Vector3 lookVector = aim.transform.position - transform.position;
        lookVector.y = 0;

        if (lookVector == Vector3.zero) { return; }

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(lookVector, Vector3.up),
            rotationSpeed * Time.deltaTime);

    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, searchDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findDistance);
    }

    public void ChangeSpawnPoint(){

        currentSpawnPointAim = GetRandomSpawnPoint();

    }

}

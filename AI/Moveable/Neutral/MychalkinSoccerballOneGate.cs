using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MychalkinSoccerballOneGate : AINeutral
{
    IDemagable health;

    GameObject[] soccerballs;

    protected override bool AimDetected(GameObject aim)
    {
        throw new System.NotImplementedException();
    }

    protected override bool AimDetected()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Awake()
    {

        aim = GetRandomSoccerball();

        health = GetComponent<IDemagable>();

    }

    // Update is called once per frame
    void Update()
    {

        if (health.GetCurrentHealth() <= 0)
        {

            navmeshAgent.speed = 0;

        }
        else
        {

            navmeshAgent.speed = speed;

        }

        if (aim == null) {

            aim = GetRandomSoccerball();

        }

        navmeshAgent.destination = aim.transform.position;

    }

    GameObject GetRandomSoccerball() {
       GameObject[] soccerballs = GameObject.FindGameObjectsWithTag("SoccerBall");

        if (soccerballs.Length == 0) {

            soccerballs = GameObject.FindGameObjectsWithTag("SpawnPoint");

        }

        return soccerballs[Random.Range(0, soccerballs.Length)];

    }

}

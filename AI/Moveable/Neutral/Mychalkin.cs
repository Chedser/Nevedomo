using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mychalkin : AINeutral
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

    // Start is called before the first frame update
    void Awake(){

        health = GetComponent<IDemagable>();
        
    }

    // Update is called once per frame
    void Update(){

        if (health.GetCurrentHealth() <= 0){

            navmeshAgent.speed = 0;

        }
        else {

            navmeshAgent.speed = speed;

        }

        navmeshAgent.destination = aim.transform.position;

    }
}

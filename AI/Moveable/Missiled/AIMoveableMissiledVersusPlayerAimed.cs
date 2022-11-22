using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveableMissiledVersusPlayerAimed : AIMoveableMissiled
{
    protected override bool AimDetected(GameObject aim)
    {
        throw new System.NotImplementedException();
    }

    protected override bool AimDetected()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start(){
        aim = GameObject.Find("Player");
        state = State.MoveToAim;
        health = GetComponent<IDemagable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aim.GetComponent<IDemagable>().GetCurrentHealth() <= 0 || GameManager.gameOver || GameManager.isPaused)
        {
            state = State.Searching;
            navmeshAgent.speed = 0;
            return;
        }

        if (health.GetCurrentHealth() <= 0)
        {
            state = State.Searching;
            navmeshAgent.speed = 0;
            return;

        }
        else
        {
            state = State.MoveToAim;
            navmeshAgent.speed = speed;

        }


        if (state == State.MoveToAim || state == State.Atacking) {
         
           RotateToAim(aim);

            if (CanSee(aim) &&
                (Vector3.Distance(transform.position, aim.transform.position) <= shootDistance))
            {

               // transform.LookAt(aim.transform.position);

                state = State.Atacking;

            }
            else
            {
                state = State.MoveToAim;

            }


        }

        if (state == State.Atacking) {

            shootTime -= Time.deltaTime;

            if (shootTime <= 0)
            {

                Shoot();

                shootTime = shootTimeInit;
            }

        }

        

        navmeshAgent.destination = aim.transform.position;
    }
}

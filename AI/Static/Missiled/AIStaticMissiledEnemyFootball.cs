using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStaticMissiledEnemyFootball : AIStaticMissiled
{

    [SerializeField] GameObject ball;

   public bool moveToRight = true;
   public bool moveToLeft = false;

    public float moveSpeed = 10f;

    protected override bool AimDetected()
    {

        bool aimDetected = false;

        GameObject[] opponents = GameObject.FindGameObjectsWithTag("FootballHelper");

        if (opponents.Length == 0) {

            return false;

        }

        float currentDistance;

        foreach (GameObject opponent in opponents)
        {

            currentDistance = Vector3.Distance(transform.position, opponent.transform.position);

            if (currentDistance <= searchDistance && opponent.GetComponent<IDemagable>().GetCurrentHealth() > 0)
            {
                aimDetected = true;
                aim = opponent;
            }

        }

        if (aimDetected == false) {

            GameObject player = GameObject.Find("Player");

            if (Vector3.Distance(transform.position, player.transform.position) <= searchDistance) {

                aimDetected = true;
                aim = player;

            }

            if (aimDetected == false && Vector3.Distance(transform.position, ball.transform.position) <= searchDistance) {

                aimDetected = true;
                aim = ball;

            }

        }

        return aimDetected;

    }
    protected override bool AimDetected(GameObject aim){
        throw new System.NotImplementedException();
    }

    private void Update(){

     //   DrawRays();

        if (health.GetCurrentHealth() <= 0 || Level2.isGoaled || GameManager.isPaused || GameManager.gameOver)
        {

            return;

        }
    
        //    Rotate();

        if (moveToRight && !moveToLeft) {

            transform.localPosition = new Vector3(transform.localPosition.x + Time.deltaTime * moveSpeed, transform.localPosition.y, transform.localPosition.z);

        } else if (!moveToRight && moveToLeft) {

            transform.localPosition = new Vector3(transform.localPosition.x - Time.deltaTime * moveSpeed, transform.localPosition.y, transform.localPosition.z);

        }
       
                
        if (AimDetected()) {

            state = State.Atacking;

        }

        if (state == State.Atacking) {

            if (Vector3.Distance(transform.position, aim.transform.position) > searchDistance){

                state = State.Searching;
                aim = null;
                return;

            }

          //  currentRotateAngle = rotationPlace.transform.localEulerAngles.y;

            shootTime -= Time.deltaTime;

                if (shootTime <= 0)
                {

                    Shoot();

                    shootTime = shootTimeInit;

                    if (aim.GetComponent<AI>() && (aim.GetComponent<IDemagable>().GetCurrentHealth() <= 0))
                    {
                        state = State.Searching;
                    }

                }

            }
                   
          
        }

    

    void DrawRays()
    {

        for (int i = 0; i < rays.Length; i++)
        {

            Debug.DrawLine(rays[i].position, rays[i].position + rays[i].forward * searchDistance, Color.red);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("TriggerRight"))
        {

            moveToRight = false;
            moveToLeft = true;

        }
        else if(other.gameObject.name.Equals("TriggerLeft")) {

            moveToRight = true;
            moveToLeft = false;

        }
    }

}

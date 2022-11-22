using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveableRaycastVersusCommand : AIMoveableRaycast
{
    string _commandToAttackTag = "Enemy";
    [SerializeField]
    float timeToChangeAimInit = 3.0f;
    float timeToChangeAim;

    private void OnEnable()
    {

        navmeshAgent.speed = speed;
        timeToChangeAim = timeToChangeAimInit;
        aim = GetNearestOpponent(this.gameObject, _commandToAttackTag);

    }

    private void Start()
    {

        health = GetComponent<IDemagable>();

        if (spawnInfo.heroType == HeroType.Enemy)
        {

            _commandToAttackTag = "Helper";

        }

        aim = GetNearestOpponent(this.gameObject, _commandToAttackTag);

    }

    GameObject GetNearestOpponent(GameObject go, string tag)
    {

        GameObject[] opponents = GameObject.FindGameObjectsWithTag(tag);

        if (opponents.Length == 0) { return null; }

        GameObject currentOpponent = opponents[0];
        float maxDistance = Vector3.Distance(go.transform.position, opponents[0].transform.position);
        float currentDistance;

        foreach (GameObject opponent in opponents)
        {

            currentDistance = Vector3.Distance(go.transform.position, opponent.transform.position);

            if (currentDistance <= maxDistance && opponent.GetComponent<IDemagable>().GetCurrentHealth() > 0)
            {

                maxDistance = currentDistance;
                currentOpponent = opponent;
            }

        }

        return currentOpponent;

    }

    protected override bool AimDetected(GameObject aim)
    {
        throw new System.NotImplementedException();
    }

    protected override bool AimDetected(){

        RaycastHit hit;

        bool aimDetected = false;

        for (int i = 0; i < rays.Length; i++){

            if (Physics.Raycast(rays[i].position, rays[i].position + rays[i].forward * searchDistance, out hit, searchDistance))
            {

                if (hit.collider.gameObject.GetComponent<SpawnInfo>() &&
                    hit.collider.gameObject.GetComponent<SpawnInfo>().heroType == HeroType.Helper)
                {

                    aim = hit.collider.gameObject;
                    Debug.DrawLine(rays[i].position, rays[i].position + rays[i].forward * searchDistance, Color.red);
                    aimDetected = true;

                }

            }

        }

        return aimDetected;

    }



    // Update is called once per frame
    void FixedUpdate()
    {

       // DrawViewState();

        if (GameManager.isPaused || GameManager.gameOver) { return; }

        if (health.GetCurrentHealth() <= 0 || aim == null){

            state = State.Searching;
            navmeshAgent.speed = 0;
            aim = GetNearestOpponent(this.gameObject, _commandToAttackTag);
            return;

        }
        else{

            navmeshAgent.speed = speed;

        }

        if (aim.GetComponent<IDemagable>().GetCurrentHealth() <= 0) {

            aim = GetNearestOpponent(this.gameObject, _commandToAttackTag);

        }

        if (AimDetected()){

            state = State.MoveToAim;

        }
        else {
            aim = GetNearestOpponent(this.gameObject, _commandToAttackTag);
            state = State.MoveToAim;
        }



        if (aim != null) {

            RotateToAim(aim);

        }
    

        if (aim != null && CanSee(aim) &&
               (Vector3.Distance(transform.position, aim.transform.position) <= shootDistance))
        {

             transform.LookAt(aim.transform.position);

            state = State.Atacking;

        }
        else
        {
            state = State.MoveToAim;

        }


        if (state == State.Atacking)
        {

            shootTime -= Time.deltaTime;

            if (shootTime <= 0){

                Shoot(aim);

                if (aim.GetComponent<IDemagable>().GetCurrentHealth() <= 0){
        
                    aim = GetNearestOpponent(this.gameObject, _commandToAttackTag);

                }

                shootTime = shootTimeInit;
            }

        }


        if (aim != null) {

            navmeshAgent.destination = aim.transform.position;

        }

     

    }
}

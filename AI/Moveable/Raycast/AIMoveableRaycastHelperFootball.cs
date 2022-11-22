using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateFootball {MoveToBall, MoveToOpponent, MoveToStaypoint, MoveToGate }

public class AIMoveableRaycastHelperFootball : AIMoveableRaycast{

    [SerializeField] GameObject opponentGate;
    [SerializeField] GameObject commandGate;
    public StateFootball stateFootball = StateFootball.MoveToBall;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject auxAim;
    [SerializeField] ShootSpawnPoint gun;

    protected override bool AimDetected(GameObject aim)
    {
        throw new System.NotImplementedException();
    }

    protected override bool AimDetected(){
 
        RaycastHit hit;

        bool aimDetected = false;

        for (int i = 0; i < rays.Length; i++)
        {

            if (Physics.Raycast(rays[i].position, rays[i].position + rays[i].forward * searchDistance, out hit, searchDistance))
            {

                if (hit.collider.gameObject.GetComponent<SpawnInfo>() &&
                    hit.collider.gameObject.GetComponent<SpawnInfo>().heroType == HeroType.Enemy &&
                    hit.collider.gameObject.GetComponent<IDemagable>().GetCurrentHealth() > 0)
                {

                    auxAim = hit.collider.gameObject;
                    Debug.DrawLine(rays[i].position, rays[i].position + rays[i].forward * searchDistance, Color.red);
                    aimDetected = true;

                }

            }


        }

        return aimDetected;

    }

    // Start is called before the first frame update
    void Awake(){
        health = GetComponent<IDemagable>();
        navmeshAgent.speed = speed;
    }

    // Update is called once per frame
    void Update(){

        if (GameManager.isPaused) { navmeshAgent.speed = 0; return; }

        if (health.GetCurrentHealth() <= 0 || Level2.isGoaled || GameManager.gameOver){

            navmeshAgent.speed = 0;
            DropBall();

            return;
        }
        else {

            navmeshAgent.speed = speed;

        }

        if (ball.GetComponent<FootballCollision>().ballOwner == HeroType.Enemy) {

            if (IsNearest(this.gameObject, ball, "FootballHelper")){

                aim = ball.GetComponent<FootballCollision>().ballOwnerGo;

            }
            else {

                aim = GetNearestOpponent(this.gameObject, "FootballEnemy");

            }

            stateFootball = StateFootball.MoveToOpponent;

        }

        if (ball.GetComponent<FootballCollision>().ballOwner == HeroType.Helper) {

            if (ball.GetComponent<FootballCollision>().ballOwnerGo == this.gameObject)
            {

                aim = opponentGate;
                stateFootball = StateFootball.MoveToGate;
                ball.transform.position = GetComponentInChildren<BallPlace>().transform.position;

            }
            else
            {

                aim = GetNearestOpponent(this.gameObject, "FootballEnemy");
                stateFootball = StateFootball.MoveToOpponent;

            }    
     
        }

        if (ball.GetComponent<FootballCollision>().ballOwner == HeroType.Player) {

            aim = GetNearestOpponent(this.gameObject, "FootballEnemy");
            stateFootball = StateFootball.MoveToOpponent;

        }


        if (ball.GetComponent<FootballCollision>().ballOwner == HeroType.Lifeless) {

            if (IsNearest(this.gameObject, ball, "FootballHelper")){

                stateFootball = StateFootball.MoveToBall;
                aim = ball;

            }
            else {

                aim = GetNearestOpponent(this.gameObject, "FootballEnemy");
                stateFootball = StateFootball.MoveToOpponent;

            }
            
        }

        if (stateFootball == StateFootball.MoveToOpponent) {

            RotateToAim(aim);

            shootTime -= Time.deltaTime;

            if (shootTime <= 0 && Vector3.Distance(this.transform.position, aim.transform.position) <= shootDistance)
            {

                Shoot(aim);
                shootTime = shootTimeInit;

                if (aim.GetComponent<IDemagable>().GetCurrentHealth() <= 0)
                {

                    aim = GetNearestOpponent(this.gameObject, "FootballEnemy");
                    stateFootball = StateFootball.MoveToOpponent;

                }
            }

        }

        navmeshAgent.destination = aim.transform.position;

    }

  

    private void OnCollisionEnter(Collision collision)
    {

        if (health.GetCurrentHealth() > 0 && collision.gameObject.GetComponent<FootballCollision>())
        {

            collision.gameObject.GetComponent<FootballCollision>().ballOwner = spawnInfo.heroType;
            aim = opponentGate;

            EquipBall(collision.gameObject);

        }

    }

    void EquipBall(GameObject ball) {

        Transform ballPlace = GetComponentInChildren<BallPlace>().transform;

        ball.transform.position = ballPlace.transform.position;

        ball.transform.SetParent(ballPlace);

        ball.GetComponent<FootballCollision>().ballOwner = HeroType.Helper;
        ball.GetComponent<FootballCollision>().ballOwnerGo = this.gameObject;
        stateFootball = StateFootball.MoveToGate;
        ball.GetComponent<Rigidbody>().isKinematic = true;

    }

    void DropBall() {

        Transform ballPlace = GetComponentInChildren<BallPlace>().transform;

        if (ballPlace.childCount == 1) {

           ballPlace.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            ballPlace.GetChild(0).gameObject.GetComponent<FootballCollision>().ballOwner = HeroType.Lifeless;
            ballPlace.GetChild(0).SetParent(null);
            stateFootball = StateFootball.MoveToBall;
            aim = ball;

        }

    }

    bool IsNearest(GameObject go, GameObject opponent, string tag) {

        GameObject[] helpers = GameObject.FindGameObjectsWithTag(tag);

        bool flag = false;

        float maxDistance = 9000f;
        float currentDistance;
        GameObject currentHelper = go;

        foreach (GameObject helper in helpers) {

            currentDistance = Vector3.Distance(helper.transform.position, opponent.transform.position);

            if (currentDistance <= maxDistance) {

                maxDistance = currentDistance;
                currentHelper = helper;
            }

        }

        if (currentHelper == go) {

            flag = true;

        }

        return flag;

    }

    GameObject GetNearestOpponent(GameObject go, string tag) {

        GameObject[] opponents = GameObject.FindGameObjectsWithTag(tag);
          
        float maxDistance = 9000f;
        float currentDistance;
        GameObject currentOpponent = opponents[0];

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

     void DrawRays()
    {

        for (int i = 0; i < rays.Length; i++){

                    Debug.DrawLine(rays[i].position, rays[i].position + rays[i].forward * searchDistance, Color.red);
            }


        }

    

}

using UnityEngine;

public class AIMoveableRaycastVersusPlayer : AIMoveableRaycast{

    // Start is called before the first frame update
    void OnEnable(){

       aim = GameObject.FindGameObjectWithTag("Player");

       health = GetComponent<IDemagable>();
        
    }

    protected override bool AimDetected(GameObject aim){

        float realAngle = Vector3.Angle(eyePlace.forward, aim.transform.position - eyePlace.position);

        RaycastHit hit;

        if (Physics.Raycast(eyePlace.position, aim.transform.position - eyePlace.position, out hit, searchDistance)){

            if ((realAngle < viewAngle / 2f &&
                Vector3.Distance(eyePlace.position, aim.transform.position) <= searchDistance && hit.transform == aim.transform) ||
                Vector3.Distance(eyePlace.position, aim.transform.position) <= findDistance){

                return true;

            }

        }

        return false;
    }

    protected override bool AimDetected(){

        if (Vector3.Distance(eyePlace.position, aim.transform.position) <= findDistance){

            return true;

        }

        RaycastHit hit;

        bool aimDetected = false;

        for (int i = 0; i < rays.Length; i++){

            if (Physics.Raycast(rays[i].position, rays[i].position + rays[i].forward * searchDistance, out hit, searchDistance))
            {

                if (hit.collider.gameObject.GetComponent<SpawnInfo>() &&
                    hit.collider.gameObject.GetComponent<SpawnInfo>().heroType == HeroType.Player)
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
    void Update(){

        if (GameManager.isPaused || aim == null) { return; }

        if (aim.GetComponent<IDemagable>().GetCurrentHealth() <= 0 || GameManager.gameOver) { 
            state = State.Searching;
        }

        if (health.GetCurrentHealth() <= 0){

            state = State.Searching;
            navmeshAgent.speed = 0;
            return;

        }
        else {

            navmeshAgent.speed = speed;

        }

        DrawViewState();

        if (state == State.Searching){

            if (canChangeSpawnPoint){

                canChangeSpawnPoint = false;

                MoveToRandomSpawnPoint(currentSpawnPointAim);

            }

            if (Vector3.Distance(navmeshAgent.transform.position, currentSpawnPointAim) <= stopDistance) {

                timeToWait -= Time.deltaTime;

                if (timeToWait <= 0) {

                    currentSpawnPointAim = GetRandomSpawnPoint();
                    canChangeSpawnPoint = true;
                    timeToWait = timeToWaitInit;

                }
            } 
            
            if (awakeFromMissile) {

                ChangeSpawnPoint();
                canChangeSpawnPoint = true;
                awakeFromMissile = false;

            }

            if (Vector3.Distance(navmeshAgent.transform.position, aim.transform.position) <= findDistance){

                state = State.MoveToAim;

            }

        }
        else{

            currentSpawnPointAim = GetRandomSpawnPoint();
            canChangeSpawnPoint = true;
            timeToWait = timeToWaitInit;

            MoveToAim(aim);
            RotateToAim(aim);

            if (CanSee(aim) &&
                (Vector3.Distance(transform.position, aim.transform.position) <= shootDistance)){

                transform.LookAt(aim.transform.position);

                state = State.Atacking;

            }
            else
            {
                state = State.MoveToAim;

            }

        }

        if (state == State.Atacking){

            shootTime -= Time.deltaTime;

            if (shootTime <= 0){

                Shoot(aim);

                shootTime = shootTimeInit;
            }

        }

        if (state == State.MoveToAim || state == State.Atacking){

            return;

        }

        timeToWait -= Time.deltaTime;

        if (timeToWait <= 0){

            if (AimDetected(aim)){

                state = State.MoveToAim;

            }

            timeToWait = timeToWaitInit;

        }

    }
 
}

using UnityEngine;

public class AIDroidRockie : AIMoveableMissiled
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
    void Awake()
    {
        health = GetComponent<IDemagable>();
        navmeshAgent.speed = speed;
        aim = GameObject.Find("Player");
        navmeshAgent.SetDestination(aim.transform.position);

    }

    // Update is called once per frame
    void Update(){

        if (GameManager.isPaused || GameManager.gameOver) {return; }

        if (health.GetCurrentHealth() <= 0 || GameManager.gameOver)
        {

            navmeshAgent.speed = 0;

            return;
        }
        else{

            navmeshAgent.speed = speed;

        }

        navmeshAgent.destination = aim.transform.position;

        RotateToAim(aim.transform.position);
   
    }

        protected void RotateToAim(Vector3 aim){

            Vector3 lookVector = aim - transform.position;
            lookVector.y = 0;

            if (lookVector == Vector3.zero) { return; }

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(lookVector, Vector3.up),
                rotationSpeed * Time.deltaTime);

        }
    
}


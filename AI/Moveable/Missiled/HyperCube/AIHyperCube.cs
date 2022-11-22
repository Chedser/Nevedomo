using UnityEngine;

public class AIHyperCube : AIMoveableMissiled {

    [SerializeField]
    Transform[] shootPlaces;

    protected override bool AimDetected(GameObject aim)
    {
        throw new System.NotImplementedException();
    }

    protected override bool AimDetected()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Awake() {

        aim = GameObject.Find("Player");
        state = State.MoveToAim;
        health = GetComponent<IDemagable>();

    }

    // Update is called once per frame
    void Update() {

        if (aim == null || aim.GetComponent<IDemagable>().GetCurrentHealth() <= 0 || GameManager.gameOver || GameManager.isPaused)
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

        RotateToAim(aim);

        if (state == State.MoveToAim || state == State.Atacking){
 
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

        if (state == State.Atacking){

            shootTime -= Time.deltaTime;

            if (shootTime <= 0){

                Shoot();

                shootTime = shootTimeInit;
            }

        }

        navmeshAgent.destination = aim.transform.position;

    }

    void ShootWeapon(Transform shootPlace) {

        GameObject missileGo = Instantiate(missile, shootPlace.position, shootPlace.rotation);
        Missile missileScript = missileGo.GetComponent<Missile>();
        Vector3 dir = transform.forward;
        missileScript.MoveTo(dir);
        missileScript.owner = spawnInfo.heroType;
        missileScript.ownerGo = spawnInfo.gameObject;

        shootPlace.GetChild(0).GetComponent<ParticleSystem>().Play();
        AudioSource shootSound = shootPlace.GetChild(1).GetComponent<AudioSource>();
        if (!shootSound.isPlaying)
        {
            shootSound.Play();
        }

    }

    new void Shoot() {

        foreach (Transform weapon in shootPlaces){

            ShootWeapon(weapon);

        }
    }

    new bool CanSee(GameObject aim){

        RaycastHit hit;

        bool aimDetected = false;

        for (int i = 0; i < rays.Length; i++)
        {

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

}

using UnityEngine;

public class AIATM : AIMoveableRaycast{

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
    void Awake(){

        navmeshAgent.speed = speed;

    }

    void Shoot() {

        foreach (Transform weapon in shootPlaces) {

            ShootWeapon(weapon);

        }

    }

    void ShootWeapon(Transform shootPlace){

        shootPlace.GetChild(0).GetComponent<ParticleSystem>().Play();
        AudioSource shootSound = shootPlace.GetChild(1).GetComponent<AudioSource>();
        if (!shootSound.isPlaying) {
            shootSound.Play();
        }

        if (Random.Range(0, kickChance) == 1)
        {

            RaycastHit hit;

            if (Physics.Raycast(shootPlace.position, shootPlace.forward * searchDistance, out hit, searchDistance))
            {

                if (hit.collider.gameObject.GetComponent<IDemagable>() != null && hit.collider.gameObject.GetComponent<IDemagable>().GetCurrentHealth() > 0){

                    hit.collider.gameObject.GetComponent<IDemagable>().TakeDemage(demage, spawnInfo.heroType);

                    if (hit.collider.gameObject.GetComponent<IDemagable>().GetCurrentHealth() <= 0 &&
                        this.GetComponent<Stats>()){

                        this.GetComponent<Stats>().SetKills(hit.collider.gameObject.GetComponent<SpawnInfo>().heroType);

                    }

                }

                ShowBulletImpact(hit);
                PlaySoundImpact(hit);
                AddForceToBody(hit.rigidbody, shootPlace.forward * searchDistance, thrustToMove);
                DestroyFixedJoint(hit.collider.gameObject);
                TryToExplode(hit.collider.gameObject);

            }

        }
    }

    // Update is called once per frame
    void Update(){
        
        if (GameManager.isPaused) { return; }

        if (canChangeSpawnPoint){

            canChangeSpawnPoint = false;

            MoveToRandomSpawnPoint(currentSpawnPointAim);

        }

        if (Vector3.Distance(navmeshAgent.transform.position, currentSpawnPointAim) <= stopDistance)
        {

            timeToWait -= Time.deltaTime;

            if (timeToWait <= 0)
            {

                currentSpawnPointAim = GetRandomSpawnPoint();
                canChangeSpawnPoint = true;
                timeToWait = timeToWaitInit;

            }
        }

        if (GameManager.gameOver) { return; }

        shootTime -= Time.deltaTime;

        if (shootTime <= 0){

            Shoot();

            shootTime = shootTimeInit;
        }


    }
}

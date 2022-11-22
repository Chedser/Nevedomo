using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteStrikie : AIMoveableMissiled
{
    string _commandToAttackTag = "Helper";
    [SerializeField]
    float timeToChangeAimInit = 3.0f;
    float timeToChangeAim;

    public float shootDistanceAK = 30.0f;
    public float shootTimeAKInit = 0.3f;
    float shootTimeAK;

    [SerializeField]
    Transform akPlace;
    [SerializeField]
    AudioSource shootAKsound;
    [SerializeField]
    ParticleSystem shootAKEffect;

    // BULLET IMPACTS
    [Space, Header("Bullet Impacts")]
    [SerializeField] protected GameObject bulletImpactGroundPr;
    [SerializeField] protected GameObject bulletImpactSandPr;
    [SerializeField] protected GameObject bulletImpactWoodPr;
    [SerializeField] protected GameObject bulletImpactIronPr;

    // BLOOD IMPACTS
    [Space, Header("Blood Impacts")]
    [SerializeField] protected GameObject bloodImpactPr;

    // SOUND IMPACTS
    [Space, Header("Sound Impacts")]
    [SerializeField] protected GameObject soundImpactMetal;
    [SerializeField] protected GameObject soundImpactStone;
    [SerializeField] protected GameObject soundImpactFlesh;

    private void OnEnable()
    {

        navmeshAgent.speed = speed;
        timeToChangeAim = timeToChangeAimInit;
        aim = GetNearestOpponent(this.gameObject, _commandToAttackTag);

        shootTimeAK = shootTimeAKInit;

    }

    protected override bool AimDetected(){

        RaycastHit hit;

        bool aimDetected = false;

        for (int i = 0; i < rays.Length; i++)
        {

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

    private void Awake(){

        health = GetComponent<IDemagable>();

        if (spawnInfo.heroType == HeroType.Helper)
        {

            _commandToAttackTag = "Enemy";

        }

        aim = GetNearestOpponent(this.gameObject, _commandToAttackTag);

    }


    // Update is called once per frame
    void FixedUpdate(){

        if (GameManager.isPaused || GameManager.gameOver) { return; }

        if (health.GetCurrentHealth() <= 0 || aim == null || aim.GetComponent<IDemagable>().GetCurrentHealth() <= 0)
        {

            state = State.Searching;
            navmeshAgent.speed = 0;
            aim = GetNearestOpponent(this.gameObject, _commandToAttackTag);
            return;

        }
        else
        {

            navmeshAgent.speed = speed;

        }
       
            if (aim != null || AimDetected())
            {

                state = State.MoveToAim;

            }
           
        RotateToAim(aim);

        if (CanSee(aim) &&
               (Vector3.Distance(transform.position, aim.transform.position) <= shootDistance))
        {

            transform.LookAt(aim.transform.position);

            state = State.Atacking;

        }
        else{
            state = State.MoveToAim;

        }


        if (state == State.Atacking){

            shootTime -= Time.deltaTime;

            if (shootTime <= 0){

                Shoot();

                if (aim.GetComponent<IDemagable>().GetCurrentHealth() <= 0){

                    aim = GetNearestOpponent(this.gameObject, _commandToAttackTag);

                }

                shootTime = shootTimeInit;
            }

            if (CanSee(aim) &&
              (Vector3.Distance(transform.position, aim.transform.position) <= shootDistanceAK))
            {

                shootTimeAK -= Time.deltaTime;

                if (shootTimeAK <= 0){

                    Shoot(aim);

                    if (aim.GetComponent<IDemagable>().GetCurrentHealth() <= 0)
                    {

                        aim = GetNearestOpponent(this.gameObject, _commandToAttackTag);

                    }

                    shootTimeAK = shootTimeAKInit;
                }

            }
       

        }

        navmeshAgent.destination = aim.transform.position;

    }

    GameObject GetNearestOpponent(GameObject go, string tag){

        GameObject[] opponents = GameObject.FindGameObjectsWithTag(tag);

        if (opponents.Length == 0) { return null; }

        GameObject currentOpponent = opponents[0];
        float maxDistance = Vector3.Distance(go.transform.position, opponents[0].transform.position);
        float currentDistance;

        foreach (GameObject opponent in opponents){

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

    public float thrustToMove;

    protected void PlaySoundImpact(RaycastHit hit)
    {
        if (hit.collider.gameObject.GetComponent<RigidbodyInfo>())
        {

            RigidbodyInfo rigidbodyInfo = hit.collider.gameObject.GetComponent<RigidbodyInfo>();

            GameObject soundToInst = soundImpactMetal;

            switch (rigidbodyInfo.rigidbodyType)
            {

                case RigidbodyType.Iron: soundToInst = soundImpactMetal; break;
                case RigidbodyType.Stone: soundToInst = soundImpactStone; break;
                case RigidbodyType.Flesh: soundToInst = soundImpactFlesh; break;

            }

            Instantiate(soundToInst, hit.collider.transform.position, Quaternion.identity);

        }
    }

    protected GameObject GetBulletImpact(RaycastHit hit)
    {

        GameObject goPr = bulletImpactGroundPr;

        if (hit.collider.gameObject.GetComponentInParent<RigidbodyInfo>())
        {

            RigidbodyType rigidbodyType = hit.collider.gameObject.GetComponentInParent<RigidbodyInfo>().rigidbodyType;

            switch (rigidbodyType)
            {

                case RigidbodyType.Iron: goPr = bulletImpactIronPr; break;
                case RigidbodyType.Sand: goPr = bulletImpactSandPr; break;
                case RigidbodyType.Wood: goPr = bulletImpactWoodPr; break;
                case RigidbodyType.Flesh: goPr = bloodImpactPr; break;

            }

        }

        return goPr;

    }

    protected void ShowBulletImpact(RaycastHit hit)
    {

        GameObject goPr = GetBulletImpact(hit);

        GameObject obj = Instantiate(goPr, hit.collider.transform.position, Quaternion.identity);
        obj.transform.SetParent(hit.collider.transform);
        obj.transform.position = hit.point + hit.normal * 0.01f;
        obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        obj.transform.rotation = Quaternion.FromToRotation(obj.transform.up, hit.normal);
        obj.GetComponent<ParticleSystem>().Play();


    }

    protected void AddForceToBody(Rigidbody rb, Vector3 dir, float thrust)
    {

        if (rb == null) { return; }

        rb.AddForce(dir * thrust, ForceMode.Impulse);

    }

    protected void DestroyFixedJoint(GameObject hitGo)
    {

        FixedJoint fj = hitGo.GetComponent<FixedJoint>();

        if (fj)
        {

            Destroy(fj);

        }

    }
    protected void TryToExplode(GameObject hitGo)
    {

        if (hitGo.GetComponent<IExplodable>() != null)
        {

            hitGo.GetComponent<IExplodable>().Activate(spawnInfo.heroType, this.gameObject);

        }

    }

    protected void Shoot(GameObject aim)
    {
        shootAKEffect.Play();

        if (!shootAKsound.isPlaying)
        {

            shootSound.Play();

        }

        if (Random.Range(0, kickChance) == 1)
        {

            RaycastHit hit;

            if (Physics.Raycast(eyePlace.position, eyePlace.forward * searchDistance, out hit, searchDistance))
            {

                if (hit.collider.transform == aim.transform &&
                    hit.collider.gameObject.GetComponent<IDemagable>().GetCurrentHealth() > 0)
                {

                    hit.collider.gameObject.GetComponent<IDemagable>().TakeDemage(demage, spawnInfo.heroType);

                    if (hit.collider.gameObject.GetComponent<IDemagable>().GetCurrentHealth() <= 0 &&
                        this.GetComponent<Stats>())
                    {

                        this.GetComponent<Stats>().SetKills(hit.collider.gameObject.GetComponent<SpawnInfo>().heroType);

                    }

                }

                ShowBulletImpact(hit);
                PlaySoundImpact(hit);
                AddForceToBody(hit.rigidbody, eyePlace.forward * searchDistance, thrustToMove);
                DestroyFixedJoint(hit.collider.gameObject);
                TryToExplode(hit.collider.gameObject);

            }

        }
    }


}

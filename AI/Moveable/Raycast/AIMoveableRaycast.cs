using UnityEngine;

public abstract class AIMoveableRaycast : AIMoveable{

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

    public float thrustToMove;

    protected void PlaySoundImpact(RaycastHit hit){
        if (hit.collider.gameObject.GetComponent<RigidbodyInfo>()){

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

    protected GameObject GetBulletImpact(RaycastHit hit){

        GameObject goPr = bulletImpactGroundPr;

        if (hit.collider.gameObject.GetComponentInParent<RigidbodyInfo>()){

            RigidbodyType rigidbodyType = hit.collider.gameObject.GetComponentInParent<RigidbodyInfo>().rigidbodyType;

            switch (rigidbodyType){

                case RigidbodyType.Iron: goPr = bulletImpactIronPr; break;
                case RigidbodyType.Sand: goPr = bulletImpactSandPr; break;
                case RigidbodyType.Wood: goPr = bulletImpactWoodPr; break;
                case RigidbodyType.Flesh: goPr = bloodImpactPr; break;

            }

        }

        return goPr;

    }

    protected  void ShowBulletImpact(RaycastHit hit){

        GameObject goPr = GetBulletImpact(hit);

        GameObject obj = Instantiate(goPr, hit.collider.transform.position, Quaternion.identity);
        obj.transform.SetParent(hit.collider.transform);
        obj.transform.position = hit.point + hit.normal * 0.01f;
        obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        obj.transform.rotation = Quaternion.FromToRotation(obj.transform.up, hit.normal);
        obj.GetComponent<ParticleSystem>().Play();


    }

    protected void AddForceToBody(Rigidbody rb, Vector3 dir, float thrust){

        if (rb == null) { return; }

        rb.AddForce(dir * thrust, ForceMode.Impulse);

    }

    protected  void DestroyFixedJoint(GameObject hitGo){

        FixedJoint fj = hitGo.GetComponent<FixedJoint>();

        if (fj)
        {

            Destroy(fj);

        }

    }
    protected void TryToExplode(GameObject hitGo){

        if (hitGo.GetComponent<IExplodable>() != null){

            hitGo.GetComponent<IExplodable>().Activate(spawnInfo.heroType, this.gameObject);

        }

    }

    protected void Shoot(GameObject aim){
        shootEffect.Play();

        if (!shootSound.isPlaying){

            shootSound.Play();

        }

        if (Random.Range(0, kickChance) == 1){

            RaycastHit hit;

            if (Physics.Raycast(eyePlace.position, eyePlace.forward * searchDistance, out hit, searchDistance)){

                if (hit.collider.transform == aim.transform && 
                    hit.collider.gameObject.GetComponent<IDemagable>().GetCurrentHealth() > 0){

                    hit.collider.gameObject.GetComponent<IDemagable>().TakeDemage(demage, spawnInfo.heroType);

                    if (hit.collider.gameObject.GetComponent<IDemagable>().GetCurrentHealth() <= 0 && 
                        this.GetComponent<Stats>()){
                        
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

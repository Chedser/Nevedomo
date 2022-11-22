using UnityEngine;

public class MissileShortGun : Missile, IExplodable{
    public void Activate(HeroType heroType, GameObject bombActivator){

        if (isDone) { return; }
        isDone = true;

        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < overlappedColliders.Length; i++){
            if (!RaycastIntersectionDetected(overlappedColliders[i])) { continue; }
            PushRigidbody(overlappedColliders[i]);
            ActivateExplodable(overlappedColliders[i]);
            BreakFixedJoint(overlappedColliders[i]);
            ChangeAISpawnPoint(overlappedColliders[i]);

            if (DeadDistanceDetected(overlappedColliders[i])){

                GiveDemage(overlappedColliders[i]);

            }

        }

        Instantiate(exploisionEffect, transform.position, Quaternion.identity);

        Destroy(this.gameObject);

    }

    public void OnCollisionEnter(Collision collision){

        try {

            if (collision.gameObject.GetComponent<Missile>()){
                          

                if (collision.gameObject.GetComponent<Missile>().owner != HeroType.Player &&
                    owner == HeroType.Player){

                    Use();

                }
                 else if (collision.gameObject.GetComponent<Missile>().owner == HeroType.Player &&
                  owner == HeroType.Player){

              //      rb.useGravity = true;

                } 


            }
            else{

                Use();

            }

        } catch {
  
            Destroy(this.gameObject);

        }

    }
    public void GiveDemage(Collider collider){

        IDemagable health = collider.gameObject.GetComponent<IDemagable>();

        if (health != null){

            if (health.GetCurrentHealth() <= 0) { return; }

            health.TakeDemage(demage, owner);

            if (health.GetCurrentHealth() <= 0){
                ownerGo.GetComponent<Stats>().SetKills(collider.gameObject.GetComponent<SpawnInfo>().heroType);

            }

        }

    }

    public void ActivateExplodable(Collider collider){

        if (collider.gameObject.GetComponent<IExplodable>() != null){

            if (!collider.gameObject.GetComponent<Missile>() ||
                collider.gameObject.GetComponent<MissileMiner>()){

                collider.gameObject.GetComponent<IExplodable>().Activate(owner, ownerGo);

            }

        }
    }

        public void PushRigidbody(Collider collider){
        Rigidbody rigidbody = collider.attachedRigidbody;

        if (rigidbody != null &&
            !collider.gameObject.GetComponent<Missile>()){
            rigidbody.AddExplosionForce(force * 100, transform.position, radius);
        }


    }

    protected override void Use(){

        Activate(owner, ownerGo);

    }


    public bool DeadDistanceDetected(Collider collider){

        bool flag = false;

        float distance = Vector3.Distance(collider.gameObject.transform.position, this.transform.position);

        if (collider.gameObject.GetComponentInParent<SpawnInfo>() &&
            collider.gameObject.GetComponentInParent<SpawnInfo>().heroType != HeroType.Lifeless &&
            collider.gameObject.GetComponentInParent<IDemagable>().GetCurrentHealth() > 0 &&
            distance <= deadRadius){

            flag = true;

        }

        return flag;
    }

    public bool RaycastIntersectionDetected(Collider collider){
        RaycastHit hit;
        bool flag = false;

        if (Physics.Raycast(transform.position, collider.transform.position - transform.position, out hit, radius))
        {

            if (hit.collider == collider ||
                (Vector3.Distance(transform.position, collider.transform.position) < hitToleranceDistance)){

                flag = true;
            }

        }

        return flag;

    }

    public void BreakFixedJoint(Collider collider)
    {

        if (collider.gameObject.GetComponent<FixedJoint>() &&
            !collider.gameObject.GetComponentInParent<SpawnInfo>())
        {

            Destroy(collider.gameObject.GetComponent<FixedJoint>());

        }
    }
}

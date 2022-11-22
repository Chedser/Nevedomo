
using UnityEngine;

public class MissileLaser : Missile, IExplodable{
      
    public void Activate(HeroType heroType, GameObject activatorGo){}

    public void ActivateExplodable(Collider collider)
    {
        if (collider.gameObject.GetComponent<IExplodable>() != null)
        {

            if (!collider.gameObject.GetComponent<Missile>() ||
                collider.gameObject.GetComponent<MissileMiner>())
            {

                collider.gameObject.GetComponent<IExplodable>().Activate(owner, ownerGo);

            }

        }
    }

    public void BreakFixedJoint(Collider collider){
        
        if (collider.gameObject.GetComponent<FixedJoint>()){

            Destroy(collider.gameObject.GetComponent<FixedJoint>());

        }
    }

    public bool DeadDistanceDetected(Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public void GiveDemage(Collider collider){
        IDemagable health = collider.gameObject.GetComponent<IDemagable>();

        if (health != null)
        {

            if (health.GetCurrentHealth() <= 0) { return; }

            health.TakeDemage(demage, owner);

            if (health.GetCurrentHealth() <= 0)
            {
                ownerGo.GetComponent<Stats>().SetKills(collider.gameObject.GetComponent<SpawnInfo>().heroType);

            }

        }
    }

        public void PushRigidbody(Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public bool RaycastIntersectionDetected(Collider collider)
    {
        throw new System.NotImplementedException();
    }

    protected override void Use()
    {
        throw new System.NotImplementedException();
    }

    public void OnCollisionEnter(Collision collision){

        GiveDemage(collision.collider);
        BreakFixedJoint(collision.collider);
        ActivateExplodable(collision.collider);
        Destroy(this.gameObject);
        
    }

    void FixedUpdate() { 
    
        rb.velocity = dir * speed * Time.deltaTime * 100f;

    }

}

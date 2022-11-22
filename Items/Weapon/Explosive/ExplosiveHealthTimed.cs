

using System.Collections;
using UnityEngine;

public class ExplosiveHealthTimed : ExplosiveBasicTimed, IExplodable{

    public float health;
    public void Activate(HeroType heroType, GameObject activatorGo){

        bombActivator = heroType;
        bombActivatorGo = activatorGo;

        if (isDone) { return; }

        this.heroType = heroType;

    }

    protected override void Use()
    {
        throw new System.NotImplementedException();
    }

    public void PushRigidbody(Collider collider){
        Rigidbody rigidbody = collider.attachedRigidbody;
        rigidbody.AddExplosionForce(force * 100000, transform.position, radius, 1f);
    }

    public void ActivateExplodable(Collider collider){

        if (collider.gameObject.GetComponent<IExplodable>() != null) collider.gameObject.GetComponent<IExplodable>().Activate(heroType, bombActivatorGo);


    }

    public void GiveDemage(Collider collider){

        IDemagable health = collider.gameObject.GetComponent<IDemagable>();

        if (health != null)
        {

            health.TakeDemage(demage, bombActivator);

        }

    }

    public bool DeadDistanceDetected(Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public bool RaycastIntersectionDetected(Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public void BreakFixedJoint(Collider collider)
    {
        throw new System.NotImplementedException();
    }
}

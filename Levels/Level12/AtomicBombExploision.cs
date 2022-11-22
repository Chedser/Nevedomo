using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomicBombExploision : Explosive{
    public void Activate(HeroType heroType){

        if (isDone) { return; }

        bombActivator = heroType;
        bombActivatorGo = this.gameObject;
        Use();

    }

    public void ActivateExplodable(Collider collider){
        if (collider.gameObject.GetComponent<IExplodable>() != null){

            if (!collider.gameObject.GetComponent<Missile>() ||
                collider.gameObject.GetComponent<MissileMiner>())
            {

                collider.gameObject.GetComponent<IExplodable>().Activate(bombActivator, bombActivatorGo);

            }

        }
    }

    public void BreakFixedJoint(Collider collider){
        if (collider.gameObject.GetComponent<FixedJoint>() &&
     !collider.gameObject.GetComponent<SpawnInfo>())
        {

            Destroy(collider.gameObject.GetComponent<FixedJoint>());

        }
    }

    public bool DeadDistanceDetected(Collider collider){
        throw new UnityException();
    }

    public void GiveDemage(Collider collider){

        IDemagable health = collider.gameObject.GetComponent<IDemagable>();

        if (health != null){

            if (health.GetCurrentHealth() <= 0) { return; }

            health.TakeDemage(demage, bombActivator);

            if (health.GetCurrentHealth() <= 0 &&
                bombActivatorGo.GetComponent<Stats>() &&
                bombActivatorGo != collider.gameObject){

                bombActivatorGo.GetComponent<Stats>().SetKills(collider.gameObject.GetComponent<SpawnInfo>().heroType);

            }

        }
    }

    public void PushRigidbody(Collider collider){
        Rigidbody rigidbody = collider.attachedRigidbody;

        if (rigidbody != null &&
            !collider.gameObject.GetComponent<Missile>())
        {
            rigidbody.AddExplosionForce(force * 100, transform.position, radius, jumpForce);
        }
    }

    public bool RaycastIntersectionDetected(Collider collider){
        throw new UnityException();
    }

    protected override void Use(){

        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < overlappedColliders.Length; i++){

            PushRigidbody(overlappedColliders[i]);
            ActivateExplodable(overlappedColliders[i]);
            BreakFixedJoint(overlappedColliders[i]);
            GiveDemage(overlappedColliders[i]);

        }
        Instantiate(exploisionEffect, transform.position, Quaternion.identity);

    }
}

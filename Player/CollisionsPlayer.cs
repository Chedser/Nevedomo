using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsPlayer : Collisions{

    public int demage;
    public float colForceToKill;

    public void OnCollisionEnter(Collision collision){

        if (!collision.gameObject.GetComponent<Rigidbody>()) { return; }

        if (collision.gameObject.GetComponent<FixedJoint>() &&
            !collision.gameObject.GetComponent<SpawnInfo>()) {

            Destroy(collision.gameObject.GetComponent<FixedJoint>());

        }

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (collision.gameObject.GetComponent<RigidbodyInfo>()) {

            RigidbodyType rbType = collision.gameObject.GetComponent<RigidbodyInfo>().rigidbodyType;
            GameObject soundToInst = metalCol;

            switch (rbType){

                case RigidbodyType.Iron: soundToInst = metalCol; break;
                case RigidbodyType.Flesh:
 
                    if (collision.relativeVelocity.magnitude >= colForceToKill) {

                        collision.gameObject.GetComponent<IDemagable>().TakeDemage(demage, HeroType.Player);

                    }

                    break;

            }

            Instantiate(soundToInst, collision.contacts[0].point, Quaternion.identity);

            if (collision.gameObject.GetComponent<Explosive>()) {

                collision.gameObject.GetComponent<Explosive>().lastContacter = HeroType.Player;

            }

        }

      

    }

}

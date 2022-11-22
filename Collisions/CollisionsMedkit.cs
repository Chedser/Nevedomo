using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MedkitType {Low, Middle}

public class CollisionsMedkit : MonoBehaviour
{

    public MedkitType medkitType;
    public int medkitHealth;

    [SerializeField] GameObject takeSound;

    private void Awake(){
        switch (medkitType) {

            case MedkitType.Low: medkitHealth = 1000; break;
            case MedkitType.Middle: medkitHealth = 2000; break;

        }
    }

    private void OnCollisionEnter(Collision collision){

        if (collision.gameObject.GetComponentInParent<PlayerHealth>() &&
            collision.gameObject.GetComponentInParent<PlayerHealth>().currentHealth > 0) {

            collision.gameObject.GetComponentInParent<PlayerHealth>().TakeHealth(medkitHealth);

            Instantiate(takeSound, transform.position, Quaternion.identity);

            Destroy(this.gameObject);

        }

    }

}

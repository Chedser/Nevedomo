using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSidewardTake : MonoBehaviour
{
    [SerializeField]
    GameObject soundTake;

    public void OnCollisionEnter(Collision collision){

        if (collision.gameObject.GetComponentInParent<SpawnInfo>() &&
            collision.gameObject.GetComponentInParent<SpawnInfo>().heroType == HeroType.Player &&

            collision.gameObject.GetComponentInChildren<WeaponSidewardController>()){

            WeaponSidewardController weaponSidewardController = collision.gameObject.GetComponentInChildren<WeaponSidewardController>();

            WeaponInfo weaponInfo = GetComponent<WeaponInfo>();

            weaponSidewardController.AddMissile(weaponInfo.weaponType);

            Instantiate(soundTake);

            Destroy(this.gameObject);

        }

    }
}

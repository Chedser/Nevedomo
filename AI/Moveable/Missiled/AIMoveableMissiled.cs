using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIMoveableMissiled : AIMoveable{

    [SerializeField] protected GameObject missile;

    protected void Shoot(){

        GameObject missileGo = Instantiate(missile, shootPlace.position, shootPlace.rotation);
        Missile missileScript = missileGo.GetComponent<Missile>();
        Vector3 dir = transform.forward;
        missileScript.MoveTo(dir);
        missileScript.owner = spawnInfo.heroType;
        missileScript.ownerGo = spawnInfo.gameObject;
      
        shootEffect.Play();
        shootSound.Play();

    }
}

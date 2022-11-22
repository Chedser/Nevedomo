using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStaticMissiled : AI{

    [SerializeField] protected GameObject missile;
    [SerializeField] protected GameObject rotationPlace;
    public float minRotateAngle;
    public float maxRotateAngle;
    public float currentRotateAngle;
    public float rotationSpeed;
    public float rotationAmp = 45f;

    protected void Shoot()
    {

        GameObject missileGo = Instantiate(missile, shootPlace.position, shootPlace.rotation);
        Missile missileScript = missileGo.GetComponent<Missile>();
        Vector3 dir = shootPlace.forward;
        missileScript.MoveTo(dir);
        missileScript.owner = spawnInfo.heroType;
        missileScript.ownerGo = spawnInfo.gameObject;

        shootEffect.Play();
        shootSound.Play();

    }

    protected void Rotate() {

        rotationPlace.transform.localRotation = Quaternion.Euler(new Vector3(0f, Mathf.Sin(Time.time * rotationSpeed) * rotationAmp, 0f));

    }

    protected void Rotate(GameObject aim)
    {

        rotationPlace.transform.LookAt(aim.transform);

    }


}

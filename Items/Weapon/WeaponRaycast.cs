
using UnityEngine;

public abstract class WeaponRaycast : Weapon
{

    public int bulletsToGo;
    public int bulletsToGoInit;
    public float maxKillDistance;

    public int demage;

    public float thrustToMove;

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
    [SerializeField]protected GameObject soundImpactMetal;
    [SerializeField]protected GameObject soundImpactStone;
    [SerializeField]protected GameObject soundImpactFlesh;

    [SerializeField] protected Stats stats;

    protected abstract void SetDemage(RaycastHit hit);
    protected abstract void ShowBulletImpact(RaycastHit hit);
    protected abstract GameObject GetBulletImpact(RaycastHit hit);

    protected abstract void ShowBloodImpact(RaycastHit hit);
    protected abstract void AddForceToBody(Rigidbody rb, Vector3 dir, float thrust);
    protected abstract void DestroyFixedJoint(GameObject hitGo);
    protected abstract void TryToExplode(GameObject hitGo);
    protected abstract void PlaySoundImpact(RaycastHit hit);
    protected abstract void MakeToChangeAim(RaycastHit hit);

    protected void AwakeEnemies() {

        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
     
        float currentDistance = 0;

        for (int i = 0; i < go.Length; i++) {

            currentDistance = Vector3.Distance(transform.position, go[i].transform.position);

            if (currentDistance <= maxKillDistance && 
                go[i].GetComponent<AI>() && 
                go[i].GetComponent<AI>().canBeAwaked &&
                GetComponentInParent<IDemagable>().GetCurrentHealth() > 0) {

                go[i].GetComponent<AI>().aim = GetComponentInParent<SpawnInfo>().gameObject;
                go[i].GetComponent<AI>().state = State.MoveToAim;

            }


        }
    
    }

}

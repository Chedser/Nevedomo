using UnityEngine;

public class MissileTake : MonoBehaviour{

    [SerializeField]
    GameObject soundTake;
  
    public void OnCollisionEnter(Collision collision){

        if (collision.gameObject.GetComponentInParent<SpawnInfo>() && 
            collision.gameObject.GetComponentInParent<SpawnInfo>().heroType == HeroType.Player &&
                        
            collision.gameObject.GetComponentInChildren<MissileController>()){

            MissileController missileController = collision.gameObject.GetComponentInChildren<MissileController>();

            if (!missileController.CanTake()) {
                return;
            }

            WeaponInfo weaponInfo = GetComponent<WeaponInfo>();
                       
            missileController.Equip(weaponInfo.weaponType);

            Instantiate(soundTake);

            Destroy(this.gameObject);

        }

    }
}

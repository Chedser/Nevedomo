using UnityEngine;

public class MissileController : MonoBehaviour{

    [SerializeField] 
    GameObject weaponScripts;

    public void Equip(WeaponType weaponType) {

        WeaponInfo[] children = this.transform.GetComponentsInChildren<WeaponInfo>(true);

        bool isFoundedInSlot = false;
   
        for (int i = 0; i < children.Length; i++) {

            if (children[i].weaponType == weaponType) { // Если в слоте есть, убираем из слота и добавляем в слот активного оружия

                    children[i].GetComponent<Weapon>().enabled = true;
                    children[i].transform.SetParent(weaponScripts.transform);

                    weaponScripts.GetComponent<ISwitchable>().Equip();

                children[i].GetComponent<WeaponMissiled>().UpdateWeaponIndicator();

                isFoundedInSlot = true;
                                      
                break;

            }
         
        }

        if (isFoundedInSlot == false) {
            AddMissile(weaponScripts, weaponType);

        }

    }

    void AddMissile(GameObject go, WeaponType weaponType){
        WeaponInfo[] children = go.GetComponentsInChildren<WeaponInfo>(true);

        for (int i = 0; i < children.Length; i++){

            if (children[i].weaponType == weaponType){

                children[i].GetComponent<WeaponMissiled>().missileCountCurrent = children[i].GetComponent<WeaponMissiled>().missileCountInit;
                children[i].GetComponent<WeaponMissiled>().UpdateWeaponIndicator();

                break;

            }

        }
 
    }

    public bool CanTake() {

        return weaponScripts.GetComponent<ISwitchable>().CanTake();

    }
   
}

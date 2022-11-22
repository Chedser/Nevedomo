using UnityEngine;
using UnityEngine.UI;

public class WeaponSidewardController : MonoBehaviour{

    [SerializeField]
    GameObject[] weaponSideward;
    Transform[] weaponIconHolders;
    GameObject weaponIconSlot;

    private void Awake(){

        weaponIconSlot = GameObject.Find("WeaponIconSlot");

        weaponIconHolders = new Transform[3];

        weaponIconHolders[0] = GameObject.Find("WeaponIconHolder1").transform;
        weaponIconHolders[1] = GameObject.Find("WeaponIconHolder2").transform;
        weaponIconHolders[2] = GameObject.Find("WeaponIconHolder3").transform;
    }

    public  void AddMissile(WeaponType weaponType){
        WeaponInfo currentWeapon;

        for (int i = 0; i < weaponSideward.Length; i++){

            currentWeapon = weaponSideward[i].GetComponent<WeaponInfo>();

            if (currentWeapon.weaponType == weaponType){
             
                currentWeapon.gameObject.GetComponent<WeaponSideward>().missileCountCurrent = currentWeapon.GetComponent<WeaponMissiled>().missileCountInit;
                currentWeapon.gameObject.GetComponent<WeaponSideward>().enabled = true;

                GetIconFromSlot(weaponType);
              
                break;

            }

        }
    }

    void GetIconFromSlot(WeaponType weaponType) {

        Transform detectedWeapon;
        WeaponInfo[] weaponInSlot = weaponIconSlot.GetComponentsInChildren<WeaponInfo>(true);

        bool foundedInSlot = false;

        for (int i = 0; i < weaponInSlot.Length; i++) {

            if (weaponInSlot[i].weaponType == weaponType) {
          
                detectedWeapon = weaponInSlot[i].transform;
                foundedInSlot = true;

                for (int j = 0; j < weaponIconHolders.Length; j++) {

                    if (weaponIconHolders[j].childCount == 0) {

                        detectedWeapon.gameObject.GetComponent<RawImage>().enabled = true;
                        detectedWeapon.gameObject.GetComponent<RawImage>().color = Color.green;
                        detectedWeapon.SetParent(weaponIconHolders[j]);
                        detectedWeapon.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

                        break;

                    }

                }


            }


            if (foundedInSlot){

                break;

            }

        }

        if (foundedInSlot == false) {

            for (int i = 0; i < weaponIconHolders.Length; i++){

                if (weaponIconHolders[i].GetComponentInChildren<WeaponInfo>() &&

                   weaponIconHolders[i].GetComponentInChildren<WeaponInfo>().weaponType == weaponType){

                    weaponIconHolders[i].GetComponentInChildren<RawImage>().color = Color.green;
                    break;
                  
                }

            }

        }

    }

    public void PutWeaponIconInSlot(Transform weaponIcon) {

        weaponIcon.SetParent(weaponIconSlot.transform);
        weaponIcon.GetComponent<RawImage>().color = Color.green;
        weaponIcon.gameObject.GetComponent<RawImage>().enabled = false;

        ChangeIconPlace();

    }

    void ChangeIconPlace() {

        if (weaponIconHolders[2].childCount == 1) {

            Transform choosedPlace;

            if (weaponIconHolders[0].childCount == 0) {

                choosedPlace = weaponIconHolders[0];

            }
            else{

                choosedPlace = weaponIconHolders[0];

            }

            weaponIconHolders[2].localPosition = choosedPlace.localPosition;

        }
    
    }

}

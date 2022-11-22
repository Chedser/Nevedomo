using UnityEngine;

public abstract class WeaponMissiled : Weapon{
    public int missileCountInit;
    public int missileCountCurrent;
    public GameObject missile;
    public MissileController missileController;
    public WeaponIndicatorUpdater weaponIndicatorUpdater;
    protected float timer;

    private void Start(){
        missileCountCurrent = missileCountInit;
    }

    public void HideWeapon(){

        transform.SetParent(missileController.transform);
        missileCountCurrent = missileCountInit;
        weaponController.ShowPreviousWeapon();
        this.enabled = false;

    }

    public void UpdateWeaponIndicator() {

        weaponIndicatorUpdater.UpdateWeaponIndicator();

    }

}

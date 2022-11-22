
using UnityEngine;

public interface ISwitchable{

    public void Equip();
    public bool CanTake();
    public WeaponType GetActiveWeaponType();

    public void UpdateRBS();
    public void UpdateRBS(int weaponSwitch);
  //  public void UpdateWeaponIndicator(int weaponId);

}

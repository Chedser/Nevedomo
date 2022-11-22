
using UnityEngine;

public abstract class WeaponSideward : WeaponMissiled{

    public Transform missilePlace2;
    public Transform aim2;
    protected int currentSideNumber;
    protected Transform currentSide;
    protected Transform currentAim;
    protected Transform weaponIcon;
    public WeaponSidewardController weaponSidewardController;
    public GameObject soundEmpty;


}


using UnityEngine;

public enum WeaponName { DefaultGun, RocketGun };

public abstract class Weapon : Item{

    public float shootTime;
 
   [SerializeField]
   public WeaponController weaponController;
   public Transform missilePlace;
   protected Color colorWeaponIndicator = Color.green;

    public  bool canShoot = true;

    [SerializeField] protected AudioSource shootSound;
    [SerializeField] protected ParticleSystem shootEffect;
    public GameObject aim;

    protected IDemagable health;


}

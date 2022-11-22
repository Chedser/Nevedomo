
using UnityEngine;

public class ShortGun : WeaponMissiled, IShootable{

    private void Awake(){

        health = GetComponentInParent<IDemagable>();

    }

    protected override void Use(){
        Shoot();
    }

    public void Shoot(){

        GameObject missileGo = Instantiate(missile, missilePlace.position, missilePlace.rotation);
        Missile missileScript = missileGo.GetComponent<Missile>();
        Vector3 dir = aim.transform.position - missilePlace.position;
        missileScript.MoveTo(dir);
        missileScript.owner = HeroType.Player;
        missileScript.ownerGo = ownerGo;
        weaponController.canScroll = true;
        weaponController.canShoot = true;
        shootEffect.Play();
        shootSound.Play();

        --missileCountCurrent;

        weaponIndicatorUpdater.UpdateWeaponIndicator((float)missileCountCurrent, (float)missileCountInit);

    }

    // Update is called once per frame
    void Update(){

        if (GameManager.isPaused) { return; }

        if (health.GetCurrentHealth() <= 0 || GameManager.gameOver) { timer = shootTime; return; }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {

            if (Input.GetMouseButton(0))
            {

                Use();

                if (missileCountCurrent <= 0)
                {

                    HideWeapon();

                }

                timer = shootTime;

            }

        }

    }

}


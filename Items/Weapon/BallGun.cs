using UnityEngine;

public class BallGun : WeaponMissiled, IShootable{

    private void Awake(){

        health = GetComponentInParent<IDemagable>();
        canShoot = true;

    }
    protected override void Use(){
        Shoot();
    }

    public void Shoot(){

        GameObject missileGo = Instantiate(missile, missilePlace.position, missilePlace.rotation);
        Missile missileScript = missileGo.GetComponent<Missile>();
        Vector3 dir = aim.transform.position - missilePlace.position;
        missileScript.owner = HeroType.Player;
        missileScript.ownerGo = ownerGo;
        missileScript.MoveTo(dir);
        weaponController.canScroll = true;
        weaponController.canShoot = true;
        shootEffect.Play();
        shootSound.Play();

        --missileCountCurrent;

        weaponIndicatorUpdater.UpdateWeaponIndicator((float)missileCountCurrent, (float)missileCountInit);

    }

    // Update is called once per frame
    void Update(){

        if (health.GetCurrentHealth() <= 0 || GameManager.gameOver ||
            GameManager.isPaused) { timer = shootTime; return; }

        timer -= Time.deltaTime;

        if (timer <= 0){

            if (Input.GetMouseButton(0)) {

                Use();

                if (missileCountCurrent <= 0)
                {

                    HideWeapon();

                }

                timer = shootTime;

            }
            
        }

    }

    public Color Lerp3(Color a, Color b, Color c, float t){
        if (t < 0.5f) // 0.0 to 0.5 goes to a -> b
            return Color.Lerp(a, b, t / 0.5f);
        else // 0.5 to 1.0 goes to b -> c
            return Color.Lerp(b, c, (t - 0.5f) / 0.5f);
    }

}

using UnityEngine;
using UnityEngine.UI;

public class RPG : WeaponSideward, IShootable, ILerpabable{

    void Awake(){

        weaponIcon = GameObject.Find("WeaponIconRPG").transform;
        health = GetComponentInParent<IDemagable>();

    }

    public Color Lerp3(Color a, Color b, Color c, float t)
    {
        if (t < 0.5f) // 0.0 to 0.5 goes to a -> b
            return Color.Lerp(a, b, t / 0.5f);
        else // 0.5 to 1.0 goes to b -> c
            return Color.Lerp(b, c, (t - 0.5f) / 0.5f);
    }

    public void Shoot(){

        if (currentSideNumber == 0){

            currentSide = missilePlace;
            currentAim = aim.transform;
            currentSideNumber = 1;

        }
        else{
            currentSide = missilePlace2;
            currentAim = aim2.transform;
            currentSideNumber = 0;
        }

        GameObject missileGo = Instantiate(missile, currentSide.position, currentSide.rotation);
        Missile missileScript = missileGo.GetComponent<Missile>();
        Vector3 dir = currentAim.transform.position - currentSide.position;
        missileScript.owner = HeroType.Player;
        missileScript.ownerGo = ownerGo;
        missileScript.MoveTo(dir);

        //    shootEffect.Play();
        shootSound.Play();

        --missileCountCurrent;

        UpdateWeaponIndicator(missileCountCurrent, missileCountInit);

    }

    public void UpdateLabel(){
        
    }

    public void UpdateWeaponIndicator(float current, float init){

        Color indicatorColor = Lerp3(Color.red, Color.blue, Color.green, current / init);

        weaponIcon.GetComponent<RawImage>().color = indicatorColor;

    }

    protected override void Use(){
        Shoot();
    }

    // Update is called once per frame
    void Update(){

        if (health.GetCurrentHealth() <= 0 || GameManager.gameOver ||
            GameManager.isPaused) { return; }

        if (missileCountCurrent <= 0) {
            weaponSidewardController.PutWeaponIconInSlot(weaponIcon);
            this.enabled = false;
            return; }

        timer -= Time.deltaTime;

        if (timer <= 0){

            if (Input.GetMouseButton(2)){

                Use();
                timer = shootTime;
            }

        }
    }
}

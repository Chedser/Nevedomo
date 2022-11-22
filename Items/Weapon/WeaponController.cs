using UnityEngine;

public class WeaponController : WeaponBasicSwitch, ISwitchable{

    [SerializeField]
    AudioSource soundWeaponChange;
    [SerializeField]
    AudioSource soundWeaponEmpty;
    [SerializeField]
    GameObject[] RBS;

    Transform weaponContainer;
    public  int currentWeaponID;
    int weaponSwitchID = 0;

    public float mouseScroll;

    public float timer = 0.3f;
    float repeatTimer;

    public bool canScroll;
    public bool canShoot = true;

    void Awake(){

        weaponContainer = this.transform;
        repeatTimer = timer;
        canShoot = true;
        canScroll = true;

    }

    public void Equip(){

        ShowActiveWeapon();

    }

    public void ShowPreviousWeapon(){

        weaponContainer.GetChild(weaponContainer.childCount - 1).gameObject.SetActive(true);
        weaponContainer.GetChild(weaponContainer.childCount - 1).gameObject.GetComponent<Weapon>().canShoot = true;

        soundWeaponEmpty.Play();

        UpdateRBS();

    }

    public bool CanTake(){

        bool flag = false;

        for (int i = 0; i < weaponContainer.childCount; i++){

            if (i == 0 && weaponContainer.GetChild(i).gameObject.activeSelf &&
                weaponContainer.GetChild(i).gameObject.GetComponent<Weapon>().canShoot){

                flag = true;
                break;

            }
            else if (i != 0){

                flag = true;
                break;

            }

        }

        return flag;

    }

    void Update(){

        if (weaponContainer.childCount <= 1){

            timer = 0;

            return;
        }

        timer -= Time.deltaTime;

        currentWeaponID = weaponSwitchID;

        if (timer <= 0 && canShoot){

            ScrollWeapon();

            canScroll = true;

        }

        if (currentWeaponID != weaponSwitchID && canScroll){

            Switch();
            timer = repeatTimer;

        }

    }

    protected override void Switch(){

        if (weaponContainer.childCount <= 1) { return; }

        for (int i = 0; i < weaponContainer.childCount; i++) {

            if (i == weaponSwitchID){

                weaponContainer.GetChild(i).gameObject.SetActive(true);
                weaponContainer.GetChild(i).gameObject.GetComponent<Weapon>().canShoot = true;


            }
            else{

                weaponContainer.GetChild(i).gameObject.SetActive(false);

            }

        }

        timer = repeatTimer;
        canScroll = false;

        if (!soundWeaponChange.isPlaying){

            soundWeaponChange.Play();

        }

        UpdateRBS(weaponSwitchID);
    //    UpdateWeaponIndicator(weaponSwitchID);


    }

    protected override void ShowActiveWeapon()// Вызывается когда юзер поднимает оружие
    {
        if (weaponContainer.childCount <= 1) { return; }

        currentWeaponID = weaponContainer.childCount - 1;

        for (int i = 0; i < weaponContainer.childCount; i++){

            if (i != currentWeaponID){

                weaponContainer.GetChild(i).gameObject.SetActive(false);

            }

        }

        UpdateRBS();
     //   UpdateWeaponIndicator(currentWeaponID);

    }

    void ScrollWeapon()
    {

        if (mouseScroll > 0.0f)
        {

            if (weaponSwitchID >= (weaponContainer.childCount - 1))
            {

                weaponSwitchID = 0;

            }
            else
            {

                weaponSwitchID++;

            }

        }

        if (mouseScroll < 0.0f)
        {

            if (weaponSwitchID <= 0)
            {

                weaponSwitchID = weaponContainer.childCount - 1;

            }
            else
            {

                weaponSwitchID--;

            }

        }

    }

    public WeaponType GetActiveWeaponType(){

        WeaponType weaponType = WeaponType.DefaultGun;

        currentWeaponID = weaponContainer.childCount - 1;

        for (int i = 0; i < weaponContainer.childCount; i++)
        {

            if (i == currentWeaponID)
            {

                weaponType = weaponContainer.GetChild(i).gameObject.GetComponent<WeaponInfo>().weaponType;

            }

        }

        return weaponType;
    }

    public void UpdateRBS(){

        WeaponType weaponType = GetActiveWeaponType();

        for (int i = 0; i < RBS.Length; i++)
        {

            if (RBS[i].GetComponent<WeaponInfo>().weaponType == weaponType)
            {

                RBS[i].SetActive(true);

            }
            else
            {

                RBS[i].SetActive(false);

            }

        }

    }

    public void UpdateRBS(int weaponSwitch){

        WeaponType weaponType = WeaponType.DefaultGun;

        for (int i = 0; i < weaponContainer.childCount; i++){

            if (i == weaponSwitch){

                weaponType =   weaponContainer.GetChild(i).gameObject.GetComponent<WeaponInfo>().weaponType;
                break;
            }

        }

        for (int i = 0; i < RBS.Length; i++) {

            if (RBS[i].GetComponent<WeaponInfo>().weaponType == weaponType){

                RBS[i].SetActive(true);

            }
            else {

                RBS[i].SetActive(false);

            }

        }
        
    }

    public Color Lerp3(Color a, Color b, Color c, float t){
        if (t < 0.5f) // 0.0 to 0.5 goes to a -> b
            return Color.Lerp(a, b, t / 0.5f);
        else // 0.5 to 1.0 goes to b -> c
            return Color.Lerp(b, c, (t - 0.5f) / 0.5f);
    }

 /*   public void UpdateWeaponIndicator(int weaponId){

        Color indicatorColor = Color.green;

        if (weaponContainer.GetChild(weaponId).gameObject.GetComponent<WeaponInfo>().weaponType != WeaponType.DefaultGun) {

            WeaponMissiled weaponMissiled = weaponContainer.GetChild(weaponId).gameObject.GetComponent<WeaponMissiled>();
            float missileCountCurrent = (float)weaponMissiled.missileCountCurrent;
            float missileCountInit = (float)weaponMissiled.missileCountInit;
            indicatorColor = Lerp3(Color.green, Color.blue, Color.red, missileCountCurrent / missileCountInit);

        }

        weaponIndicator.GetComponent<SpriteRenderer>().color = indicatorColor;
    } */
}

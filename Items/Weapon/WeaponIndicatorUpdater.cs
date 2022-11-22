using UnityEngine;

public class WeaponIndicatorUpdater : MonoBehaviour, ILerpabable{

    [SerializeField]
    WeaponMissiled weaponScript;
    [SerializeField]
    SpriteRenderer weaponIndicator;
    Color indicatorColor = Color.green;
    float missileCountCurrent;
    float missileCountInit;

    // Start is called before the first frame update
    void Awake(){

        missileCountCurrent = (float)weaponScript.missileCountCurrent;
        missileCountInit = (float)weaponScript.missileCountInit;
        indicatorColor = Lerp3(Color.green, Color.blue, Color.red, missileCountCurrent / missileCountInit);
        weaponIndicator.color = indicatorColor;
    }
     

    public Color Lerp3(Color a, Color b, Color c, float t){
        if (t < 0.5f) // 0.0 to 0.5 goes to a -> b
            return Color.Lerp(a, b, t / 0.5f);
        else // 0.5 to 1.0 goes to b -> c
            return Color.Lerp(b, c, (t - 0.5f) / 0.5f);
    }

    public void UpdateWeaponIndicator(float current, float init){

        indicatorColor = Lerp3(Color.red, Color.blue, Color.green, current / init);

       // Debug.Log(current + " " + init + " " + current / init);

        weaponIndicator.color = indicatorColor;

    }


    public void UpdateWeaponIndicator(){

        weaponIndicator.color = Color.green;

    }


}

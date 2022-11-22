using UnityEngine;

public enum Language {EN, RU}

public abstract class LanguageManager : MonoBehaviour{

    [SerializeField]
    protected AudioSource clickSound;

    public static Language currentLanguage = Language.RU;

    public bool isMainMenu = false;

    protected void InitLanguage() {

        if (!PlayerPrefs.HasKey("L")){

            PlayerPrefs.SetInt("L", (int)Language.RU);

        }
        else{

            if ((PlayerPrefs.GetInt("L") != (int)Language.EN) &&
                (PlayerPrefs.GetInt("L") != (int)Language.RU)){

                PlayerPrefs.SetInt("L", (int)Language.RU);

            }

        }

        currentLanguage = (Language)PlayerPrefs.GetInt("L");
     
    }

    // Update is called once per frame
    void Update(){

        if (isMainMenu == false) { return; }

        if (Input.GetKeyDown(KeyCode.L)) {

            if (PlayerPrefs.GetInt("L") == (int)Language.EN){

                PlayerPrefs.SetInt("L", (int)Language.RU);
                currentLanguage = Language.RU;

            }
            else {

                PlayerPrefs.SetInt("L", (int)Language.EN);
                currentLanguage = Language.EN;

            }

            UpdateLanguageUI();

            clickSound.Play();
        }

    }
       
    protected virtual void UpdateLanguageUI() { PlayerPrefs.SetInt("L", (int)currentLanguage); }

    protected virtual void UpdateStatsUI() { }

}

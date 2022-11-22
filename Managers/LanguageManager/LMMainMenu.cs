using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class LMMainMenu : LanguageManager{

    [SerializeField]
    RawImage[] images;

    [Header("Main Buttons Text")]
    [SerializeField]
    Text fightButtonTxt;
    [SerializeField]
    Text levelsButtonTxt;
    [SerializeField]
    Text controlsButtonTxt;
    [SerializeField]
    Text exitButtonTxt;

    [Header("Graphics")]
    [SerializeField]
    Text graphicsLabelTxt;
    [SerializeField]
    Text graphicsLevelTxt;

    [Header("Loading")]
    [SerializeField]
    Text loadingTxt;

    [Header("Controls")]
    [SerializeField]
    Text moveTxt;
    [SerializeField]
    Text jumpTxt;
    [SerializeField]
    Text shootTxt;
    [SerializeField]
    Text grenadesTxt;
    [SerializeField]
    Text rpgTxt;
    [SerializeField]
    Text dropMineTxt;
    [SerializeField]
    Text explodeMineTxt;
    [SerializeField]
    Text changeWeaponTxt;
    [SerializeField]
    Text muteMusicTxt;
    [SerializeField]
    Text changeLanguageTxt;
    [SerializeField]
    Text exitTxt;
    [SerializeField]
    Text controlsTxt;
    [SerializeField]
    Text levelsTxt;

    [Header("Stats")]
    [SerializeField]
    Text winsTxt;
    [SerializeField]
    Text killsTxt;

    [SerializeField]
    Text changeLangTxt;

    [HideInInspector]
  public  int winsCount;
    [HideInInspector]
    public int killsCount;

    private void Awake(){

        /*if (SteamManager.Initialized) {

            SteamAPI.RunCallbacks();

            SteamUserStats.RequestCurrentStats();

            winsCount = GetStats("WINS_COUNT");
            killsCount = GetStats("KILLS_COUNT");

        }*/

        UpdateLanguageUI();
}

    protected override void UpdateLanguageUI(){

        InitLanguage();

        Graphics currentQuality = (Graphics)QualitySettings.GetQualityLevel();

        string grStr = currentQuality.ToString();

        if (PlayerPrefs.GetInt("L") == (int)Language.EN){

            images[(int)Language.EN].enabled = true;
            images[(int)Language.RU].enabled = false;

            fightButtonTxt.text = "FIGHT!";
            levelsButtonTxt.text = "LEVELS";
            controlsButtonTxt.text = "CONTROLS";
            exitButtonTxt.text = "EXIT";
            graphicsLabelTxt.text = "GRAPHICS";
            loadingTxt.text = "LOADING...";
            controlsTxt.text = "CONTROLS";
            levelsTxt.text = "LEVELS";
  
            moveTxt.text = "MOVE\t\t\t\t\t\t\tWSAD";
            jumpTxt.text = "JUMP\t\t\t\t\t\t\tSPACE";
            shootTxt.text = "SHOOT\t\t\t\t\t\t\tMOUSE LEFT";
            grenadesTxt.text = "GRENADES\t\t\t\t\tMOUSE RIGHT";
            rpgTxt.text = "RPG\t\t\t\t\t\t\t\tMOUSE MIDDLE";
            dropMineTxt.text = "DROP MINE\t\t\t\t\tF";
            explodeMineTxt.text = "EXPLODE MINE\t\t\t\tE";
            changeWeaponTxt.text = "CHANGE WEAPON\t\t\tMOUSE SCROLL";
            muteMusicTxt.text = "MUTE MUSIC\t\t\t\t\tC";
            changeLanguageTxt.text = "CHANGE LANGUAGE\t\tL";
            exitTxt.text = "EXIT\t\t\t\t\t\t\t\tESC";

            winsTxt.text = "WINS\t" + winsCount;
            killsTxt.text = "KILLS\t" + killsCount;

            changeLangTxt.text = "Press L\nto switch";

        }
        else {

            images[(int)Language.EN].enabled = false;
            images[(int)Language.RU].enabled = true;

            fightButtonTxt.text = "¡Œ…!";
            levelsButtonTxt.text = "”–Œ¬Õ»";
            controlsButtonTxt.text = "”œ–¿¬À≈Õ»≈";
            exitButtonTxt.text = "¬€’Œƒ";
            graphicsLabelTxt.text = "√–¿‘» ¿";
            loadingTxt.text = "œŒ√–”« ¿...";
            controlsTxt.text = "”œ–¿¬À≈Õ»≈";
            levelsTxt.text = "”–Œ¬Õ»";

            moveTxt.text = "ƒ¬»∆≈Õ»≈\t\t\t\t\t\t\tWSAD";
            jumpTxt.text = "œ–€∆Œ \t\t\t\t\t\t\t\t\tœ–Œ¡≈À";
            shootTxt.text = "—“–≈À‹¡¿\t\t\t\t\t\t\t\tÃ€ÿ‹ À≈¬Œ";
            grenadesTxt.text = "√–¿Õ¿“€\t\t\t\t\t\t\t\t\tÃ€ÿ‹ œ–¿¬Œ";
            rpgTxt.text = "–œ√\t\t\t\t\t\t\t\t\t\t\t\tÃ€ÿ‹ —≈–≈ƒ»Õ¿";
            dropMineTxt.text = "¡–Œ—»“‹ Ã»Õ”\t\t\t\t\tF";
            explodeMineTxt.text = "¬«Œ–¬¿“‹ Ã»Õ”\t\t\t\tE";
            changeWeaponTxt.text = "—Ã≈Õ¿ Œ–”∆»ﬂ\t\t\t\tÃ€ÿ‹  –”“»“‹";
            muteMusicTxt.text = "Œ“ Àﬁ◊»“‹ Ã”«€ ”\tC";
            changeLanguageTxt.text = "—Ã≈Õ»“‹ ﬂ«€ \t\t\t\t\tL";
            exitTxt.text = "¬€’Œƒ\t\t\t\t\t\t\t\t\t\tESC";

            winsTxt.text = "œŒ¡≈ƒ€\t" + winsCount;
            killsTxt.text = "”¡»“Œ\t\t" + killsCount;

            changeLangTxt.text = "L\nÒÏÂÌËÚ¸";

            switch (currentQuality){
                case Graphics.Min: grStr = "Ã»Õ»Ã”Ã"; break;
                case Graphics.Fast: grStr = "¡€—“–Œ"; break;
                case Graphics.Normal: grStr = "ÕŒ–Ã¿À‹ÕŒ"; break;
                case Graphics.Good: grStr = "’Œ–ŒÿŒ"; break;
                case Graphics.Proper: grStr = "À”◊ÿ≈"; break;
                case Graphics.Max: grStr = "Ã¿ —»Ã”Ã"; break;

            }

        }

        graphicsLevelTxt.text = grStr;

        PlayerPrefs.SetInt("L", (int)currentLanguage);

    }

    public static int GetStats(string statName){

        int statFromServer = 0;

        SteamUserStats.GetStat(statName, out statFromServer);
    
        return statFromServer;

    }

}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour{

    public int buttonNumber;

    [SerializeField] MenuManager menuManager;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject levelsPanel;

    [SerializeField] GameObject[] buttons;
    [SerializeField] AudioSource hoverSound;
    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource exitSound;
    [SerializeField] Text graphicsTxt;
    [SerializeField] Text loadTxt;
    [SerializeField] RotateLogo rotateLogo;
    [SerializeField] GameObject winImage;

    enum MenuButton {
                    FightButton = 0, 
                    LevelsButton = 1, 
                    ControlsButton = 2, 
                    ExitButton = 3,
                    GraphicsButton = 4
                                      }


   public bool isClicked;

    private void Start(){

        if (LanguageManager.currentLanguage == Language.EN){

            graphicsTxt.text = ((Graphics)PlayerPrefs.GetInt("q")).ToString();

        }
        else {

            Graphics currentGraphics = (Graphics)PlayerPrefs.GetInt("q");

            string grStr = "ÌÈÍÈÌÓÌ";

            switch (currentGraphics) {

                case Graphics.Fast: grStr = "ÁÛÑÒÐÎ"; break;
                case Graphics.Normal: grStr = "ÍÎÐÌÀËÜÍÎ"; break;
                case Graphics.Good: grStr = "ÕÎÐÎØÎ"; break;
                case Graphics.Proper: grStr = "ËÓ×ØÅ"; break;
                case Graphics.Max: grStr = "ÌÀÊÑÈÌÓÌ"; break;

            }

            graphicsTxt.text = grStr;

        }

        if (PlayerPrefs.HasKey("L12")){
            string hashFromRegedit = PlayerPrefs.GetString("L12");
            string hashLevel = HashManager.GetLevelHash(12);

            if (hashFromRegedit == hashLevel){
                winImage.SetActive(true);

            }

        }

    }

    // Update is called once per frame
    void Update(){

        if (isClicked) { return; }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {

            if (buttonNumber >= 0 && buttonNumber != (buttons.Length - 1)) {

                ++buttonNumber;

            } else if (buttonNumber == (buttons.Length - 1)) {

                buttonNumber = 0;

            }

            menuManager.ScaleButton(buttons, buttonNumber);
            menuManager.PlaySound(hoverSound);

        } else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {

            if (buttonNumber == 0){

                buttonNumber = (buttons.Length - 1);

            }else if (buttonNumber <= (buttons.Length - 1)){

                --buttonNumber;

            }

            menuManager.ScaleButton(buttons, buttonNumber);
            menuManager.PlaySound(hoverSound);

        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {

            isClicked = true;

            menuManager.PlaySound(clickSound);
 
            switch (buttonNumber) {

                case (int)MenuButton.FightButton:
                    rotateLogo.enabled = true;
                    loadTxt.enabled = true;
                    menuManager.ScaleButtonInvert(buttons, buttonNumber);
                    SceneManager.LoadScene("Level" + PlayerPrefs.GetString("CL"));
                        break;
                case (int)MenuButton.LevelsButton:
                    menuManager.ShowBlock(levelsPanel);
                    break;
                case (int)MenuButton.ControlsButton: 
                             menuManager.ShowBlock(controlsPanel);
                             break;
                case (int)MenuButton.ExitButton:
                    menuManager.ScaleButtonInvert(buttons, buttonNumber);
                    Application.Quit(); break;
                case (int)MenuButton.GraphicsButton: isClicked = false;
                                                     TriggerQuality(); break;
            }

        }

    }

    void TriggerQuality() {

        int currentQuality = PlayerPrefs.GetInt("q");

        if (currentQuality >= (int)Graphics.Min && currentQuality < (int)Graphics.Max){

            ++currentQuality;

        }
        else if(currentQuality == (int)Graphics.Max) {

            currentQuality = (int)Graphics.Min;

        }

        PlayerPrefs.SetInt("q", currentQuality);

        string grStr = ((Graphics)currentQuality).ToString();

        if (LanguageManager.currentLanguage == Language.RU){

            switch ((Graphics)currentQuality){
                case Graphics.Min: grStr = "ÌÈÍÈÌÓÌ"; break;
                case Graphics.Fast: grStr = "ÁÛÑÒÐÎ"; break;
                case Graphics.Normal: grStr = "ÍÎÐÌÀËÜÍÎ"; break;
                case Graphics.Good: grStr = "ÕÎÐÎØÎ"; break;
                case Graphics.Proper: grStr = "ËÓ×ØÅ"; break;
                case Graphics.Max: grStr = "ÌÀÊÑÈÌÓÌ"; break;

            }

        }
       
        graphicsTxt.text = grStr;

        QualitySettings.SetQualityLevel(currentQuality, true);

    }

}

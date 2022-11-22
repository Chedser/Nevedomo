using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsPanelManager : MonoBehaviour
{
    [SerializeField] GameObject levelsOpenedSlot;
    [SerializeField] GameObject levelsSlot;
    [SerializeField] AudioSource hoverSound;
    [SerializeField] AudioSource clickSound;
    [SerializeField] Text loadText;
    int activeButtonsCount;

   static int buttonNumber = 1;
   bool isClicked;

    // Start is called before the first frame update
    void Awake(){

        PullLevelsFromSlot();
        activeButtonsCount = levelsOpenedSlot.transform.childCount;

    }

    // Update is called once per frame
    void Update()
    {

        if (isClicked) { return; }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        { // Нажали вниз

            if (buttonNumber >= 1 && buttonNumber < activeButtonsCount)
            {

                ++buttonNumber;

            }
            else if (buttonNumber == activeButtonsCount)
            {

                buttonNumber = 1;

            }

            ScaleButton(buttonNumber);
            PlaySound(hoverSound);

        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        { // Нажали вверх

            if (buttonNumber == 1)
            {

                buttonNumber = activeButtonsCount;

            }
            else if (buttonNumber <= activeButtonsCount)
            {

                --buttonNumber;

            }
            ScaleButton(buttonNumber);
            PlaySound(hoverSound);
      
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {

            isClicked = true;

            loadText.enabled = true;

            PlaySound(clickSound);
            levelsOpenedSlot.transform.GetChild(buttonNumber - 1).gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.5f, 0.6f);
            HashManager.SetCurrentLevel(buttonNumber - 1);
            SceneManager.LoadScene("Level" + buttonNumber);

        }
    }

    Transform GetLevelFromSlot(int levelNumber, GameObject go)
    {

        return go.transform.Find("LoadLevel_" + levelNumber.ToString());
    }

    void PullLevelsFromSlot()
    {

        int currentLevelNumber = 2;
        string hashLevel = "";
        string keyFromRegedit = "";
        string hashFromRegedit = "";

        while (true)
        {

            if (!PlayerPrefs.HasKey("L" + currentLevelNumber.ToString())) { break; }

            keyFromRegedit = "L" + currentLevelNumber.ToString();

            hashFromRegedit = PlayerPrefs.GetString(keyFromRegedit);
            hashLevel = HashManager.GetLevelHash(currentLevelNumber);

            if (hashFromRegedit != hashLevel)
            {
                PlayerPrefs.DeleteKey(keyFromRegedit);
                break;

            }
            else
            {

                Transform currentOpenedLevel = GetLevelFromSlot(currentLevelNumber, levelsSlot);
                currentOpenedLevel.transform.SetParent(levelsOpenedSlot.transform);
                currentOpenedLevel.transform.gameObject.SetActive(true);

                ++currentLevelNumber;
            }

        }

    }

    public void ScaleButton(int buttonNumber)
    {

        for (int i = 1; i <= activeButtonsCount; i++) {

            if (i == buttonNumber)
            {

                levelsOpenedSlot.transform.GetChild(i - 1).gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.6f, 0.7f);

            }
            else {
                levelsOpenedSlot.transform.GetChild(i - 1).gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.5f, 0.6f);
            }

        }
        
    }

    public void PlaySound(AudioSource audio)
    {

        if (!audio.isPlaying)
        {

            audio.Play();

        }
        else
        {

            audio.Stop();
            audio.Play();

        }

    }

}

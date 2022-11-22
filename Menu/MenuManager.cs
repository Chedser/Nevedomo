using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Graphics{
    Min = 0,
    Fast = 1,
    Normal = 2,
    Good = 3,
    Proper = 4,
    Max = 5
}

public class MenuManager : MonoBehaviour{
    private void Start(){

        int currentQuality = 0;

        if (!PlayerPrefs.HasKey("CL")){

            PlayerPrefs.SetString("CL", "1");

        }

        if (!PlayerPrefs.HasKey("q")){

            PlayerPrefs.SetInt("q", 0);

        }
        else{

            currentQuality = PlayerPrefs.GetInt("q");

            if (!(currentQuality >= (int)Graphics.Min && currentQuality <= (int)Graphics.Max)){

                PlayerPrefs.SetInt("q", 0);
                currentQuality = 0;
            }

        }

        QualitySettings.SetQualityLevel(currentQuality, true);

        if (!PlayerPrefs.HasKey("CL")){

            PlayerPrefs.SetString("CL", "1");

        }
              

    }
    private void Awake(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void ScaleButton(GameObject[] buttons, int buttonNumber){

        for (int i = 0; i < buttons.Length; i++){

            if (i == buttonNumber){

                buttons[i].GetComponent<RectTransform>().localScale = new Vector3(-1.2f, 1.3f, 1f);

            }
            else{
                buttons[i].GetComponent<RectTransform>().localScale = new Vector3(-1f, 1f, 1f);
            }

        }

    }

    public void ScaleButtonInvert(GameObject[] buttons, int buttonsNumber){
        buttons[buttonsNumber].GetComponent<RectTransform>().localScale = new Vector3(-1f, 1f, 1f);

    }

    public void PlaySound(AudioSource audio){

        if (!audio.isPlaying){

            audio.Play();

        }
        else{

            audio.Stop();
            audio.Play();

        }

    }

    public void ShowBlock(GameObject go){

        go.SetActive(true);

    }

    public IEnumerator LoadAsyncScene(string sceneName){

        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone){
            Debug.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");

            if (asyncOperation.progress >= 0.9f){
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

    }

}
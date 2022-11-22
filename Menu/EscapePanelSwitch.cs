using UnityEngine;

public class EscapePanelSwitch : MonoBehaviour{
   
    [SerializeField] GameObject exitPanel;
    [SerializeField] MenuManager menuManager;
    [SerializeField] AudioSource hoverSound;
    [SerializeField] MusicManager musicManager;

   public bool isShown;

    // Update is called once per frame
    void Update(){

        if (GameManager.gameOver) { return; }

        if (Input.GetKeyDown(KeyCode.Escape)) {
         
            PauseTrigger(isShown);
           
            menuManager.PlaySound(hoverSound);

        }

    }

   public void PauseTrigger(bool _isShown) {

        isShown = !_isShown;

        if (isShown){

            Time.timeScale = 0f;

        }
        else {

            Time.timeScale = 1f;

        }
        GameManager.isPaused = isShown;
        exitPanel.SetActive(isShown);

        musicManager.ChangeMusicVolume();

    }

}

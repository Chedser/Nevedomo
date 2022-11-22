using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeLastLevel : MonoBehaviour{

    bool isClicked;
    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape) && !isClicked){

            SceneManager.LoadScene("MainMenu");
            isClicked = true;
            
        }
    }
}

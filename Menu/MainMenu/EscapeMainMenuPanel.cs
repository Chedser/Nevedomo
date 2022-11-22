using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMainMenuPanel : MonoBehaviour
{

    [SerializeField] MainMenuPanel mainMenuPanel;
    [SerializeField] AudioSource clickSound;

    private void Awake(){

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            mainMenuPanel.isClicked = false;

            clickSound.Play();

            this.gameObject.SetActive(false);

        }
        
    }
}

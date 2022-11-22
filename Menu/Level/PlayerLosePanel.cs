using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLosePanel : MonoBehaviour
{

    public int buttonNumber;

    [SerializeField] MenuManager menuManager;

    [SerializeField] GameObject[] buttons;
    [SerializeField] AudioSource hoverSound;
    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource exitSound;

    bool isClicked;

    // Update is called once per frame
    void Update()
    {

        if (isClicked) { return; }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {

            if (buttonNumber >= 0 && buttonNumber != (buttons.Length - 1))
            {

                ++buttonNumber;

            }
            else if (buttonNumber == (buttons.Length - 1))
            {

                buttonNumber = 0;

            }

            menuManager.ScaleButton(buttons, buttonNumber);
            menuManager.PlaySound(hoverSound);

        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {

            if (buttonNumber == 0)
            {

                buttonNumber = (buttons.Length - 1);

            }
            else if (buttonNumber <= (buttons.Length - 1))
            {

                --buttonNumber;

            }

            menuManager.ScaleButton(buttons, buttonNumber);
            menuManager.PlaySound(hoverSound);

        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {

            isClicked = true;

            switch (buttonNumber)
            {

                case 0:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    menuManager.PlaySound(clickSound);
                    break;
                case 1:
                    SceneManager.LoadScene("MainMenu");
                    menuManager.PlaySound(exitSound); break;
            }

        }

    }

}

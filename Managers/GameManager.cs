using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    public int killsByFriends;
    public int killsByEnemies;
    public int killsToWin;
    public bool isLastLevel;
    [SerializeField] LevelManager levelManager;

    public static bool playerWin;
    public static bool enemyWin;
    public static bool gameOver;
    public static bool isPaused;

    public int currentLevelNumber;

    private void Awake(){

        InitGame();

    }

    void InitGame() {
        playerWin = false;
        enemyWin = false;
        gameOver = false;
        isPaused = false;
        killsByFriends = 0;
        killsByEnemies = 0;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (HashManager.LevelIsCracked(currentLevelNumber)) {

            SceneManager.LoadScene("MainMenu");
        
        };

        if (!PlayerPrefs.HasKey("CL")) {

            HashManager.SetCurrentLevel(0);

        }

    }

    public void UpdateTopLabels() {

        levelManager.UpdateLevelInfo();

    }

}

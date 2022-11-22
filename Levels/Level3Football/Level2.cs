using UnityEngine;

public class Level2 : LevelManager{

  public int helpersGoals;
  public  int enemiesGoals;
  public bool canSetGoal = true;
    [SerializeField] GameObject ball;
    [SerializeField] Transform ballPlace;
    [SerializeField] GameObject player;
    [SerializeField] AudioSource whistleSound;

    public static bool isGoaled;

    string _strBullets;
    string _strRockets;

    // Start is called before the first frame update
    void Awake(){

        if (LanguageManager.currentLanguage == Language.EN){

            _strBullets = "Bullets \t\t";
            _strRockets = "Rockets \t\t";

        }
        else {

            _strBullets = "Áóëåòû \t\t";
            _strRockets = "Ðîêåòû \t\t";

        }

        levelMenuManager.statLabel1.text = _strBullets + helpersGoals;
        levelMenuManager.statLabel2.text = _strRockets + enemiesGoals;
        isGoaled = false;
        whistleSound.Play();

    }

    public override void UpdateLevelInfo(){

        if (GameManager.gameOver) {

            canSetGoal = false;

            if (helpersGoals <= enemiesGoals)
            {

                GameManager.enemyWin = true;
                GameManager.playerWin = false;

            }
            else {

                GameManager.enemyWin = false;
                GameManager.playerWin = true;

              /*  Achievments.SetAchievment(Achievments.achievments[Achievments.achNumber]);
                Achievments.SetStats(Achievments.killsStatsName, gameManager.killsByFriends, Achievments.killsLeaderBoardName);
                Achievments.SetStats(Achievments.winsStatsName, 1, Achievments.winsLeaderBoardName); */

                /* ÍÅ ÒÐÎÃÀÒÜ!!! */
                if (gameManager.isLastLevel == false){

                    HashManager.WriteLevelInRegedit(gameManager.currentLevelNumber);
                    HashManager.SetCurrentLevel(gameManager.currentLevelNumber);

                }

            }

            levelMenuManager.ShowGameOverPanel();

            return;

        }

        if (canSetGoal == false) { return; }

        levelMenuManager.statLabel1.text = _strBullets + helpersGoals;
        levelMenuManager.statLabel2.text = _strRockets + enemiesGoals;

        player.GetComponent<PlayerMove>().enabled = false;

        canSetGoal = false;
        whistleSound.Play();

        Invoke(nameof(UnGoal), 3f);

    }

    void UnGoal() {
        canSetGoal = true;
        isGoaled = false;
        ball.transform.position = ballPlace.position;
        player.transform.position = GameObject.FindGameObjectsWithTag("SpawnPoint")[Random.Range(0, GameObject.FindGameObjectsWithTag("SpawnPoint").Length)].transform.position;

        player.GetComponent<PlayerMove>().enabled = true;

        whistleSound.Play();

    }

}

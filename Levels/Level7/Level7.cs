
public class Level7 : LevelManager
{

    string _strBullets;
    string _strRockets;

    // Start is called before the first frame update
    void Awake()
    {

        if (LanguageManager.currentLanguage == Language.EN)
        {

            _strBullets = "Bullets ";
            _strRockets = "Rockets ";

        }
        else
        {

            _strBullets = "Булеты ";
            _strRockets = "Рокеты ";

        }

        levelMenuManager.statLabel1.text = _strBullets + gameManager.killsByFriends + " | " + gameManager.killsToWin;
        levelMenuManager.statLabel2.text = _strRockets + gameManager.killsByEnemies + " | " + gameManager.killsToWin;
    }

    public override void UpdateLevelInfo()
    {

        if (GameManager.gameOver)
        { // Время кончилось 

            GameManager.enemyWin = true;
            GameManager.playerWin = false;

            levelMenuManager.ShowGameOverPanel();
            return;

        }

        levelMenuManager.statLabel1.text = _strBullets + gameManager.killsByFriends + " | " + gameManager.killsToWin;
        levelMenuManager.statLabel2.text = _strRockets + gameManager.killsByEnemies + " | " + gameManager.killsToWin;

        if (gameManager.killsByFriends == gameManager.killsToWin && gameManager.killsByEnemies != gameManager.killsToWin) {

            GameManager.gameOver = true;
            GameManager.playerWin = true;
            GameManager.enemyWin = false;

            /* Achievments.SetAchievment(Achievments.achievments[Achievments.achNumber]);
            Achievments.SetStats(Achievments.killsStatsName, gameManager.killsByFriends, Achievments.killsLeaderBoardName);
            Achievments.SetStats(Achievments.winsStatsName, 1, Achievments.winsLeaderBoardName); */

            if (gameManager.isLastLevel == false){

                HashManager.WriteLevelInRegedit(gameManager.currentLevelNumber);
                HashManager.SetCurrentLevel(gameManager.currentLevelNumber);

            }

            levelMenuManager.ShowGameOverPanel();

        } else if (gameManager.killsByEnemies == gameManager.killsToWin && gameManager.killsByFriends != gameManager.killsToWin) {

            GameManager.gameOver = true;
            GameManager.playerWin = false;
            GameManager.enemyWin = true;

            levelMenuManager.ShowGameOverPanel();

        }

    }
}

using UnityEngine;

public class Level5 : LevelManager{
    string _strKills;

  public bool isActivatedSoldiers;
  public bool isActivatedRobots;
  public  int killsToShowSoldiers = 50;
  public  int killsToShowRobots = 100;

    [SerializeField]
    Respawner soldiersRespawner;
    [SerializeField]
    Respawner robotsRespawner;

    void Awake(){

        if (LanguageManager.currentLanguage == Language.EN){

            _strKills = "Kills ";

        }
        else
        {

            _strKills = "Óáèòî ";

        }

        levelMenuManager.statLabel2.text = _strKills + gameManager.killsByFriends + " | " + gameManager.killsToWin;

    }

    public override void UpdateLevelInfo(){

        levelMenuManager.statLabel2.text = _strKills + gameManager.killsByFriends + " | " + gameManager.killsToWin;

        if (GameManager.gameOver == false){

            if (gameManager.killsByFriends >= gameManager.killsToWin){

                GameManager.gameOver = true;
                GameManager.playerWin = true;
                GameManager.enemyWin = false;

                /* Achievments.SetAchievment(Achievments.achievments[Achievments.achNumber]);
                Achievments.SetStats(Achievments.killsStatsName, gameManager.killsByFriends, Achievments.killsLeaderBoardName);
                Achievments.SetStats(Achievments.winsStatsName, 1, Achievments.winsLeaderBoardName); */

                /* ÍÅ ÒÐÎÃÀÒÜ!!! */
                if (gameManager.isLastLevel == false){

                    HashManager.WriteLevelInRegedit(gameManager.currentLevelNumber);
                    HashManager.SetCurrentLevel(gameManager.currentLevelNumber);

                }

                levelMenuManager.ShowGameOverPanel();

            }

        }
        else {

            GameManager.gameOver = true;
            GameManager.playerWin = false;
            GameManager.enemyWin = true;

            levelMenuManager.ShowGameOverPanel();

        }
       
    }

    private void Update(){

        if (gameManager.killsByFriends >= killsToShowSoldiers && !isActivatedSoldiers) {

            soldiersRespawner.enabled = true;
            isActivatedSoldiers = true;

        }

        if (gameManager.killsByFriends >= killsToShowRobots && !isActivatedRobots){

            robotsRespawner.enabled = true;
            isActivatedRobots = true;

        }

    }

}

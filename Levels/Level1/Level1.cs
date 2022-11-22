
public class Level1 : LevelManager{

    string _killsStr;

    void Awake() {
        if (LanguageManager.currentLanguage == Language.EN){

            _killsStr = "Kills ";

        }
        else {

            _killsStr = "”·ËÚÓ ";

        }
        
        levelMenuManager.statLabel1.text = _killsStr + gameManager.killsByFriends + " | " + gameManager.killsToWin;

    }

    public override void UpdateLevelInfo(){

        levelMenuManager.statLabel1.text = _killsStr + gameManager.killsByFriends + " | " + gameManager.killsToWin;

        if (gameManager.killsByFriends == gameManager.killsToWin){

            GameManager.playerWin = true;
            GameManager.gameOver = true;

            /*Achievments.SetStats(Achievments.killsStatsName, gameManager.killsByFriends, Achievments.killsLeaderBoardName);
            Achievments.SetStats(Achievments.winsStatsName, 1, Achievments.winsLeaderBoardName); */

            /* Õ≈ “–Œ√¿“‹!!! */
            if (gameManager.isLastLevel == false) {

                HashManager.WriteLevelInRegedit(gameManager.currentLevelNumber);
                HashManager.SetCurrentLevel(gameManager.currentLevelNumber);

            }

            levelMenuManager.ShowGameOverPanel();

        }
                
    }
   
}

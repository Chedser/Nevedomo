
public class Level10 : LevelManager{
    string _strKills;

    // Start is called before the first frame update
    void Awake(){

        if (LanguageManager.currentLanguage == Language.EN){

            _strKills = "Kills ";

        }
        else{

            _strKills = "”·ËÚÓ ";

        }

        levelMenuManager.statLabel1.text = _strKills + gameManager.killsByFriends + " | " + gameManager.killsToWin;

    }

    public override void UpdateLevelInfo(){

        if (GameManager.gameOver) {

            GameManager.playerWin = false;
            GameManager.enemyWin = true;

            levelMenuManager.ShowGameOverPanel();

            return; }

        levelMenuManager.statLabel1.text = _strKills + gameManager.killsByFriends + " | " + gameManager.killsToWin;

        if (gameManager.killsByFriends == gameManager.killsToWin){

            GameManager.playerWin = true;
            GameManager.enemyWin = false;
            GameManager.gameOver = true;

            /* Achievments.SetAchievment(Achievments.achievments[Achievments.achNumber]);
            Achievments.SetStats(Achievments.killsStatsName, gameManager.killsByFriends, Achievments.killsLeaderBoardName);
            Achievments.SetStats(Achievments.winsStatsName, 1, Achievments.winsLeaderBoardName); */

            /* Õ≈ “–Œ√¿“‹!!! */
            if (gameManager.isLastLevel == false){

                HashManager.WriteLevelInRegedit(gameManager.currentLevelNumber);
                HashManager.SetCurrentLevel(gameManager.currentLevelNumber);

            }

            levelMenuManager.ShowGameOverPanel();

        }

    }

}

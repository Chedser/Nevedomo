using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9 : LevelManager{

    [SerializeField] uint currentReds = 0;
    [SerializeField] uint redsToWin = 5;

    string _strReds;

    // Start is called before the first frame update
    void Start(){

        if (LanguageManager.currentLanguage == Language.EN){

            _strReds = "Entities ";

        }
        else{

            _strReds = "—Û˘ÌÓÒÚË ";

        }

        levelMenuManager.statLabel1.text = _strReds + currentReds + " | " + redsToWin;

    }


    public override void UpdateLevelInfo(){}

    public void UpdateLevel() {

        if (GameManager.gameOver){

            GameManager.playerWin = false;
            GameManager.enemyWin = true;

            levelMenuManager.ShowGameOverPanel();

            return;
        }

        ++currentReds;

        levelMenuManager.statLabel1.text = _strReds + currentReds + " | " + redsToWin;

        if (currentReds == redsToWin){

            GameManager.gameOver = true;
            GameManager.playerWin = true;
            GameManager.enemyWin = false;

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

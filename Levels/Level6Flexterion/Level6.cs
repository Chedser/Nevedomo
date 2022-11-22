using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6 : LevelManager
{

    public int ballsToWin = 7;
    public int currentBalls;

    GameObject[] spawnPoints;
    [SerializeField]
    GameObject[] spawnPointsNotLive;

    string _strBalls;

 
    void Awake()
    {

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        if (LanguageManager.currentLanguage == Language.EN)
        {

            _strBalls = "Balls ";

        }
        else
        {

            _strBalls = "Ãˇ˜Ë ";

        }

        levelMenuManager.statLabel2.text = _strBalls + currentBalls + " | " + ballsToWin;

    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.GetComponent<SoccerballOneGate>() && !GameManager.gameOver)
        {

            ++currentBalls;

            if (currentBalls == ballsToWin) {

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

            levelMenuManager.statLabel2.text = _strBalls + currentBalls + " | " + ballsToWin;

            Destroy(other.gameObject);

        }
        else if (other.GetComponent<BasicHealth>() || other.GetComponent<Missile>())
        {

            Teleport(true, other.gameObject);

        }
        else if (other.GetComponent<Rigidbody>().mass < 5000f)
        {

            Teleport(false, other.gameObject);

        }

    }

    void Teleport(bool isDown, GameObject go)
    {

        Vector3 pos;

        if (isDown)
        {

            pos = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

        }
        else
        {

            pos = spawnPointsNotLive[Random.Range(0, spawnPointsNotLive.Length)].transform.position;

        }


        go.transform.position = pos;

    }

    public override void UpdateLevelInfo(){

        if (GameManager.gameOver) { // ¬ÂÏˇ ÍÓÌ˜ËÎÓÒ¸ 

            GameManager.enemyWin = true;
            GameManager.playerWin = false;

            levelMenuManager.ShowGameOverPanel();
            return;

        }

       
    }
}
using UnityEngine;

public class Level8 : LevelManager{

    public uint currentBarrels = 0;
    public uint barrelsToWin = 10;

    string _strBarrels;

    GameObject[] spawnPoints;

    // Start is called before the first frame update
    void Awake(){

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        if (LanguageManager.currentLanguage == Language.EN){

            _strBarrels = "Barrels ";
            
        }
        else
        {

            _strBarrels = "¡Ó˜ÍË ";

        }

              levelMenuManager.statLabel2.text = _strBarrels + currentBarrels + " | " + barrelsToWin;
           }

    private void OnCollisionEnter(Collision collision){

        if (collision.gameObject.GetComponent<BarrelHolley>() && !collision.gameObject.GetComponent<BarrelHolley>().isCached) {

            ++currentBarrels;

            collision.gameObject.GetComponent<BarrelHolley>().isCached = true;

            Destroy(collision.gameObject);

            UpdateLevelInfo();

        } else if (collision.gameObject.GetComponent<BasicHealth>()) {

            Vector3 pos = GetRandomSpawnPoint().transform.position;

            collision.gameObject.transform.position = pos;

        }

    }

    public override void UpdateLevelInfo(){

        if (GameManager.gameOver) {

            GameManager.playerWin = false;
            GameManager.enemyWin = true;

            levelMenuManager.ShowGameOverPanel();

            return;

        }

        levelMenuManager.statLabel2.text = _strBarrels + currentBarrels + " | " + barrelsToWin;

        if (currentBarrels == barrelsToWin) {

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

    GameObject GetRandomSpawnPoint(){

        return spawnPoints[Random.Range(0, spawnPoints.Length)];

    }

}

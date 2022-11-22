using UnityEngine;

public class BlackHole : LevelManager
{
    public int barrelsToWin = 5;
    int currentBarrelsPulled;
    public float radius;
    public float force;

    string _strBarrels;

    void Awake(){

        if (LanguageManager.currentLanguage == Language.EN){

            _strBarrels = "Barrels \t";
        }
        else {

            _strBarrels = "¡Ó˜ÍË \t";

        }

        levelMenuManager.statLabel2.text = _strBarrels + currentBarrelsPulled;
    }

    public override void UpdateLevelInfo(){

        if (currentBarrelsPulled == barrelsToWin){

            GameManager.gameOver = true;
            GameManager.playerWin = true;
            GameManager.enemyWin = false;

        }

        if (GameManager.gameOver){

            if (GameManager.playerWin == true && GameManager.enemyWin == false){

               /* Achievments.SetAchievment(Achievments.achievments[Achievments.achNumber]);
                Achievments.SetStats(Achievments.killsStatsName, gameManager.killsByFriends, Achievments.killsLeaderBoardName);
                Achievments.SetStats(Achievments.winsStatsName, 1, Achievments.winsLeaderBoardName); */

                /* Õ≈ “–Œ√¿“‹!!! */
                if (gameManager.isLastLevel == false){

                    HashManager.WriteLevelInRegedit(gameManager.currentLevelNumber);
                    HashManager.SetCurrentLevel(gameManager.currentLevelNumber);

                }

            }
            else {

                GameManager.playerWin = false;
                GameManager.enemyWin = true;

            }

            levelMenuManager.ShowGameOverPanel();

        }
    }

    protected void OnDrawGizmosSelected(){
        Gizmos.color = Color.blue;
      //  Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.red;
     //   Gizmos.DrawWireSphere(transform.position, radius / 2f);
    }

    private void FixedUpdate(){
        PullRigidbody();
    }

    void PullRigidbody(){

        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < overlappedColliders.Length; i++){

            if (overlappedColliders[i].attachedRigidbody != null) {

                overlappedColliders[i].attachedRigidbody.AddExplosionForce(-force * 100, transform.position, radius);

            }
            
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.GetComponentInParent<BasicHealth>())
        {
            other.gameObject.GetComponentInParent<IDemagable>().TakeDemage(90000, HeroType.Lifeless);

            if (other.gameObject.GetComponentInParent<SpawnInfo>() &&
                other.gameObject.GetComponentInParent<SpawnInfo>().heroType != HeroType.Player)
            {

                other.gameObject.GetComponentInParent<SpawnInfo>().gameObject.SetActive(false);

            }

        }
        else {

            if (other.gameObject.GetComponent<BarrelRadiation>() && other.gameObject.GetComponent<BarrelRadiation>().canBeCatched) {

                other.gameObject.GetComponent<BarrelRadiation>().canBeCatched = false;

                ++currentBarrelsPulled;
          
                levelMenuManager.statLabel2.text = _strBarrels + currentBarrelsPulled;

                UpdateLevelInfo();
             

            }

                Destroy(other.gameObject);
         
        }
    }

}

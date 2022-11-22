using UnityEngine;

public class Level12 : LevelManager{

    [SerializeField]
    AtomicBombExploision exploision;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject playerCamera;
    [SerializeField]
    GameObject deadCamera;
    [SerializeField]
    ATMHealth atmHealth;
    [SerializeField]
    GameObject atomicBombShield;
    [SerializeField]
    HyperCubeHealth hyperCubeHealth;
    [SerializeField]
    GameObject winCamera;
    [SerializeField]
    GameObject aimImage;
    [SerializeField]
    GameObject statsContainer;
    [SerializeField]
    LoseSoundPlayer loseSoundPlayer;
    [SerializeField]
    GameObject ruSubt;
    [SerializeField]
    GameObject enSubt;
    [SerializeField]
    GameObject healthAndWeaponImages;

    public override void UpdateLevelInfo(){

        //Проигрыш
        if (GameManager.gameOver) {
            GameManager.playerWin = false;
            GameManager.enemyWin = true;
            exploision.Activate(HeroType.Enemy);
            Destroy(exploision.gameObject);
            Destroy(player);
            deadCamera.SetActive(true);
            atmHealth.TakeDemage(10000, HeroType.Enemy);
            hyperCubeHealth.TakeDemage(10000, HeroType.Enemy);
            aimImage.SetActive(false);
            loseSoundPlayer.enabled = true;

            levelMenuManager.ShowGameOverPanel();
            return; }

        //Победа
        if (hyperCubeHealth.GetCurrentHealth() <= 0) {
 
            GameManager.playerWin = true;
            GameManager.enemyWin = false;
            GameManager.gameOver = true;
            playerCamera.SetActive(false);
            winCamera.SetActive(true);
            aimImage.SetActive(false);
            atomicBombShield.SetActive(false);
            statsContainer.SetActive(false);
            healthAndWeaponImages.SetActive(false);

            if (LanguageManager.currentLanguage == Language.EN){

                enSubt.SetActive(true);

            }
            else{

                ruSubt.SetActive(true);

            }

            /* Achievments.SetAchievment(Achievments.achievments[Achievments.achNumber]);
            Achievments.SetStats(Achievments.killsStatsName, gameManager.killsByFriends, Achievments.killsLeaderBoardName);
            Achievments.SetStats(Achievments.winsStatsName, 1, Achievments.winsLeaderBoardName); */

        }

    }
}

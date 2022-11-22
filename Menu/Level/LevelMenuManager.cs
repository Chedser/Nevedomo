using UnityEngine;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour{
    [SerializeField] public Text statLabel1;
    [SerializeField] public Text statLabel2;
    [SerializeField] public Text statLabel3;
    [SerializeField] public GameObject playerWinPanel;
    [SerializeField] public GameObject playerLosePanel;

    public void ShowGameOverPanel(){

        if (GameManager.gameOver == false) { return; }

        if (GameManager.playerWin) {

            playerWinPanel.SetActive(true);
            playerLosePanel.SetActive(false);

        } else if (GameManager.enemyWin) {

            playerWinPanel.SetActive(false);
            playerLosePanel.SetActive(true);

        }

    }

}

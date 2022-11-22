
using UnityEngine;

public class EnemyStats : Stats{

    public override void SetKills(HeroType killed){
 
        if (GameManager.gameOver) { return; }

        if (killed == HeroType.Player || killed == HeroType.Helper) {

            ++kills;
   
            ++gameManager.killsByEnemies;
         
            gameManager.UpdateTopLabels();

        }

    }
   
}

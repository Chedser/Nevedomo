using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsFootball : EnemyStats{
    public override void SetKills(HeroType killed){

        if (GameManager.gameOver) { return; }

        if (killed == HeroType.Player || killed == HeroType.Helper){

            ++kills;
            ++gameManager.killsByEnemies;
  
        }

    }
}

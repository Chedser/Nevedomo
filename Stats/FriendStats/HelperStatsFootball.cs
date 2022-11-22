using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperStatsFootball : HelperStats{
    public override void SetKills(HeroType killed){

        if (killed == HeroType.Enemy){

            ++kills;
            ++gameManager.killsByFriends;
 
        }

    }
}

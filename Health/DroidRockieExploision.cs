using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidRockieExploision : RobotExploision
{
    public new void Activate(HeroType heroType){

        if (isDone) { return; }

        bombActivator = heroType;
        bombActivatorGo = this.gameObject;

        Use();

    }

  }

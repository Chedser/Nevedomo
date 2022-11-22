using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballGateCollision : MonoBehaviour{

    [SerializeField] Level2 level2;

    public enum GateOwner {Helpers, Enemies }

    public GateOwner gateOwner = GateOwner.Helpers;

    private void OnTriggerEnter(Collider other){

        if (GameManager.gameOver || Level2.isGoaled) {
            return;
        }

        if (other.gameObject.GetComponent<FootballCollision>()) {

            if (gateOwner == GateOwner.Helpers){

                ++level2.enemiesGoals;

            }
            else {

                ++level2.helpersGoals;

            }

            level2.UpdateLevelInfo();
            Level2.isGoaled = true;

        }

    }

}

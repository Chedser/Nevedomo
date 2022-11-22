using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBallChecker : MonoBehaviour
{
    [SerializeField] Transform soccerBallPlace;
    [SerializeField] Transform ground;

    // Update is called once per frame
    void Update()
    {

        if (this.transform.position.y < ground.position.y) {

            this.transform.position = soccerBallPlace.position;

        }

    }
}

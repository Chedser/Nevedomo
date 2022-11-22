using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthCheckerFootbal : MonoBehaviour
{

    [SerializeField] GameObject ball;
    [SerializeField] PlayerHealth playerHealth; 

    // Update is called once per frame
    void Update()
    {

        if (playerHealth.currentHealth <= 0 && ball.GetComponent<FootballCollision>().ballOwner == HeroType.Player) {

            ball.GetComponent<FootballCollision>().ballOwner = HeroType.Lifeless;

        }

    }
}

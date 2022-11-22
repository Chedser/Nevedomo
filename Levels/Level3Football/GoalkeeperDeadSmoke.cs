using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperDeadSmoke : MonoBehaviour
{
    [SerializeField] BasicHealth health;
    [SerializeField] ParticleSystem smoke;
    public float healthToSmoke = 1000f;
    bool isPlaying;

    // Update is called once per frame
    void Update()
    {

        if (isPlaying == false && health.currentHealth <= healthToSmoke) {

            smoke.Play();
            isPlaying = true;

        }

    }
}

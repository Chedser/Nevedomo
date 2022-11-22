using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseSoundPlayer : MonoBehaviour{

    [SerializeField]
    AudioSource loseSound;
    bool isPlayed;
    float time = 1.5f;

    // Update is called once per frame
    void Update(){

        time -= Time.deltaTime;

        if (time <= 0 && !loseSound.isPlaying && !isPlayed) {

            loseSound.Play();
            isPlayed = true;

        }

 
    }
}

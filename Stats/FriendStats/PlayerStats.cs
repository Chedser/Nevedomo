using UnityEngine;

public class PlayerStats : FriendStats{

    [SerializeField]
    AudioSource[] speachAudio;

    void PlaySpeach() {

        bool nothingPlaying = true;

        foreach (AudioSource audio in speachAudio) {

            if (audio.isPlaying) {
                nothingPlaying = false;
            }

        }

        if (nothingPlaying) {

            speachAudio[Random.Range(0, speachAudio.Length)].Play();

        }

    }

    public override void SetKills(HeroType killed){

        if (GameManager.gameOver) { return; }

        if (killed == HeroType.Enemy){
         
            ++kills;

            ++gameManager.killsByFriends;

            gameManager.UpdateTopLabels();
            PlaySpeach();

        }

    }
}

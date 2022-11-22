using UnityEngine;

public class MusicManager : MonoBehaviour{

    [SerializeField] AudioSource music;
    [SerializeField] static bool isPlaying = true;
  
    public void ChangeMusicVolume() {

        if (GameManager.isPaused){

            music.volume = 0.2f;

        }
        else {

            music.volume = 0.5f;

        }

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.C)) {

            if (isPlaying)
            {

                music.Pause();
                isPlaying = false;
                
            }
            else {

                music.UnPause();
                isPlaying = true;

            }

        }

        if (!music.isPlaying && isPlaying) {
      
            music.Play();

        }
    }

}

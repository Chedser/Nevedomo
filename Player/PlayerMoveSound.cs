using UnityEngine;

public class PlayerMoveSound : MonoBehaviour
{

    [SerializeField] AudioSource idleSound;
    [SerializeField] AudioSource moveSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0){

            if (moveSound.isPlaying){

                moveSound.Stop();

            }

            if (!idleSound.isPlaying) {

                idleSound.Play();

            }

        }
        else {

            if (!moveSound.isPlaying){

                moveSound.Play();

            }

            if (idleSound.isPlaying){

                idleSound.Stop();

            }

        }

    }

    void TriggerSound() { 
    
    }

}

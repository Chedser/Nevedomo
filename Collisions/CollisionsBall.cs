using UnityEngine;

public class CollisionsBall : MonoBehaviour{
    [SerializeField] AudioSource ballSound;

    private void OnCollisionEnter(Collision collision){

        ballSound.Play();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballCollision : MonoBehaviour{

    public HeroType ballOwner = HeroType.Lifeless;
    public GameObject ballOwnerGo;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision){

        if (collision.gameObject.GetComponent<SpawnInfo>() && collision.gameObject.GetComponent<SpawnInfo>().heroType == HeroType.Player) {

            ballOwnerGo = collision.gameObject;
            ballOwner = HeroType.Player;


        }

    }

    public void DropBall() {

        this.transform.SetParent(null);
        this.GetComponent<Rigidbody>().isKinematic = false;
        ballOwner = HeroType.Lifeless;
        ballOwnerGo = null;

    }
    

}

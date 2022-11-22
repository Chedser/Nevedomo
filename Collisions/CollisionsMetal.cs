using UnityEngine;

public class CollisionsMetal : Collisions{
    
    public void OnCollisionEnter(Collision collision){

        if (collision.relativeVelocity.magnitude > 2 &&
            !collision.collider.gameObject.GetComponent<SpawnInfo>())

            Instantiate(metalCol, collision.contacts[0].point, Quaternion.identity);
    }

}



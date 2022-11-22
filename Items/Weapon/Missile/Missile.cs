using UnityEngine;

public abstract class Missile:Explosive {

    public float speed;
    protected Vector3 dir;
    public HeroType owner;
    public Rigidbody rb;
    
    public void MoveTo(Vector3 dir) {
            
            rb.AddForce(dir * speed, ForceMode.Impulse);
           

    }

    public void MoveToKinematic(Vector3 dir) {

        this.dir = dir;

    }

    public void ChangeAISpawnPoint(Collider collider){

        if (collider.gameObject.GetComponent<AIMoveable>()){
            collider.gameObject.GetComponent<AIMoveable>().awakeFromMissile = true;
        }

    }

}

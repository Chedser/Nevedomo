using UnityEngine;

public enum SpawnPoint { SpawnPoint, SpawnPointEnemy }

public abstract class BasicHealth : MonoBehaviour{

   public float currentHealth;

    public float minHealth;
    public float maxHealth;
    public float initHealth;
    protected float currentHash;

    [SerializeField] protected AudioSource idleAudio;
    [SerializeField] protected AudioSource deadSound;
    [SerializeField] protected AudioSource demageSound;


    public SpawnPoint spawnPoint = SpawnPoint.SpawnPoint;

    protected abstract void Die();

    protected  Vector3 GetRandomSpawnPoint(SpawnPoint sp){

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(sp.ToString());

        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
    }

    protected abstract void Respawn();
  
    protected void PlayDemageSound() {
        
        if (demageSound != null && !demageSound.isPlaying) {

            demageSound.Play();

        }
    
    }

    protected virtual void PlayDeadSound(){

        if (deadSound != null && !deadSound.isPlaying){

            deadSound.Play();

        }

    }



}

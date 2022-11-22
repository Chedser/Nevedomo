using UnityEngine;

public class SpawnHealth : BasicHealth, IDemagable{

    [SerializeField]
  protected  Animator animator;
    [SerializeField]
  protected  CapsuleCollider[] colliders;
   protected CapsuleCollider lifeCollider;
  protected   CapsuleCollider deadCollider;

    [SerializeField]
  protected  GameObject deadEffect;

    public bool isRobot;

    void Awake(){

        initHealth = InitHealth();

        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

        if (colliders.Length == 2) {

            lifeCollider = colliders[0];
            deadCollider = colliders[1];

        }

    }

    public void TakeDemage(int demage, HeroType kicker){

        if (currentHash != HashManager.GetHashHealth(currentHealth, initHealth)){
            GameManager.gameOver = true;
            GameManager.enemyWin = true;
            return;

        }

        if (currentHealth <= 0) { return; }

        currentHealth -= demage;

        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

        if (currentHealth <= 0){

            currentHealth = 0;

            currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

            Die();

            if (GameManager.gameOver == false) {

                Invoke(nameof(Respawn), 3.0f);

            }
        

        }
        else{

            PlayDemageSound();

        }
    }

    protected override void Die(){

        if (!isRobot) {
            animator.SetBool("isDead", true);
            TriggerColliders();
        }
               
        if (deadSound != null) {

            if (idleAudio != null) {
                idleAudio.Stop();
            }

            if (demageSound != null) {
                demageSound.Stop();
            }

            deadSound.Play();

        }

    }

    public float InitHealth(){

        return Random.Range(minHealth, maxHealth);

    }

  protected override  void Respawn() {
        if (deadEffect != null) { Instantiate(deadEffect, transform.position, Quaternion.identity); }
        transform.position = GetRandomSpawnPoint(spawnPoint);
        initHealth = InitHealth();
        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

        if (!isRobot) {
            animator.SetBool("isDead", false);
            TriggerColliders(false);
        }
      

        this.gameObject.SetActive(false);

    }

    public float GetCurrentHealth(){
        return currentHealth;
    }

  protected  void TriggerColliders(bool isDead = true){

        if (colliders.Length == 0){
            return;

        }

        lifeCollider.enabled = !isDead;
        deadCollider.enabled = isDead;

    }
}

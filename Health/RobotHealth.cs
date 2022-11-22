using UnityEngine;

public class RobotHealth : SpawnHealth, IDemagable{

    [SerializeField]
    RobotExploision robotExploision;

    void Awake(){

        initHealth = InitHealth();

        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

        if (colliders.Length == 2)
        {

            lifeCollider = colliders[0];
            deadCollider = colliders[1];

        }

    }

    new public void TakeDemage(int demage, HeroType kicker){

        if (currentHash != HashManager.GetHashHealth(currentHealth, initHealth))
        {
            GameManager.gameOver = true;
            GameManager.enemyWin = true;
            return;

        }

        if (currentHealth <= 0) { return; }

        currentHealth -= demage;

        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

        if (currentHealth <= 0)
        {

            currentHealth = 0;

            currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

            Die(kicker);

            if (GameManager.gameOver == false)
            {

                Invoke(nameof(Respawn), 3.0f);

            }


        }

    }

  void Die(HeroType kicker){

        animator.SetBool("isDead", true);
        robotExploision.Activate(kicker);
        TriggerColliders();

    }

    protected override void Respawn(){
        transform.position = GetRandomSpawnPoint(spawnPoint);
        initHealth = InitHealth();
        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

            animator.SetBool("isDead", false);
            TriggerColliders(false);

        this.gameObject.SetActive(false);

    }

}

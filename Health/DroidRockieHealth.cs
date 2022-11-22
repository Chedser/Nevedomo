using UnityEngine;

public class DroidRockieHealth : SpawnHealth, IDemagable
{
    [SerializeField] DroidRockieExploision droidRockieExploision;

    void Awake(){

        initHealth = InitHealth();

        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

    }

    public new void TakeDemage(int demage, HeroType kicker)
    {

        if (currentHash != HashManager.GetHashHealth(currentHealth, initHealth))
        {
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

            droidRockieExploision.Activate(kicker);
            if (deadEffect != null) { Instantiate(deadEffect, transform.position, Quaternion.identity); }
            this.gameObject.SetActive(false);

            if (GameManager.gameOver == false){

                Invoke(nameof(Respawn), 3.0f);

            }


        }
        else
        {

            PlayDemageSound();

        }
    }


    protected override void Respawn()
    {
        
        transform.position = GetRandomSpawnPoint(spawnPoint);
        initHealth = InitHealth();
        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

    }

    private void OnCollisionEnter(Collision collision){

        if (collision.gameObject.GetComponentInParent<BasicHealth>()){ // Столкнулись с живым


            TakeDemage(100, collision.gameObject.GetComponentInParent<SpawnInfo>().heroType);

        }
        else
        {

            TakeDemage(100, HeroType.Lifeless);

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperRocketHealth : BasicHealth, IDemagable
{

    [SerializeField]
    GameObject deadEffect;


    void Awake()
    {

        initHealth = InitHealth();

        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

    }

    public void TakeDemage(int demage, HeroType kicker)
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

        if (currentHealth <= 0)
        {

            currentHealth = 0;

            currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

        }
   }

    protected override void Die()
    {


    }



    public float InitHealth()
    {

        return Random.Range(minHealth, maxHealth);

    }

    protected override void Respawn()
    {
       
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    void TriggerColliders(bool isDead = true)
    {

    }
}

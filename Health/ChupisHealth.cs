using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChupisHealth : BasicHealth, IDemagable
{

    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected CapsuleCollider[] colliders;
    protected CapsuleCollider lifeCollider;
    protected CapsuleCollider deadCollider;

    [SerializeField]
    protected GameObject deadEffect;
    [SerializeField]
    Respawner respawner;
    [SerializeField]
    MusicManager musicManager;
    [SerializeField]
    TimeManager timeManager;

    public static bool isDead;

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float InitHealth(){

        return Random.Range(minHealth, maxHealth);

    }


    public void TakeDemage(int demage, HeroType kicker){

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

                Die();
                         
            }
          
        }

    protected void TriggerColliders(bool isDead = true)
    {

        if (colliders.Length == 0)
        {
            return;

        }

        lifeCollider.enabled = !isDead;
        deadCollider.enabled = isDead;

    }

    protected override void Die()
    {
        animator.SetBool("isDead", true);
        TriggerColliders();

    
            if (idleAudio != null)
            {
                idleAudio.Stop();
            }

            if (demageSound != null)
            {
                demageSound.Stop();
            }
    

        Invoke(nameof(Respawn), 3.0f);

    }

    protected override void Respawn(){
        
        if (deadEffect != null) { Instantiate(deadEffect, transform.position, Quaternion.identity); }

        respawner.enabled = true;
        musicManager.enabled = true;
        timeManager.enabled = true;

        isDead = true;

        this.gameObject.SetActive(false);

    }

    // Start is called before the first frame update
    void Awake()
    {
        initHealth = InitHealth();

        isDead = false;

        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

        if (colliders.Length == 2)
        {

            lifeCollider = colliders[0];
            deadCollider = colliders[1];

        }
    }
     
}

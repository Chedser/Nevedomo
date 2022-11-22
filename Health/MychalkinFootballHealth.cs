using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MychalkinFootballHealth : BasicHealth, IDemagable
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    CapsuleCollider[] colliders;
    CapsuleCollider lifeCollider;
    CapsuleCollider deadCollider;
    [SerializeField] Transform mychalkinPlace;

    [SerializeField]
    GameObject deadEffect;

    void Awake()
    {

        initHealth = InitHealth();

        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

        if (colliders.Length == 2)
        {

            lifeCollider = colliders[0];
            deadCollider = colliders[1];

        }

        idleAudio.Play();

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

            Die();

            if (GameManager.gameOver == false)
            {

                Invoke(nameof(Respawn), 3.0f);

            }


        }
        else
        {

            PlayDemageSound();

        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float InitHealth(){
        return Random.Range(minHealth, maxHealth);
    }

    protected override void Die(){
        animator.SetBool("isDead", true);
        TriggerColliders();

        if (idleAudio.isPlaying) {

            idleAudio.Stop();
        }

        deadSound.Play();

    }

    protected override void Respawn(){
        if (deadEffect != null) { Instantiate(deadEffect, transform.position, Quaternion.identity); }
        transform.position = mychalkinPlace.position;
        initHealth = InitHealth();
        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);
        animator.SetBool("isDead", false);

        TriggerColliders(false);

        this.gameObject.SetActive(false);
    }

    void TriggerColliders(bool isDead = true){

        if (colliders.Length == 0){
            return;

        }

        lifeCollider.enabled = !isDead;
        deadCollider.enabled = isDead;

    }

}

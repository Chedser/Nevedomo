using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PahomHeaderHealth : BasicHealth, IDemagable
{

    [SerializeField]
    protected Animator animator;

    void Start() {

        initHealth = InitHealth();

        currentHealth = initHealth;

    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float InitHealth()
    {
        return Random.Range(minHealth, maxHealth);
    }

    public void TakeDemage(int demage, HeroType kicker){
     

        if (currentHealth <= 0) { return; }

        currentHealth -= demage;

        if (currentHealth <= 0)
        {

            currentHealth = 0;

            Die();

        }

    }

    protected override void Die(){

        animator.SetBool("isDead", true);

        if (deadSound != null)
        {

            if (idleAudio != null)
            {
                idleAudio.Stop();
            }

            if (demageSound != null)
            {
                demageSound.Stop();
            }

            deadSound.Play();

        }
    }

    protected override void Respawn()
    {
        throw new System.NotImplementedException();
    }


}

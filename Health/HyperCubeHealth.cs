using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HyperCubeHealth : BasicHealth, IDemagable
{

    [SerializeField]
    ATMHealth ATMHealth;
    [SerializeField]
    NavMeshAgent navmeshAgent;
    [SerializeField]
    ParticleSystem deadEffect;
    [SerializeField]
    RawImage cubeIcon;
    [SerializeField]
    Level12 level12;

    Color color = Color.green;

    void Awake(){

        initHealth = InitHealth();
        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);
        cubeIcon.color = color;

    }

    public Color Lerp3(Color a, Color b, Color c, float t){
        if (t < 0.5f) // 0.0 to 0.5 goes to a -> b
            return Color.Lerp(a, b, t / 0.5f);
        else // 0.5 to 1.0 goes to b -> c
            return Color.Lerp(b, c, (t - 0.5f) / 0.5f);
    }

    void ChangeHealthIconColor(float currentHealth, float initHealth){

        if (currentHealth <= 0) { 
            cubeIcon.enabled = false;
            return;
        }

        color = Lerp3(Color.red, Color.blue, Color.green, currentHealth / initHealth);

        cubeIcon.color = color;

    }
    public float GetCurrentHealth(){
        return currentHealth;
    }

    public float InitHealth(){

        return Random.Range(minHealth, maxHealth);

    }

    public void TakeDemage(int demage, HeroType kicker){

        if (ATMHealth.GetCurrentHealth() > 0) { return; }

        if (currentHash != HashManager.GetHashHealth(currentHealth, initHealth)){
            GameManager.gameOver = true;
            GameManager.playerWin = false;
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

        ChangeHealthIconColor(currentHealth, initHealth);

    }

    protected override void Die(){
        navmeshAgent.enabled = false;
        deadEffect.Play();
        level12.UpdateLevelInfo();
    }

    protected override void Respawn()
{
        throw new System.NotImplementedException();
    }

}

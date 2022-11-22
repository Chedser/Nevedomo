using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ATMHealth : BasicHealth, IDemagable{

    [SerializeField]
    ParticleSystem deadEffect;
    [SerializeField]
    GameObject hyperCube;
    [SerializeField]
    NavMeshAgent navmeshAgent;
    [SerializeField]
    AI ai;

    [SerializeField]
    RawImage atmIcon;

    Color color = Color.green;

    void Awake(){
        initHealth = InitHealth();

        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);
        atmIcon.color = color;
    }

    public float GetCurrentHealth(){
        return currentHealth;
    }

    public float InitHealth(){
        return Random.Range(minHealth, maxHealth);
    }

    public Color Lerp3(Color a, Color b, Color c, float t){
        if (t < 0.5f) // 0.0 to 0.5 goes to a -> b
            return Color.Lerp(a, b, t / 0.5f);
        else // 0.5 to 1.0 goes to b -> c
            return Color.Lerp(b, c, (t - 0.5f) / 0.5f);
    }

    void ChangeHealthIconColor(float currentHealth, float initHealth){

        if (currentHealth <= 0){
            atmIcon.enabled = false;
            return;
        }

        color = Lerp3(Color.red, Color.blue, Color.green, currentHealth / initHealth);

        atmIcon.color = color;

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

        }

        ChangeHealthIconColor(currentHealth, initHealth);

    }

    protected override void Die(){
        deadEffect.Play();
        idleAudio.Stop();
        deadSound.Play();
        hyperCube.GetComponents<Collider>()[1].enabled = false;
        hyperCube.transform.GetChild(0).gameObject.SetActive(false);
        this.GetComponent<Rigidbody>().freezeRotation = false;
        ai.enabled = false;
        navmeshAgent.enabled = false;

    }

    protected override void Respawn()
    {
        throw new System.NotImplementedException();
    }


 
}

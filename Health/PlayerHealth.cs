using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : BasicHealth, IDemagable{

    RawImage healthIcon;
    RawImage cherepIcon;

    int maxHealthMedkit = 5000;

    Color color = Color.green;

    [SerializeField]
    AudioSource[] speachAudio;
    [SerializeField] AudioSource respawnSound;
    [SerializeField]
    ParticleSystem deadEffect;

    private void Awake(){

        healthIcon = GameObject.Find("HealthImage").GetComponent<RawImage>();
        cherepIcon = GameObject.Find("CherepImage").GetComponent<RawImage>();
        healthIcon.color = color;

        initHealth = InitHealth();

        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);
      
    }

    public Color Lerp3(Color a, Color b, Color c, float t){
        if (t < 0.5f) // 0.0 to 0.5 goes to a -> b
            return Color.Lerp(a, b, t / 0.5f);
        else // 0.5 to 1.0 goes to b -> c
            return Color.Lerp(b, c, (t - 0.5f) / 0.5f);
    }

    void ChangeHealthIconColor(float currentHealth, float initHealth) {

        color = Lerp3(Color.red, Color.blue, Color.green, currentHealth / initHealth);

        healthIcon.color = color;

    }

    public void TakeDemage(int demage, HeroType kicker) {

        if (currentHash != HashManager.GetHashHealth(currentHealth, initHealth)) {

            currentHealth = 0f;
            currentHash = HashManager.GetHashHealth(currentHealth, initHealth);
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

              Invoke(nameof(Respawn), 3.0f);

        }
        else{

            PlayDemageSound();

        }

        ChangeHealthIconColor(currentHealth, initHealth);

    }

    public void TakeHealth(int health) {

        if (currentHash != HashManager.GetHashHealth(currentHealth, initHealth))
        {

            currentHealth = 0f;
            currentHash = HashManager.GetHashHealth(currentHealth, initHealth);
            GameManager.gameOver = true;
            GameManager.enemyWin = true;
            return;

        }

        if (currentHealth <= 0) { return; }

        if (currentHealth < maxHealthMedkit)
        {

            currentHealth += health;

        }
        else {

            currentHealth = maxHealth;

        }

        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);

        ChangeHealthIconColor(currentHealth, initHealth);

    }

    protected override void Die(){
        healthIcon.enabled = false;
        cherepIcon.enabled = true;
        deadEffect.Play();
        PlayDeadSound();
    }

    protected override void Respawn(){
        initHealth = InitHealth();
        currentHealth = initHealth;
        currentHash = HashManager.GetHashHealth(currentHealth, initHealth);
        transform.position = GetRandomSpawnPoint(spawnPoint);

        healthIcon.enabled = true;
        healthIcon.color = Color.green;
        cherepIcon.enabled = false;
        deadEffect.Stop();

        PlayRespawnSound();

    }

    public float GetCurrentHealth(){
        return currentHealth;
    }

    protected override void PlayDeadSound() {

        foreach (AudioSource audio in speachAudio){

            if (audio.isPlaying){
                audio.Stop();
            }

        }

        if (deadSound != null && !deadSound.isPlaying){

            deadSound.Play();

        }

    }

    void PlayRespawnSound(){

        foreach (AudioSource audio in speachAudio)
        {

            if (audio.isPlaying)
            {
                audio.Stop();
            }

        }

        if (respawnSound != null && !respawnSound.isPlaying)
        {

            respawnSound.Play();

        }

    }

    public float InitHealth(){

        return Random.Range(minHealth, maxHealth);

    }

}



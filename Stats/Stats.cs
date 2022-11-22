using UnityEngine;

public abstract class Stats : MonoBehaviour{

    public int kills;


    [SerializeField] protected SpawnInfo spawnInfo;
    protected GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("LevelScripts").GetComponent<GameManager>();
    }

    public abstract void SetKills(HeroType killed);

}

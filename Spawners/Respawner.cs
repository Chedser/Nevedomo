using UnityEngine;

public class Respawner : MonoBehaviour{

    [SerializeField] GameObject[] spawns;
    public float spawnTime;
    
    // Update is called once per frame
    void FixedUpdate(){

        if (GameManager.gameOver || spawns.Length == 0) { return; }

        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0) {

            foreach (GameObject go in spawns){

                if (!go.activeSelf) {

                    go.SetActive(true);

                }

            }

            spawnTime = 3f;

        }


    }
  
}

using UnityEngine;

public class MychalkinRespawner : MonoBehaviour
{

    [SerializeField] GameObject mychalkin;
    public float spawnTime;

    // Update is called once per frame
    void Update(){

        if (GameManager.gameOver) {
            return;
        }

        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0){

                if (!mychalkin.activeSelf){

                mychalkin.SetActive(true);

                }

            spawnTime = 3f;

            }

        }
    }

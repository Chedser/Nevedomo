using UnityEngine;

public class MychalkinRespawnerFree : MonoBehaviour{
    [SerializeField] GameObject mychalkin;
    public float spawnTime;

    // Update is called once per frame
    void Update(){

        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0){

            if (!mychalkin.activeSelf){

                mychalkin.SetActive(true);

            }

            spawnTime = 3f;

        }

    }

}

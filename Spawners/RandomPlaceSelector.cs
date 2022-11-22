using UnityEngine;

public class RandomPlaceSelector : MonoBehaviour{

    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start(){

        player.transform.position = GetRandomSpawnPoint();

    }

    Vector3 GetRandomSpawnPoint(){

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
    }

}

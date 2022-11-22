using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCatcher : MonoBehaviour
{

    GameObject[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SoccerballOneGate>()) {

            Vector3  pos = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

            other.gameObject.transform.position = pos;
              
        }
    }
}

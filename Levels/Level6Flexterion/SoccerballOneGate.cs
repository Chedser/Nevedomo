using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerballOneGate : MonoBehaviour
{
    [SerializeField]
    GameObject ground;
    [SerializeField]
    GameObject ceil;

    GameObject[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {

        if (this.gameObject.transform.position.y <= ground.gameObject.transform.position.y ||
            this.gameObject.transform.position.y >= ceil.gameObject.transform.position.y) { 
        
            this.gameObject.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

            Debug.Log("Баг с мячом");

        }
        
    }
}

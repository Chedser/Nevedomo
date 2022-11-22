using UnityEngine;

public class TeleportCylinder : MonoBehaviour
{

    GameObject[] spawnPoints;
    [SerializeField] Level9 level9;
    bool isDone;

    // Start is called before the first frame update
    void Start(){

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

    }

    private void OnTriggerEnter(Collider other){

        if (other.GetComponentInParent<Rigidbody>().mass < 5000f) { // Телепортируем 

            Teleport(other);

        } else if (other.GetComponentInParent<PlayerHealth>() && !isDone) {

            isDone = true;

            Teleport(other);
            level9.UpdateLevel();
            Destroy(this.gameObject);

        }
    }

    void Teleport(Collider other) {

        Vector3 pos;

        pos = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

        other.gameObject.transform.position = pos;

    }

}

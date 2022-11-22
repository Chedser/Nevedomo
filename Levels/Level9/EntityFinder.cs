using UnityEngine;
using UnityEngine.UI;

public class EntityFinder : MonoBehaviour{
    
    [SerializeField] Text entityDistanceTxt;
    GameObject[] entities;

    [SerializeField] float time = 0.3f;
    float currentTime;

    Color color = Color.red;

    // Start is called before the first frame update
    void Awake(){

        currentTime = time;

    }

    // Update is called once per frame
    void Update(){

        currentTime -= Time.deltaTime;

        if (currentTime <= 0 && !GameManager.gameOver) {

            entities = GameObject.FindGameObjectsWithTag("Entity");

            if (entities.Length == 0) {return; }

            int distance = GetNearestDistance();

            entityDistanceTxt.text = distance.ToString();

            if (distance <= 100){

                color = Color.green;

            }
            else if (distance > 100 && distance <= 200){

                color = Color.yellow;

            }
            else {

                color = Color.red;

            }

            entityDistanceTxt.color = color;

            currentTime = time;
        }

    }

    int GetNearestDistance() {

        float nearestDistance = Vector3.Distance(this.transform.position, entities[0].transform.position);

        foreach (GameObject go in entities) {

            if (Vector3.Distance(this.transform.position, go.transform.position) < nearestDistance) {

                nearestDistance = Vector3.Distance(this.transform.position, go.transform.position);

            }

        }

        return (int)nearestDistance;
    
    }

}

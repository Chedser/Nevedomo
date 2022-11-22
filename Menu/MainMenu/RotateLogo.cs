using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLogo : MonoBehaviour{

    [SerializeField] GameObject logo;
    public float speed;

    // Update is called once per frame
    void Update(){

        logo.transform.Rotate(new Vector3(0f, 0f, 1f) * Time.deltaTime * speed);

    }
}

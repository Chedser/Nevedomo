using UnityEngine;

public abstract class Shoot : MonoBehaviour{

     protected Transform shootPlace;
    [SerializeField] protected GameObject missile;

    private void Awake(){
        shootPlace = this.transform;
    }

}

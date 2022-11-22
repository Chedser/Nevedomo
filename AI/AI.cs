using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State {Searching, Atacking, MoveToAim }
[RequireComponent(typeof(IDemagable))]
public abstract class AI : MonoBehaviour {

    private bool aimDetected;
    public float shootTimeInit;
    protected float shootTime;

    public float searchDistance;
    public float shootDistance;
    public float viewAngle;
    public GameObject aim;
    public State state = State.Searching;
    public bool canBeAwaked = true;

    public IDemagable health;
    [SerializeField] protected Transform eyePlace;
    [SerializeField] protected Transform shootPlace;
    [SerializeField] protected ParticleSystem shootEffect;
    [SerializeField] protected AudioSource shootSound;
    [SerializeField] protected SpawnInfo spawnInfo;

    [SerializeField]
    protected Transform[] rays;

    private void Awake(){

        health = GetComponent<IDemagable>();
        shootTime = shootTimeInit;

    }

    protected abstract bool AimDetected(GameObject aim);
    protected abstract bool AimDetected();
    protected bool CanSee(GameObject aim) {

        RaycastHit hit;

        bool canSee = false;

            if (Physics.Raycast(eyePlace.position,eyePlace.forward * searchDistance, out hit, searchDistance)){

                if (hit.collider.transform == aim.transform){
                 
                    Debug.DrawLine(eyePlace.position, eyePlace.position + eyePlace.forward * searchDistance, Color.green);
                    canSee = true;

                }

            }

        return canSee;

    }

    protected void DrawViewState() {

        for (int i = 0; i < rays.Length; i++) {

            Debug.DrawLine(rays[i].position, rays[i].position + rays[i].forward * searchDistance, Color.yellow);
            
        }
       
    }
    
}

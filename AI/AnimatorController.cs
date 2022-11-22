using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(IDemagable))]
public class AnimatorController : MonoBehaviour{

    [SerializeField]
    NavMeshAgent navmeshAgent;
    [SerializeField]
    Animator animator;
    [SerializeField]
    IDemagable health;

    // Start is called before the first frame update
    void Awake(){

        health = GetComponent<IDemagable>();
        
    }

    // Update is called once per frame
    void Update(){

        if (animator.GetBool("isDead")) {
            return;
        }
       

        if (navmeshAgent.velocity.magnitude <= 0.1f){

            animator.SetBool("isRunning", false);

        }
        else {
            animator.SetBool("isRunning", true);
        }
    }
}

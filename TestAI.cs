using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject aim;

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(aim.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = aim.transform.position;
    }
}

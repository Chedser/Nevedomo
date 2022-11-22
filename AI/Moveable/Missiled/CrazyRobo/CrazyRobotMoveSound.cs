using UnityEngine;
using UnityEngine.AI;

public class CrazyRobotMoveSound : MonoBehaviour
{

    [SerializeField] IDemagable health;
    [SerializeField] AudioSource moveSound;
    [SerializeField] NavMeshAgent navmeshAgent;

    // Start is called before the first frame update
    void Start()
    {

        health = GetComponent<IDemagable>();

    }

    // Update is called once per frame
    void Update()
    {

        if (navmeshAgent.velocity.magnitude >= 0.1f && health.GetCurrentHealth() > 0)
        {

            if (!moveSound.isPlaying)
            {

                moveSound.Play();

            }

        }
        else {

            moveSound.Stop();

        }


    }
}

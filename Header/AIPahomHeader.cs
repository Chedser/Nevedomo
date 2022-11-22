using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIPahomHeader : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent navmeshAgent;
    [SerializeField]
    GameObject box;
    [SerializeField]
    GameObject ealogo;

    [SerializeField]
    AudioSource achotamSound;

    [SerializeField]
    AudioSource hahaSound;

    [SerializeField]
    AudioSource stepsSound;

    bool _isPlayedAchotamSound;
    bool _isPressedEnter;

    IDemagable health;

    // Start is called before the first frame update
    void Start(){

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Invoke(nameof(ShowLogo), 2.0f);

        health = GetComponent<IDemagable>();

        navmeshAgent.SetDestination(box.transform.position);

    }

    // Update is called once per frame
    void Update(){

        if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) &&
            
            !_isPressedEnter) {

            _isPressedEnter = true;

            LoadScene();

            navmeshAgent.speed = 0;

            return;

        }

        if (box == null) { return; }

        if (health.GetCurrentHealth() > 0 && navmeshAgent.velocity.magnitude > 0)
        {

            if (!stepsSound.isPlaying)
            {

                stepsSound.Play();

            }

        }
        else {

            stepsSound.Stop();

        }

        if (Mathf.Abs((navmeshAgent.transform.position - box.transform.position).magnitude) <= 2 && !_isPlayedAchotamSound) {

            achotamSound.Play();

            box.GetComponent<IExplodable>().Activate(HeroType.Player, box);

            Invoke(nameof(PlayHaha), 1.5f);

            _isPlayedAchotamSound = true;

        }

    }

    void PlayHaha() {

        hahaSound.Play();

        Invoke(nameof(LoadScene), 4.0f);

    }

    void ShowLogo() {

        ealogo.SetActive(true);

    }

    void LoadScene() {

        SceneManager.LoadSceneAsync("MainMenu");

    }

}

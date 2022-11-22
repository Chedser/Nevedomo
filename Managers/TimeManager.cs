using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Text timerText;
    [SerializeField] LevelManager levelManager;

    private float _timeLeft = 0f;

    string _strTime;

    private IEnumerator StartTimer(){
      
        while (_timeLeft > 0 && GameManager.gameOver == false){
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
    }

    private void Start(){

        if (LanguageManager.currentLanguage == Language.EN){

            _strTime = "Time ";

        }
        else{

            _strTime = "Время ";

        }

        _timeLeft = time;
        StartCoroutine(StartTimer());
    }

    private void UpdateTimeText(){
  
        if (_timeLeft < 0){
            _timeLeft = 0;
            GameManager.gameOver = true;
            levelManager.UpdateLevelInfo();
            this.enabled = false;

        }

        timerText.text = _strTime + ((int)_timeLeft).ToString();
    }
}

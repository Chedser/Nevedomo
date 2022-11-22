using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class VKPlaySecurity : MonoBehaviour{
    
    const string URL = "https://store.my.games/app/20000/oauth/authorize?redirect_uri=https%3A%2F%2Fexample.com%2Frand&response_type=code";
    [SerializeField]
    bool secureActivated;

    private void Awake(){
        if (!secureActivated) { return; }
        StartCoroutine(getRequest(URL));
    }

    IEnumerator getRequest(string uri){
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest(); // Отправка запроса на URL

        Debug.LogFormat("isNetworkError {0}: response: {1}", uwr.isNetworkError, uwr.downloadHandler.text);

        /* Ошибка подключения или ответ содержит 'err' */
        if (uwr.isNetworkError || uwr.downloadHandler.text.Contains("err")){
            Debug.Log("Выход из приложения");
            Application.Quit();
        }
        
    }
}

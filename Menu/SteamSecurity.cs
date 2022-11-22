using UnityEngine;
using Steamworks;

public class SteamSecurity : MonoBehaviour{

    void Start(){
                
        if (SteamManager.Initialized){
            string name = SteamFriends.GetPersonaName();
            
            if (name.Equals("") || name == null) {

                Application.Quit();

            }
        }
        else {

            Application.Quit();

        }
    }

}

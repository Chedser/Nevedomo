using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HashManager : MonoBehaviour{
    public static float GetHashHealth(float currentValue, float maxValue) {

        return currentValue / maxValue;
    }

    public static string GetLevelHash(int levelNumber) {

        return (3.14 * levelNumber * levelNumber).ToString();
    }

    public static string GetHash(int digit) {

        return (3.14 * digit * digit).ToString();

    }

    public static void WriteLevelInRegedit(int currentLevel) {

        int nextLevel = currentLevel + 1;

        PlayerPrefs.SetString("L"+ nextLevel, GetLevelHash(nextLevel));

    }

    public static bool LevelIsCracked(int currentLevelNumber) {

        bool isCracked = false;

        if (currentLevelNumber != 1){

            string keyFromRegedit = "L" + currentLevelNumber.ToString();

            string hashFromRegedit = PlayerPrefs.GetString(keyFromRegedit);

            if (!PlayerPrefs.HasKey(keyFromRegedit))
            {
                isCracked = true;
          
            }
            else{

                string hashLevel = HashManager.GetLevelHash(currentLevelNumber);

                if (hashFromRegedit != hashLevel){

                    isCracked = true;

                    PlayerPrefs.DeleteKey(keyFromRegedit);

                }

            }

        }

        return isCracked;

    }

    public static void LoadNextLevel(int currentNumberLevel) {

        if (!LevelIsCracked(currentNumberLevel + 1)){

            SceneManager.LoadScene("Level" + (currentNumberLevel + 1).ToString());

        }
        else {

            SceneManager.LoadScene("MainMenu");

        }

    }

    public static void SetCurrentLevel(int currentNumberLevel) {

        PlayerPrefs.SetString("CL", (currentNumberLevel + 1).ToString());

    }

}

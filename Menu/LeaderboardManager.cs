using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class LeaderboardManager : MonoBehaviour{

    [SerializeField]
    LMMainMenu lmMainMenu;

    string killsLeaderBoardName = "KILLERS";
    string winsLeaderBoardName = "WINNERS";
    [SerializeField] GameObject[] winImages;
    [SerializeField] GameObject[] killImages;
    [SerializeField] GameObject godOfWarImage;

    public static string[] achievmentsKills = {
        "ACH_K_1",
        "ACH_K_2",
        "ACH_K_3",
        "ACH_K_4",
        "ACH_K_5",
        "ACH_K_6",
        "ACH_K_7",
        "ACH_K_8",
        "ACH_K_9"};

    public static string[] achievmentsWins = {
        "ACH_W_1",
        "ACH_W_2",
        "ACH_W_3",
        "ACH_W_4",
        "ACH_W_5",
        "ACH_W_6",
        "ACH_W_7",
        "ACH_W_8",
        "ACH_W_9",
        "ACH_W_10",
        "ACH_W_11",
        "ACH_W_12",
        "ACH_W_13",
        "ACH_W_14",
        "ACH_W_15",
        "ACH_W_16",
        "ACH_W_17",
        "ACH_W_18",
        "ACH_W_19",
        "ACH_W_20",
        "ACH_W_21",
        "ACH_W_22",
        "ACH_W_23",
        "ACH_W_24",
        "ACH_W_25",
        "ACH_W_26"};

    static bool isMaximumKills;
    static bool isMaximumWins;

    // Start is called before the first frame update
    void Start(){

        /* if (SteamManager.Initialized){

            StartCoroutine(UpdateLeaderBoard(killsLeaderBoardName, lmMainMenu.killsCount));
            StartCoroutine(UpdateLeaderBoard(winsLeaderBoardName, lmMainMenu.winsCount));
            SetAchievmentKills(lmMainMenu.killsCount);
            SetAchievmentWins(lmMainMenu.winsCount);
            SetAchievmentGodOfWar();
        } */

    }

    public IEnumerator UpdateLeaderBoard(string leaderboardName, int score)
    {
        if (!SteamManager.Initialized)
        {
            Debug.Log("SteamManager is not initialized.");
            yield break;
        }

        bool error = false;

        SteamLeaderboard_t highScoreLeaderboard = new SteamLeaderboard_t();
        bool findLeaderboardCallCompleted = false;

        var findLeaderboardCall = SteamUserStats.FindLeaderboard(leaderboardName);
        var findLeaderboardCallResult = CallResult<LeaderboardFindResult_t>.Create();
        findLeaderboardCallResult.Set(findLeaderboardCall, (leaderboardFindResult, failure) =>
        {
            if (!failure && leaderboardFindResult.m_bLeaderboardFound == 1)
            {
                highScoreLeaderboard = leaderboardFindResult.m_hSteamLeaderboard;
            }
            else
            {
                error = true;
            }

            findLeaderboardCallCompleted = true;
        });

        while (!findLeaderboardCallCompleted) yield return null;

        if (error)
        {
            Debug.Log("Error finding High Score leaderboard.");
            yield break;
        }

        LeaderboardScoreUploaded_t leaderboardScore = new LeaderboardScoreUploaded_t();
        bool uploadLeaderboardScoreCallCompleted = false;

        var uploadLeaderboardScoreCall = SteamUserStats.UploadLeaderboardScore(highScoreLeaderboard,
            ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, score, new int[0], 0);
        var uploadLeaderboardScoreCallResult = CallResult<LeaderboardScoreUploaded_t>.Create();
        uploadLeaderboardScoreCallResult.Set(uploadLeaderboardScoreCall, (scoreUploadedResult, failure) =>
        {
            if (!failure && scoreUploadedResult.m_bSuccess == 1)
            {
                leaderboardScore = scoreUploadedResult;
            }
            else
            {
                error = true;
            }

            uploadLeaderboardScoreCallCompleted = true;
        });

        while (!uploadLeaderboardScoreCallCompleted) yield return null;

        if (error)
        {
            Debug.Log("Error uploading to High Score leaderboard.");
            yield break;
        }

        if (leaderboardScore.m_bScoreChanged == 1)
        {
            Debug.Log("New high score! Global rank #{0}." + leaderboardScore.m_nGlobalRankNew);

        }
        else
        {
            Debug.Log("A previous score was better. Global rank #{0}." + leaderboardScore.m_nGlobalRankNew);
        }
    }

    void SetAchievmentKills(int score){

            List<string> achNames = GetKillersAchNames(score);

            if (achNames.Count == 0) { return; }

            foreach (string achName in achNames){

                if (!PlayerPrefs.HasKey(achName))
                { // Достижение еще не получали
                    SteamUserStats.SetAchievement(achName);

                    if (SteamUserStats.StoreStats()){

                        PlayerPrefs.SetString(achName, achName);

                    }

                }

            }
  
    }

    List<string> GetKillersAchNames(int score){

        List<string> achNames = new List<string>();

        int killIndex = -1;

        if (score >= 1000)
        {
            achNames.Add(achievmentsKills[0]);
            ++killIndex;
        }

        if (score >= 2000)
        {
            achNames.Add(achievmentsKills[1]);
            ++killIndex;
        }

        if (score >= 3000)
        {
            achNames.Add(achievmentsKills[2]);
            ++killIndex;
        }

        if (score >= 4000)
        {
            achNames.Add(achievmentsKills[3]);
            ++killIndex;
        }

        if (score >= 5000)
        {
            achNames.Add(achievmentsKills[4]);
            ++killIndex;
        }

        if (score >= 6000)
        {
            achNames.Add(achievmentsKills[5]);
            ++killIndex;
        }

        if (score >= 7000)
        {
            achNames.Add(achievmentsKills[6]);
            ++killIndex;
        }

        if (score >= 8000)
        {
            achNames.Add(achievmentsKills[7]);
            ++killIndex;
        }

        if (score >= 9000)
        {
            achNames.Add(achievmentsKills[8]);
            ++killIndex;
        }


        if (killIndex != -1)
        {
            killImages[killIndex].gameObject.SetActive(true);
            if (killIndex == 8) {
                isMaximumKills = true;
            }
        }

        return achNames;

    }

    List<string> GetWinnersAchNames(int score){

        List<string> achNames = new List<string>();

        int winIndex = -1;
        int[] winDigits = new int[]{4, 8, 12, 16, 20, 24, 28, 32, 36, 40,
                                    44, 48, 52, 56, 60, 64, 68, 72, 76, 80,
                                    84, 88, 92, 95, 97, 100 };

        for (int i = 0; i < winDigits.Length; i++)
        {
            if (score >= winDigits[i])
            {
                achNames.Add(achievmentsWins[i]);
                ++winIndex;
            }
        }

        if (winIndex != -1) {
            winImages[winIndex].gameObject.SetActive(true);
         
            if (winIndex == 25){
                isMaximumWins = true;
            }
        }

        return achNames;

    }

    void SetAchievmentWins(int score){

            List<string> achNames = GetWinnersAchNames(score);

            if (achNames.Count == 0) { return; }

            foreach (string achName in achNames){

                if (!PlayerPrefs.HasKey(achName)){ // Достижение еще не получали
                    SteamUserStats.SetAchievement(achName);

                    if (SteamUserStats.StoreStats()){

                        PlayerPrefs.SetString(achName, achName);

                    }

                }

            }
        
    }

    void SetAchievmentGodOfWar(){

        if (isMaximumKills && isMaximumWins && PlayerPrefs.HasKey("L12")){

                string hashFromRegedit = PlayerPrefs.GetString("L12");
                string hashLevel = HashManager.GetLevelHash(12);

                if (hashFromRegedit == hashLevel){
                    godOfWarImage.SetActive(true);
                if (!PlayerPrefs.HasKey("ACH_GOD_OF_WAR"))
                    { // Достижение еще не получали
                        SteamUserStats.SetAchievement("ACH_GOD_OF_WAR");

                        if (SteamUserStats.StoreStats()){

                            Debug.Log("Получено достижение Бог Войны!");

                            PlayerPrefs.SetString("ACH_GOD_OF_WAR", "ACH_GOD_OF_WAR");

                        }

                    }

                }

        }
    }

}



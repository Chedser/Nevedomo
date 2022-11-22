using UnityEngine;
using Steamworks;

public enum Rank {
    Private = 1, // Рядовой
    Corporal, // Младший сержант
    Sergeant, // Сержант
    StaffSergeant, // Старший сержант
    MasterSergeant, // Старшина 
    Ensign, // Прапорщик
    SeniorEnsign, // Старший прапорщик
    Lieutenant, // Лейтенант
    SeniorLieutenant, // Старший лейтенант
    Captain, // Капитан
    Major, // Майор
    LieutenantColonel, // Подполовник
    Colonel // Полковник
}

public class Achievments : MonoBehaviour{

    [SerializeField]
    GameManager gameManager;

    uint accountId;
   public static uint achNumber;
    static CSteamID steamID;
    uint appId = 1861310;

   public static string killsLeaderBoardName = "KILLERS";
    public static string winsLeaderBoardName = "WINNERS";

    public static string killsStatsName = "KILLS_COUNT";
    public static string winsStatsName = "WINS_COUNT";

    public static string[] achievments = { 
        "ACH_R_1", 
        "ACH_R_2", 
        "ACH_R_3", 
        "ACH_R_4", 
        "ACH_R_5", 
        "ACH_R_6",
        "ACH_R_7",
        "ACH_R_8",
        "ACH_R_9",
        "ACH_R_10",
        "ACH_R_11",
        "ACH_R_12"};

    private const ELeaderboardUploadScoreMethod s_leaderboardMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest;

    private static SteamLeaderboard_t s_currentLeaderboard;
    private static bool s_initialized = false;
    private static CallResult<LeaderboardFindResult_t> m_findResult = new CallResult<LeaderboardFindResult_t>();
    private static CallResult<LeaderboardScoreUploaded_t> m_uploadResult = new CallResult<LeaderboardScoreUploaded_t>();

    // Start is called before the first frame update
    void Start(){

        SteamAPI.Init();

        SteamAPI.RunCallbacks();

        achNumber = (uint)gameManager.currentLevelNumber - 1;

      //  PlayerPrefs.DeleteKey(achievments[achNumber]);
       // SteamUserStats.ClearAchievement(achievments[achNumber]);
     
        if (SteamManager.Initialized){

            steamID = SteamUser.GetSteamID();

            accountId = steamID.GetAccountID().m_AccountID;

            if (gameManager.currentLevelNumber == (int)Rank.Private){

                SetAchievment(achievments[achNumber]);

            }

        }

    }

    public static void SetAchievment(string achName){

        if (SteamManager.Initialized){

            if (!PlayerPrefs.HasKey(achName)){ // Достижение еще не получали
                               
                SteamUserStats.RequestCurrentStats();

                SteamUserStats.SetAchievement(achName);
                                                             
                    if (SteamUserStats.StoreStats()){

                    Debug.Log("Получено достижение!");

                    PlayerPrefs.SetString(achName, achName);

                    }
                                                  
            }

        }

    }

    public static void SetStats(string statName, int data, string leaderBoardName) {

        if (SteamManager.Initialized) {

            SteamAPI.RunCallbacks();

            SteamUserStats.RequestCurrentStats();

            int statFromServer = 0;

            SteamUserStats.GetStat(statName, out statFromServer);

            int statToSave = statFromServer + data;

            SteamUserStats.SetStat(statName, statToSave);

            SteamUserStats.StoreStats();

        }
    
    }

    public static int GetStats(string statName) {

        SteamAPI.RunCallbacks();

        SteamUserStats.RequestCurrentStats();

        int statFromServer = 0;

        SteamUserStats.GetStat(statName, out statFromServer);

        return statFromServer;

    }
           
}




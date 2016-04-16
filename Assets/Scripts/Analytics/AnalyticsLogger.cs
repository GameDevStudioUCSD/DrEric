using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityEngine.UI;

public class AnalyticsLogger : MonoBehaviour {

    private int level = -1;
    private string world = "NOTAWORLD";
    private string eventName = "INVALIDEVENT";
    private bool destroyOnMainMenu = false;

    private Dictionary<string, object> log;
    private bool hasCompleted = false;
    public string randHash = "http://bit.ly/DrEricSurvey";
    public Text displayHash;


    // MonoBehaviour functions
	void Start() {
        DontDestroyOnLoad(this.gameObject);
        SetupLoggingDict();
        displayHash.text += randHash;
	}
	
	void CreateRandomHash() {
		for (int i = 0; i < 4; i++)
		{
            randHash += (char)((int)Random.Range(65, 90));
        }
        randHash += (int)Random.Range(0, 9);
        randHash += (int)Random.Range(0, 9);
	}

    void OnLevelWasLoaded(int level)
    {
        SetupLoggingDict();
        if(world != "NOTAWORD")
            destroyOnMainMenu = true;
    }

    void Update()
    {
        Dictionary<string, object> turkerLog = new Dictionary<string, object>();
        turkerLog["UID"] = randHash;
        if (level == 11 && DeathCount.GetDeathCount() > 2 && !hasCompleted )
        {
            //Analytics.CustomEvent("Turker Completed", turkerLog);
            hasCompleted = displayHash.enabled = true;
        }
        if (level == 12 && !hasCompleted)
        {
            //Analytics.CustomEvent("Turker Completed", turkerLog);
            hasCompleted = displayHash.enabled = true;
        }
    }

    // Public Functions
    public static void SendEvent()
    {
        AnalyticsLogger logger = GetInstance();
        if (logger == null)
            LogWarningNotFound();
        else
            logger.CreateCustomEvent();
    }

    public static AnalyticsLogger GetInstance()
    {
        GameObject loggerObj = GameObject.Find(Names.ANALYTICSLOGGER);
        AnalyticsLogger logger = null;
        if (loggerObj != null)
            logger = loggerObj.GetComponent<AnalyticsLogger>();
        return logger;
    }
    
    public static void DumpCompletionLog()
    {
        AnalyticsLogger logger = GetInstance();
        if (logger == null)
        {
            LogWarningNotFound();
            return;
        }
        logger.LogFinishingStats();
        Debug.Log("Custom Event: " + logger.eventName);
        foreach( string key in logger.log.Keys)
        {
            Debug.Log("Key: " + key + " Value: " + logger.log[key]);
        }
    }
    #if UNITY_ANALYTICS
    public void CreateCustomEvent()
    {
        LogFinishingStats();
        //Analytics.CustomEvent(eventName, log);
    }

    #else
        public void CreateCustomEvent() {
            Debug.LogWarning("Unity Analytics is not working on your device!");
        }
    #endif


    public void LogCustomEvent(string key, object value)
    {
        log[key] = value;
    }

    // Private Functions
    private void SetupLoggingDict()
    {
        DetermineLevelInfo();
        // Create a new instance for the dict
        log = new Dictionary<string, object>();
        // Set the event's name
        eventName = "Completed_" + world + "_Level_" + level;
    }

    private void LogFinishingStats()
    {
        LogCustomEvent("Number of Deaths", DeathCount.GetDeathCount());
        LogCustomEvent("Time to Complete", Time.timeSinceLevelLoad);
    }
    private void DetermineLevelInfo()
    {
        int sceneIdx = Application.loadedLevel;
        LevelInfo info = LevelLoader.GetLevelInfo(sceneIdx );
        switch (info.GetWorld())
        {
            case World.MainMenu:
                if(destroyOnMainMenu)
                    Destroy(this.gameObject);
                break;
            case World.Space:
                world = "Space";
                level = (int)(info.GetLevel()+1);
                break;
            default:
                world = "NOTAWORLD";
                break;
        }
    }


    private static void LogWarningNotFound()
    {
        Debug.LogWarning("The UnityAnalytics GameObject could not be found! Did you start the game from the mainmenu?");
    }


   

}

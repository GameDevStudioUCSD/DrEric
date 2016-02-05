using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class AnalyticsLogger : MonoBehaviour {

    public int level = -1;
    public string world = "NOTAWORLD";
    public string eventName = "INVALIDEVENT";
    private bool destroyOnMainMenu = false;

    private Dictionary<string, object> log;


    // MonoBehaviour functions
	void Start() {
        DontDestroyOnLoad(this.gameObject);
        SetupLoggingDict();
	}

    void OnLevelWasLoaded(int level)
    {
        destroyOnMainMenu = true;
        SetupLoggingDict();
    }

    // Public Functions
    public static void SendEvent()
    {
        AnalyticsLogger logger = GetInstance(); 
        logger.CreateCustomEvent();
    }

    public static AnalyticsLogger GetInstance()
    {
        GameObject loggerObj = GameObject.Find(Names.ANALYTICSLOGGER);
        AnalyticsLogger logger = loggerObj.GetComponent<AnalyticsLogger>();
        return logger;
    }
    
    public static void DumpCompletionLog()
    {
        AnalyticsLogger logger = GetInstance();
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
        Analytics.CustomEvent(eventName, log);
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
    void SetupLoggingDict()
    {
        DetermineLevelInfo();
        // Create a new instance for the dict
        log = new Dictionary<string, object>();
        // Set the event's name
        eventName = "Completed_" + world + "_Level_" + level;
    }

    void LogFinishingStats()
    {
        LogCustomEvent("Number of Deaths", DeathCount.GetDeathCount());
        LogCustomEvent("Time to Complete", Time.timeSinceLevelLoad);
    }
    void DetermineLevelInfo()
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


   

}

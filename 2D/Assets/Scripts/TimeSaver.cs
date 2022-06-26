using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TimeSaver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finishTimer;
    public TimeSpan finishTime;
    public static TimeSaver instance;

    private void Awake()
    {
        ManageSingelton();
    }

    void ManageSingelton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public string GetTime()
    {
        string timePlayingStr = "Run Time:" + finishTime.ToString("mm':'ss'.'ff");
        return timePlayingStr;
    }

}

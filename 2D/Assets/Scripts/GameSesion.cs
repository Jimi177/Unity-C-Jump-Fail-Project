using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class GameSesion : MonoBehaviour
{
    [Header("Game On")]
    public bool gameON = true;

    [Header("Timer")]
    [SerializeField] TextMeshProUGUI timeCounter;
    private TimeSpan timePlaying;
    private float elapsedTime;

    [Header("Fails")]
    [SerializeField] TextMeshProUGUI failsCounter;
    public int fails;


    [Header("Player")]
    public Vector2 playerPosition;
    public bool isMoving;
    public bool isGrounded;
    public bool isMovingOnLadder;

    //Acess
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        #region Start set
        failsCounter.text = "Fails:";
        timeCounter.text = "Time: 00:00.0";
        gameON = true;
        fails = 0;
        #endregion
    }

    private void Update()
    {
        UpdateTimer();
    }


    void UpdateTimer()
    {
        if(gameON && timeCounter != null)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Run Time:" + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
        }
    }

    public void FailsCounter()
    {
        if(gameON && failsCounter != null)
        {
            audioPlayer.DeathClip();
            string failsCounterStr = "Fails:" + fails + "  XD";
            failsCounter.text = failsCounterStr;
        }
    }

    public void RestartTimer()
    {
        elapsedTime = 0;
    }
    public void LevelFinished()
    {
        fails = 0;
        elapsedTime = 0;
    }

    #region AudioClips
    public void BounceAudio()
    {
        audioPlayer.BounceClip(); 
    }
    public void LadderAudio()
    {
        audioPlayer.LadderClip();
    }
    public void JumpAudio()
    {
        audioPlayer.JumpClip();
    }
    #endregion

}

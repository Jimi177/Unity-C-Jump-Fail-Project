using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Scenes : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] bool isOnTriggerEnter;
    [SerializeField] bool finishScreen;
    [SerializeField] bool mainMenu;
    [SerializeField] int sceneIndex;

    AudioPlayer audioPlayer;
    GameSesion gs;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gs = FindObjectOfType<GameSesion>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOnTriggerEnter)
        {
            if (finishScreen)
            {
                LoadFinishScreen(sceneIndex);
            }

            if (mainMenu)
            {
                LoadMainMenu();
            }
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(2);
    }

    private void LoadFinishScreen(int sceneIndex)
    {
        gs.gameON = false;
        audioPlayer.FinishClip();
        StartCoroutine(LoadScene(sceneIndex));
    }

    IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(index);
    }
}

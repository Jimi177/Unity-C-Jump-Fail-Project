using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{
    AudioPlayer audioPlayer;
    GameSesion gs;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gs = FindObjectOfType<GameSesion>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gs.gameON = false;
        audioPlayer.FinishClip();
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}

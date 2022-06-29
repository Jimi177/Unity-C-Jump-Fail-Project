using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool isOnTriggerEnter;
    [SerializeField] private bool finishScreen;
    [SerializeField] private bool mainMenu;
    [SerializeField] private int sceneIndex;

    public int GetCurrentIndex()
    {
        return sceneIndex;
    }

    public static void LoadFinishScene(int index)
    {
        var audioPlayer = FindObjectOfType<AudioPlayer>();
        var gs = FindObjectOfType<GameSesion>();

        gs.gameON = false;
        audioPlayer.FinishClip();
        SceneManager.LoadScene(index);
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
}

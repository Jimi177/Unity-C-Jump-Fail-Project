using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Running")]
    [SerializeField] AudioClip randomStepClip;
    [SerializeField] AudioClip[] stepClips;
    [SerializeField] float timeBetweenSteps = 0.5f;

    [Header("Jump")]
    [SerializeField] AudioClip jumpClip;
    [SerializeField] [Range(0f, 1f)] float jumpVolume = 1f;

    [Header("Ladder")]
    [SerializeField] AudioClip ladderClip;
    [SerializeField] [Range(0f, 1f)] float ladderVolume = 1f;
    [SerializeField] float timeBetweenSounds = 0.3f;

    [Header("Death")]
    [SerializeField] AudioClip randomDeathClip;
    [SerializeField] AudioClip[] deathClips;
    [SerializeField] [Range(0f, 1f)] float deathVolume = 1f;

    [Header("Bounce")]
    [SerializeField] AudioClip bounceClip;
    [SerializeField] [Range(0f, 1f)] float bounceVolume = 1f;

    [Header("Finish")]
    [SerializeField] AudioClip finishClip;
    [SerializeField] [Range(0f, 1f)] float finishVolume = 1f;


    //Timer to restart clips
    [SerializeField] float timer;

    //Acess
    [SerializeField] AudioSource playerRunning;
    GameSesion gs;
    

    private void Awake()
    {
        gs = FindObjectOfType<GameSesion>();
    }

    private void Update()
    {
        RunningClip();
    }

    void PlayClip(AudioClip clip, float volumen)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip,gs.playerPosition, volumen); 
        }
    }

    void Timer()
    {
        timer += Time.deltaTime;
    }
    #region AudioClips
    public void RunningClip()
    {
        Timer();

        if (gs.isMoving && gs.isGrounded && timeBetweenSteps < timer)
        {
            timer = 0;
            RandomRunningClip();
        }
    }
    void RandomRunningClip()
    {
        randomStepClip = stepClips[Random.Range(0, stepClips.Length)];
        playerRunning.PlayOneShot(randomStepClip);
    }
    public void JumpClip()
    {
        PlayClip(jumpClip, jumpVolume);
    }
    public void LadderClip()
    {
        if(gs.isMovingOnLadder && timeBetweenSounds < timer)
        {
            timer = 0;
            PlayClip(ladderClip, ladderVolume);
        }
    }
    public void FinishClip()
    {
        PlayClip(finishClip, finishVolume);
    }
    public void BounceClip()
    {
        PlayClip(bounceClip, bounceVolume);
    }
    public void DeathClip()
    {
        RandomDeathClip();
        PlayClip(randomDeathClip, deathVolume);
    }
    AudioClip RandomDeathClip()
    {
        randomDeathClip = deathClips[Random.Range(0, deathClips.Length)];
        return randomDeathClip;
    }
    #endregion
}

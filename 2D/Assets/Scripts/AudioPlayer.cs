using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Running")]
    [SerializeField] private AudioClip randomStepClip;
    [SerializeField] private AudioClip[] stepClips;
    [SerializeField] private float timeBetweenSteps = 0.5f;

    [Header("Jump")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] [Range(0f, 1f)] private float jumpVolume = 1f;

    [Header("Ladder")]
    [SerializeField] private AudioClip ladderClip;
    [SerializeField] [Range(0f, 1f)] private float ladderVolume = 1f;
    [SerializeField] private float timeBetweenSounds = 0.3f;

    [Header("Death")]
    [SerializeField] private AudioClip randomDeathClip;
    [SerializeField] private AudioClip[] deathClips;
    [SerializeField] [Range(0f, 1f)] private float deathVolume = 1f;

    [Header("Bounce")]
    [SerializeField] private AudioClip bounceClip;
    [SerializeField] [Range(0f, 1f)] private float bounceVolume = 1f;

    [Header("Finish")]
    [SerializeField] private AudioClip finishClip;
    [SerializeField] [Range(0f, 1f)] private float finishVolume = 1f;


    //Timer to restart clips
    [SerializeField] private float timer;

    //Acess
    [SerializeField] private AudioSource playerRunning;
    private GameSesion gs;
    

    private void Awake()
    {
        gs = FindObjectOfType<GameSesion>();
    }

    private void Update()
    {
        RunningClip();
    }

    private void PlayClip(AudioClip clip, float volumen)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip,gs.playerPosition, volumen); 
        }
    }

    private void Timer()
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
    private void RandomRunningClip()
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
    private AudioClip RandomDeathClip()
    {
        randomDeathClip = deathClips[Random.Range(0, deathClips.Length)];
        return randomDeathClip;
    }
    #endregion
}

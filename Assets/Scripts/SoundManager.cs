using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip captureClip;
    public AudioClip moveClip;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        BoardManager.OnPieceMove += BoardManagerOnPieceMove;
        BoardManager.OnPieceCapture += BoardManagerOnPieceCapture;
    }


    private void OnDisable()
    {
        BoardManager.OnPieceMove -= BoardManagerOnPieceMove;
        BoardManager.OnPieceCapture -= BoardManagerOnPieceCapture;
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.PlayOneShot(clip);
    }

    private void BoardManagerOnPieceMove(object sender, EventArgs e)
    {
        PlayClip(moveClip);
    }

    private void BoardManagerOnPieceCapture(object sender, EventArgs e)
    {
        PlayClip(captureClip);
    }
}
using System;
using UnityEngine;

public class UIAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public static UIAnimationManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        Invoke(nameof(ListenToEvents), 0.1f);
    }
    private void ListenToEvents()
    {
        GameManager.instance.onGameEnded += OnGameEnded;
        GameManager.instance.onGamePaused += OnGamePaused;
    }

    private void OnDisable()
    {
        GameManager.instance.onGameEnded -= OnGameEnded;
        GameManager.instance.onGamePaused -= OnGamePaused;
    }
    private void OnGameEnded()
    {
        PlayEndAnimation();
    }
    private void OnGamePaused()
    {
        PlayPauseAnimation();
    }

    private void PlayPauseAnimation()
    {
        anim.Play(null);
        anim.Play("PanelPause_On");
    }

    private void PlayEndAnimation()
    {
        anim.Play("PanelGameEnded_On");
    }

    public void PlayStartAnimation()
    {
        anim.Play("CountDownTimer");
    }
}

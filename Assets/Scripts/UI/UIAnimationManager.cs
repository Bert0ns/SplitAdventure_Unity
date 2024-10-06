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
        GameManager.instance.onGameEnded += OnGameEnded;
    }
    private void OnDisable()
    {
        GameManager.instance.onGameEnded -= OnGameEnded;
    }
    private void OnGameEnded()
    {
        PlayEndAnimation();
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

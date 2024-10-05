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

    public void PlayStartAnimation()
    {
        anim.Play("CountDownTimer");
        anim.Play("PanelFade");
    }
}

using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    public void PlayAnimationWalkRight()
    {
        anim.Play("Character_WalkRight");
    }
    public void PlayAnimationWalkLeft() 
    {
        anim.Play("Character_WalkLeft");
    }
    public void PlayAnimationIdle()
    {
        anim.Play("Character_Idle");
    }
}

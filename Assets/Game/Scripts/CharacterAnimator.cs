using UnityEngine;

public enum AnimationState { IDLE = 0, RUN = 1, JUMP = 2, DASH = 3, ANGEL = 4, BURN = 5 }

public class CharacterAnimator : MonoBehaviour
{
    public Animator animator;

    public void PlayAnim(AnimationState state)
    {
        animator.SetInteger("AnimState", (int)state);
    }
}

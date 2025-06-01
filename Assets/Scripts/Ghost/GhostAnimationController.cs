using UnityEngine;

public class GhostAnimationController : MonoBehaviour
{
    private Animator anim;

    private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
    private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
    private static readonly int SurprisedState = Animator.StringToHash("Base Layer.surprised");
    private static readonly int DissolveState = Animator.StringToHash("Base Layer.dissolve");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        anim.CrossFade(IdleState, 0.1f);
    }

    public void PlayMove()
    {
        anim.CrossFade(MoveState, 0.1f);
    }

    public void PlaySurprised()
    {
        anim.CrossFade(SurprisedState, 0.1f);
    }

    public void PlayDissolve()
    {
        anim.CrossFade(DissolveState, 0.1f);
    }
}

using UnityEngine;


public class _AnimationsHandler : MonoBehaviour
{
    //public _PlayerStateMachine _cntx;
    public Animator anim;



    public void Initialize()
    {
        //if (!_cntx) _cntx = GetComponent<_PlayerStateMachine>();
        if (!anim) anim = GetComponent<Animator>();
    }
    public void UpdateAnimatorFloat(string name, float value)
    {
        anim.SetFloat(name, value);
    }

    public void UpdateAnimatorInt(string name, int value)
    {
        anim.SetInteger(name, value);
    }
    public void UpdateAnimatorBool(string name, bool value)
    {
        anim.SetBool(name, value);
    }
    public void UpdateAnimatortrrigger(string name)
    {
        anim.SetTrigger(name);
    }


    public void PlayAnimation(string targetAnim, bool isInteracting)
    {
        anim.applyRootMotion = isInteracting;
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }


    public void EnableCombo()
    {
        anim.SetBool("canDoCombo", true);
    }

    public void DisableCombo()
    {
        anim.SetBool("canDoCombo", false);
    }

    public void RestIsInteracting()
    {
        anim.SetBool("isInteracting", false);
    }
}
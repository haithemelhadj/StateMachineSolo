using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator anim;


    public void Initialize()
    {
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

    public void PlayAnimation(string targetAnim)
    {
        anim.CrossFade(targetAnim, 0.2f);
    }

    public void ExitAnimation()
    {
        anim.CrossFade("Empty", 0.2f);
    }

}

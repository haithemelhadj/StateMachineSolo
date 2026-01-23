using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootState : State
{
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        DetectGettingHit(other);
    }
    public override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        DetectGettingHit(other);
    }

    public void DetectGettingHit(Collider2D other)
    {
        if (currentContext.isInvunrable) return;
        if (other.gameObject.CompareTag("Attack"))
        {
            currentContext.HitSource = other.gameObject;
            SwitchState(factory.GetState(_States.GetHit));
        }
    }
}

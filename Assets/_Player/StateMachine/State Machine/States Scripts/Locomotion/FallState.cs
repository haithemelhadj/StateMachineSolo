using UnityEngine;

[CreateAssetMenu(fileName = "Fall State", menuName = "States List/Fall")]
public class FallState : LocomotionState
{

    #region  walk Movement 
    [Header("Walk Movement")]
    public float fallMaxSpeed;
    public float fallAcceleration;
    public float fallDeceleration;
    public bool highFall;
    #endregion
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (currentContext.isGrounded)
        {
            SwitchState(factory.GetState(_States.Grounded));
            //currentContext.animatorController.PlayAnimation(animationName="Fall Recovery");

        }
        if (currentContext.canCyoteJump && currentContext.jumpInputDown)
        {
            SwitchState(factory.GetState(_States.Jump));
        }

        if (!currentContext.isGrounded && currentContext.isHuggingWall)
        {
            SwitchState(factory.GetState(_States.WallSlide));
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        SetMoveSpeed();
        if (currentContext.fasterFallMultiplier == 0f) currentContext.fasterFallMultiplier = 1f;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        Fall();
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (currentContext.jumpInputDown)
        {
            currentContext.jumpPressTime = Time.time;
            currentContext.willBufferJump = true;
        }
        CyoteTime();
    }

    public void Fall()
    {
        if (currentContext.Rb.velocity.y <= 0f)
        {
            //make fall speed faster
            currentContext.Rb.velocity += Vector2.up * Physics2D.gravity.y * currentContext.fasterFallMultiplier * Time.deltaTime;
            //limit fall speed 
            if (currentContext.Rb.velocity.y < currentContext.maxFallSpeed)
            {
                highFall = true;
                currentContext.Rb.velocity = new Vector2(currentContext.Rb.velocity.x, currentContext.maxFallSpeed);
            }
            else
                highFall = false;

        }

    }

    public void CyoteTime()
    {
        if (Time.time - currentContext.LastTimeGrounded > currentContext.cyoteTime)// || Time.time - currentContext.LastTimeWalled > currentContext.cyoteTime)
        {
            currentContext.canCyoteJump = false;
        }
    }

    #region change Speed Input

    public override void SetMoveSpeed()
    {

        currentContext.currentMaxMoveSpeed = fallMaxSpeed;
        currentContext.currentAcceleration = fallAcceleration;
        currentContext.currentDeceleration = fallDeceleration;
    }
    #endregion
}

using UnityEngine;

public class LocomotionState : State
{

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (currentContext.dashInputDown && currentContext.canDashCheck())
        {
            SwitchState(factory.GetState(_States.Dash));
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Move();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
    }
    public override void OnExit()
    {
        base.OnExit();
    }

    #region Move
    public void Move()
    {
        if (currentContext.hInput != 0f)
        {
                currentContext.Rb.velocity = Vector3.MoveTowards(currentContext.Rb.velocity, new Vector3(currentContext.hInput * currentContext.currentMaxMoveSpeed, currentContext.Rb.velocity.y, 0f), currentContext.currentAcceleration * Time.deltaTime);
            

            //flip character and keep it that way when no inputs
            if (!currentContext.isHuggingWall)
                Flip();
        }
        else if (currentContext.Rb.velocity.x != 0f) //slow player to stop
            currentContext.Rb.velocity = Vector3.MoveTowards(currentContext.Rb.velocity, new Vector3(0f, currentContext.Rb.velocity.y, 0f), currentContext.currentDeceleration * Time.deltaTime);
        currentContext.currentMoveSpeed = currentContext.Rb.velocity.x; //set current horizontal speed
    }

    public void Flip()
    {
        Vector3 currentScale = currentContext.transform.localScale;
        currentScale.x = Mathf.Sign(currentContext.hInput) * Mathf.Abs(currentScale.x);
        currentContext.transform.localScale = currentScale;
    }
    #endregion

    #region walk Speed Input

    public virtual void SetMoveSpeed()
    {

    }
    #endregion







}

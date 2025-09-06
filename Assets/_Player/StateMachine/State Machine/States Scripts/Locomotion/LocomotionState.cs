using UnityEngine;

public class LocomotionState : State
{
    protected float speedFlipModifier = 0.5f;
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
        //currentContext.animatorController.PlayAnimation(animationName);
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
            CheckFlip();
        }
        else if (currentContext.Rb.velocity.x != 0f) //slow player to stop
        {
            currentContext.Rb.velocity = Vector3.MoveTowards(currentContext.Rb.velocity, new Vector3(0f, currentContext.Rb.velocity.y, 0f), currentContext.currentDeceleration * Time.deltaTime);
        }

        //currentContext.Rb.velocity = currentContext.currentMoveSpeed; //apply movement
    }

    private void CheckFlip()
    {
        //if flip while moving fast, flip speed instantly to avoid sliding
        //if (Mathf.Abs(currentContext.Rb.velocity.x) >= (currentContext.currentMaxMoveSpeed / 2) 
         if( Mathf.Sign(currentContext.Rb.velocity.x) != Mathf.Sign(currentContext.hInput))
        {
                Debug.Log("velocity: "+currentContext.Rb.velocity.x + "\n"+
                    "currentContext.currentMaxMoveSpeed: " + currentContext.currentMaxMoveSpeed + "\n"+
                    "Sign(currentContext.Rb.velocity.x): " + Mathf.Sign(currentContext.Rb.velocity.x) + "\n"+
                    "Sign(currentContext.hInput): " + Mathf.Sign(currentContext.hInput) + "\n"
                    );
            float currentMoveSpeed = currentContext.hInput * currentContext.currentMaxMoveSpeed;
            currentContext.Rb.velocity = new Vector3(currentMoveSpeed, currentContext.Rb.velocity.y, 0f);
            if ((stateMachine.currentState is GroundedState))
            {
                currentContext.animatorController.PlayAnimation("MoveFlip");
                Debug.Log("Flip Animation");
            }

            //### another way of doing it is to increase acceleration when flip is detected
            //float speedDif = currentContext.currentMaxMoveSpeed - Mathf.Abs(currentContext.currentMoveSpeed.x);
            //float movement = speedDif * currentContext.currentAcceleration;
            //currentContext.currentMoveSpeed = (new Vector3(currentContext.hInput * movement, currentContext.Rb.velocity.y, 0f));
        }



        //flip character and keep it that way when no inputs
        if (currentContext.canFlip)
            Flip();
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

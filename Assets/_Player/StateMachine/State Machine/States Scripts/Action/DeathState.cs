using UnityEngine;

[CreateAssetMenu(fileName = "Death State", menuName = "States List/Player/Death")]
public class DeathState : ActionState
{
    
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - enterTime > duration)
        {
            //Debug.Log(this.name + "duration has ended");
            SwitchState(factory.GetState(_States.Grounded));
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //Debug.Log("dead");
    }

    public override void OnExit()
    {
        base.OnExit();
            currentContext.transform.position = currentContext.respawnPoint.position;
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
}

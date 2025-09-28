using UnityEngine;

[CreateAssetMenu(fileName = "Ground Npc Chase", menuName = "States List/Ground Npc /Chase")]
public class GroundNpcChase : GroundNpcLocomotion
{

    [Header("Chase")]
    public float chaseSpeed;
    
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (!currentContext.hasTarget)
        {
            SwitchState(factory.GetState(_States.Search));
        }
    }

    public override void OnEnter()
    {
        Debug.Log("Chase");
        base.OnEnter();
        ChangeColor(color: Color.red);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        MoveTowardsTargetPosition(currentContext.currentTarget.position, chaseSpeed);
        currentContext.targetLastSeenPos = currentContext.targetInSight.position;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ground Npc Idle", menuName = "States List/Ground Npc /Idle")]
public class GroundNpcIdle : GroundNpcLocomotion
{
    public float minRandIdleTime;
    public float maxRandIdleTime;
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - enterTime >= duration)
        {
            SwitchState(factory.GetState(_States.Patrol));
        }
    }


    public override void OnEnter()
    {
        base.OnEnter();
        ChangeColor(color: Color.green);
        duration = Random.Range(minRandIdleTime, maxRandIdleTime);
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
}

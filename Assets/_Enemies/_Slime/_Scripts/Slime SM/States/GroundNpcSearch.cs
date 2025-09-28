using UnityEngine;

[CreateAssetMenu(fileName = "Ground Npc Search", menuName = "States List/Ground Npc /Search")]
public class GroundNpcSearch : GroundNpcLocomotion
{

    #region Search
    [Header("Search")]
    public float searchSpeed;
    public float minrandSearchTime;
    public float maxrandSearchTime;
    #endregion
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
        Debug.Log("Search");
        base.OnEnter();
        ChangeColor(color: Color.yellow);
        duration = Random.Range(minrandSearchTime, maxrandSearchTime);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        MoveTowardsTargetPosition(currentContext.targetLastSeenPos, searchSpeed);
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

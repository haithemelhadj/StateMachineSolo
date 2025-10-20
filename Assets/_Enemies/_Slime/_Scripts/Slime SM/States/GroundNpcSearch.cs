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

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    public override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
    }

    public override void OnEnter()
    {
        //Debug.Log("Search");
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

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }


}

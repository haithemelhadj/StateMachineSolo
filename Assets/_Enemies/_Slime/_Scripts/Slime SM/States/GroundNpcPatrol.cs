using UnityEngine;

[CreateAssetMenu(fileName = "Ground Npc patrol", menuName = "States List/Ground Npc /patrol")]
public class GroundNpcPatrol : GroundNpcLocomotion
{
    [Header("Patrol")]
    public float patrolSpeed;
    public float minRandPatrolTime;
    public float maxRandPatrolTime;

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - enterTime >= duration)
        {
            SwitchState(factory.GetState(_States.Idle));
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
        base.OnEnter();
        //Debug.Log("Patrol");
        ChangeColor(color: Color.blue);
        duration = Random.Range(minRandPatrolTime, maxRandPatrolTime);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        Patrol();
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

    public void Patrol()
    {
        currentContext.Rb.velocity = Vector3.MoveTowards(currentContext.Rb.velocity, new Vector3(currentContext.transform.localScale.x * patrolSpeed, currentContext.Rb.velocity.y, 0f), patrolSpeed);
        if ((currentContext.isNearEdge || currentContext.isHuggingWall) && currentContext.isGrounded)
        {
            currentContext.Flip();
        }
    }

}

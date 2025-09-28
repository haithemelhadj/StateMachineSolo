using UnityEngine;

public class GroundNpcLocomotion : GroundNpcState
{
    public float catchDistance = 1;
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (currentContext.hasTarget)
        {
            SwitchState(factory.GetState(_States.Chase));
        }

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

    public bool RandomChance(float percentage)
    {
        return Random.Range(0f, 100f) < percentage;
    }


    public void ChangeColor(Color color)
    {
        currentContext.spriteRenderer.color = color;
    }

    

    public void MoveTowardsTargetPosition(Vector2 target, float speed)
    {
        if (Mathf.Abs(target.x - currentContext.transform.position.x) < catchDistance)//calculate x distance for grounded mob (abs for both sides)
        {
            //stand idle
            currentContext.Rb.velocity = Vector3.zero;
            if (Vector2.Distance(currentContext.transform.position, target) < currentContext.attackDistance)
            {
                Debug.Log("attack!");
                //SwitchState(_factory.Attack());
            }
        }
        else
        {
            currentContext.Rb.velocity = Vector3.MoveTowards(currentContext.Rb.velocity, new Vector3(currentContext.transform.localScale.x * speed, currentContext.Rb.velocity.y, 0f), speed * 0.3f);
        }
        if (Mathf.Sign(currentContext.transform.position.x - target.x) != Mathf.Sign(-currentContext.transform.localScale.x))
            currentContext.Flip();

    }


    

}

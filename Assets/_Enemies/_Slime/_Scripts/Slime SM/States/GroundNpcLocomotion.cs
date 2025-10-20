using UnityEngine;

public class GroundNpcLocomotion : GroundNpcState
{
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
        if (Mathf.Abs(target.x - currentContext.transform.position.x) < currentContext.catchDistance)//calculate x distance for grounded mob (abs for both sides)
        {
            //stand idle
            currentContext.Rb.velocity = Vector3.zero;
            if (Vector2.Distance(currentContext.transform.position, target) < currentContext.attackDistance)
            {
                if (Time.time - currentContext.lastAttackTime >= currentContext.attackCooldown)
                {

                    //Debug.Log("attack!");
                    //SwitchState(_factory.Attack());
                    SwitchState(factory.GetState(_States.Attack));
                }
            }
        }
        else
        {
            currentContext.Rb.velocity = Vector3.MoveTowards(currentContext.Rb.velocity, new Vector3(currentContext.transform.localScale.x * speed, currentContext.Rb.velocity.y, 0f), speed * 0.3f);
        }
        if (Mathf.Sign(currentContext.transform.position.x - target.x) != Mathf.Sign(-currentContext.transform.localScale.x))
            currentContext.Flip();

    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Attack"))
        {
            SwitchState(factory.GetState(_States.GetHit));
            Debug.Log("hit!");
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    public override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
    }
}

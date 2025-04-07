using UnityEngine;

namespace StateMachine
{
    public class AiMovementState : AiBaseState
    {

        public AiMovementState(AiStateMachine currentContext, AiStateFactory StateFactory)
            : base(currentContext, StateFactory)
        {
            //_isRootState = true;
        }
        public override void EnterState()
        {
            Debug.Log("enter new state:" + this);


        }
        public override void UpdateState()
        {
            //Debug.Log("update move state");

        }

        public override void FixedUpdateState()
        {
            
            CheckSwitchState();

        }
        public override void ExitState()
        {
            //Debug.Log("exit move state");
        }
        public override void CheckSwitchState()
        {
            if (IsInFOV(_cntx.targetPlayer))
            {
                SwitchState(_factory.Chase());
            }


        }
        public void ChangeColor(Color color)
        {
            _cntx.spriteRenderer.color = color;
        }

        public bool IsInFOV(Transform target)
        {
            Vector2 directionToTarget = target.position - _cntx.transform.position;
            float distance = directionToTarget.magnitude;
            //Debug.Log("player distance:"+ distance);

            if (distance > _cntx.detectionRange)//check target is in distance
                return false;
            Vector2 forward = _cntx.transform.right * Mathf.Sign(_cntx.transform.localScale.x);

            // Calculate angle between forward and direction to target
            float angle = Vector2.Angle(forward, directionToTarget);

            return angle < (_cntx.fovAngle / 2f);
        }
        public void MoveTowardsTargetPosition(Vector2 target, float speed)
        {
            if (Mathf.Abs(target.x- _cntx.transform.position.x) < _cntx.catchDistance)//calculate x distance for grounded mob (abs for both sides)
            {
                //stand
                _cntx.selfRb.velocity = Vector3.zero;
                _cntx.animator.SetFloat("speed", 0);
                if(Vector2.Distance(_cntx.transform.position, target)<_cntx.attackDistance)
                {
                    Debug.Log("attack!");
                    SwitchState(_factory.Attack());
                }
            }
            else
            {
                _cntx.selfRb.velocity = Vector3.MoveTowards(_cntx.selfRb.velocity, new Vector3(_cntx.transform.localScale.x * speed, _cntx.selfRb.velocity.y, 0f), speed * 0.3f);
                _cntx.animator.SetFloat("speed", 1);
            }
            if (Mathf.Sign(_cntx.transform.position.x - target.x) != Mathf.Sign(-_cntx.transform.localScale.x))
                _cntx.Flip();
            
        }
        public void Move()
        {
            //Debug.Log(" Moving");
            _cntx.selfRb.velocity = Vector3.MoveTowards(_cntx.selfRb.velocity, new Vector3(_cntx.transform.localScale.x * _cntx.patrolSpeed, _cntx.selfRb.velocity.y, 0f), _cntx.patrolSpeed * 0.3f);
            _cntx.animator.SetFloat("speed", 1);
            if ((!_cntx.LedgeCheck() || _cntx.WallCheck()) || !_cntx.GroundCheck())
            {
                _cntx.Flip();
            }
        }
        public bool RandomChance(float percentage)
        {
            return Random.Range(0f, 100f) < percentage;
        }

    }
}


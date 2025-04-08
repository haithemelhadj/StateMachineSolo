using UnityEngine;

namespace StateMachine
{
    public class _PlayerAttackState : _PlayerBaseState
    {
        public _PlayerAttackState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
            Attack();
        }
        public override void UpdateState()
        {
            CheckSwitchState();
        }
        public override void FixedUpdateState()
        {

        }
        public override void ExitState()
        {

        }
        public override void CheckSwitchState()
        {

        }


        public void Attack()
        {

            if (_cntx.verticalInput != 0)
            {
                if (_cntx.verticalInput < 0 && _cntx.isGrounded) return;

                //attack vertically
                _cntx.atkDistance = Mathf.Sign(_cntx.verticalInput) * (_cntx.playerHeight / 2 + _cntx.atkRange);
                _cntx.atkPosition = new Vector2(_cntx.transform.position.x, _cntx.transform.position.y + _cntx.atkDistance);
                _cntx.atkRotation = 90f * Mathf.Sign(_cntx.verticalInput) * Mathf.Sign(_cntx.transform.localScale.x);//rotation is based on localScale.x
            }
            else
            {
                //attack horizontally
                _cntx.atkDistance = Mathf.Sign(_cntx.transform.localScale.x) * (_cntx.playerWidth / 2 + _cntx.atkRange);
                _cntx.atkPosition = new Vector2(_cntx.transform.position.x + _cntx.atkDistance, _cntx.transform.position.y);
                _cntx.atkRotation = 0f;
            }
            //set position and rotation
            _cntx.atkObj.transform.position = _cntx.atkPosition;
            _cntx.atkObj.transform.eulerAngles = new Vector3(0f, 0f, _cntx.atkRotation);
            //attack and disable attack after attackTime
            _cntx.atkObj.SetActive(true);
            _cntx.atkAnimator.SetBool("Attack", true);
            _cntx.Invoke(nameof(_cntx.StopAttacking), _cntx.atkTime);

        }



    }
}

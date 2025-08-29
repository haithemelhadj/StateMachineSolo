using UnityEngine;
using StateMachine;

[CreateAssetMenu(fileName = "WallJump State", menuName = "Player/States/WallJump")]

    public class _PlayerWallJumpState : _PlayerJumpState
    {
        public _PlayerWallJumpState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
        public override void EnterState()
        {
            base.EnterState();
        }
        public override void UpdateState()
        {
            //base.UpdateState();
            _cntx.isWallJumping = Time.time - _cntx.wallJumpPressTime < _cntx.wallJumpDuration;
            _cntx.jumpDirection = new Vector2(-_cntx.transform.localScale.x * _cntx.wallJumpDirection.x, _cntx.wallJumpDirection.y);
            _cntx.playerAnimator.SetBool("isJumping", true);
            Jumping(_cntx.jumpDirection);
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
            base.CheckSwitchState();
            if (_cntx.jumpInputUp)
            {
                SwitchState(_factory.GetState(_States.Fall));
            }
            if (!_cntx.isWallJumping)
            {
                SwitchState(_factory.GetState(_States.Jump));

            }
            if (_cntx.dashInputDown)
            {
                SwitchState(_factory.GetState(_States.Dash));
            }
        }

        /*
        public void Jumping(Vector2 JumpDirection)
        {
            _cntx.playerRb.velocity = JumpDirection;
        }
        /**/
    }

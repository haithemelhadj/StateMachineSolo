
using UnityEngine;
using StateMachine;

[CreateAssetMenu(fileName = "Jump State", menuName = "Player/States/Jump")]
public class _PlayerJumpState : _PlayerMovementState
    {
        //private bool _hasAppliedJump;
        //private bool _isWallJumping;

        public _PlayerJumpState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
            base.EnterState();
            //set jump start counter
            _cntx.willBufferJump = false;
            _cntx.jumpTimeCounter = _cntx.maxJumpTime;
            //animator
            _cntx.playerAnimator.SetBool("isJumping", true);
            //null y velocity
            _cntx.playerRb.velocity = new Vector2(_cntx.playerRb.velocity.x, 0f);

            // Apply initial jump impulse

            Jump();

            //.
            //set air movement speed
            //_cntx.c_MaxHSpeed = _cntx.f_MaxHSpeed;
            //_cntx.c_Acceleration = _cntx.f_Acceleration;
            //_cntx.c_Deceleration = _cntx.f_Deceleration;
        }
        public override void UpdateState()
        {
            base.UpdateState();
            _cntx.jumpTimeCounter -= Time.deltaTime;
            CheckSwitchState();
        }

        /*
        public override void FixedUpdateState()
        {
            base.FixedUpdateState();

            if (_cntx.playerRb.velocity.y > 0f) // going up
            {
                if (_cntx.jumpInput && _cntx.jumpTimeCounter > 0)
                {
                    // Reduce gravity while holding for longer jump
                    _cntx.playerRb.gravityScale = _cntx.lowGravityScale;
                    _cntx.jumpTimeCounter -= Time.fixedDeltaTime;
                }
                else
                {
                    // Normal or increased gravity for early release
                    _cntx.playerRb.gravityScale = _cntx.normalGravityScale;
                }
            }
            else // falling
            {
                _cntx.playerRb.gravityScale = _cntx.fasterFallMultiplier;
            }

            CheckSwitchState();
        }
        /**/

        /*

        public override void FixedUpdateState()
        {
            // Apply jump ONCE at the start
            if (!_hasAppliedJump)
            {
                if (_cntx.isWallJumping)
                {
                    // Lock movement for short time after wall jump
                    _cntx.playerRb.velocity = new Vector2(
                        -_cntx.transform.localScale.x * _cntx.wallJumpDirection.x,
                        _cntx.wallJumpDirection.y
                    );
                }
                else
                {
                    // Normal jump
                    _cntx.playerRb.velocity = new Vector2(_cntx.playerRb.velocity.x, _cntx.jumpForce);
                }
                _hasAppliedJump = true;
            }

            // Handle variable jump height
            if (!_cntx.jumpInput || _cntx.jumpTimeCounter <= 0)
            {
                // Apply extra gravity for snappy short hop
                _cntx.playerRb.velocity += Vector2.up * Physics2D.gravity.y * (_cntx.shortHopMultiplier - 1f) * Time.fixedDeltaTime;
            }

            _cntx.jumpTimeCounter -= Time.fixedDeltaTime;

            // Allow air movement ONLY if not locked by wall jump
            if (!_cntx.isWallJumping || Time.time - _cntx.wallJumpPressTime >= _cntx.wallJumpDuration)
            {
                base.FixedUpdateState();
            }

            CheckSwitchState();
        }
        /**/


        /**/
        public override void FixedUpdateState()
        {
            //Jump();
            base.FixedUpdateState();
        }

        private void Jump()
        {
            //wall jumping + can't move 
            if (Time.time - _cntx.wallJumpPressTime < _cntx.wallJumpDuration)
            {
                _cntx.jumpDirection = new Vector2(-_cntx.transform.localScale.x * _cntx.wallJumpDirection.x, _cntx.wallJumpDirection.y);
            }
            else // else if is normal jumping
            {
                _cntx.jumpDirection = new Vector2(_cntx.playerRb.velocity.x, _cntx.jumpForce);
            }

            _cntx.playerRb.velocity = _cntx.jumpDirection;// * Time.deltaTime;
        }

        /**/

        public override void ExitState()
        {
            base.ExitState();
            _cntx.willBufferJump = false;
            _cntx.playerAnimator.SetBool("isJumping", false);

            //_cntx.c_MaxHSpeed = _cntx.j_MaxHSpeed;
            //_cntx.c_Acceleration = _cntx.j_Acceleration;
            //_cntx.c_Deceleration = _cntx.j_Deceleration;


            _cntx.playerRb.velocity = new Vector2(_cntx.playerRb.velocity.x, _cntx.playerRb.velocity.y * 0.5f);
        }
        public override void CheckSwitchState()
        {
            base.CheckSwitchState();
            if (!_cntx.jumpInput || _cntx.jumpTimeCounter < 0 || _cntx.isHeadBumping)
            {
                SwitchState(_factory.GetState(_States.Fall));
            }
        }

        public void Jumping(Vector2 JumpDirection)
        {

        }
    }

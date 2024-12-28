using UnityEngine;

namespace StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public PlayerBaseState _currentState;
        //public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

        PlayerStateFactory _states;

        //current

        private void Awake()
        {
            GetComponents();
            InitializeState();
            //max falling speed is always negative
            maxFallSpeed = -Mathf.Abs(maxFallSpeed);
        }


        private void Update()
        {
            //rays casts
            GroundCheck();
            HeadCheck();
            WallCheck();
            //inputs
            GetHInputs();
            GetVInputs();
            GetJumpInput();
            //actions

            //
            JumpBuffer();
            //logic
            _currentState.UpdateState();
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdateState();
        }
        //Awake
        #region Get Components
        [Header("Components")]
        public Rigidbody2D playerRb;
        public CapsuleCollider2D capsuleCollider;
        //public Animator playerAnimator;
        public float playerHeight;
        public float playerWidth;
        public void GetComponents()
        {
            playerRb = GetComponent<Rigidbody2D>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();
            //playerAnimator = GetComponent<Animator>();
            playerWidth = capsuleCollider.size.x * transform.localScale.x;
            playerHeight = capsuleCollider.size.y * transform.localScale.y;
        }
        #endregion
        private void InitializeState()
        {
            _states = new PlayerStateFactory(this);
            _currentState = _states.Grounded();
            _currentState.EnterState();
        }

        //Movement
        #region Movement Input
        [Header("Movement Inputs")]
        public float horizontalInput;
        public void GetHInputs()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }

        public float verticalInput;
        public void GetVInputs()
        {
            verticalInput = Input.GetAxisRaw("Vertical");
        }
        #endregion

        #region  Current Movement 
        [Header("Current Movement")]
        public float c_MaxHSpeed;
        public float c_Acceleration;
        public float c_Deceleration;
        #endregion

        #region  Ground Movement 
        [Header("Ground Movement")]
        public float g_MaxHSpeed;
        public float g_Acceleration;
        public float g_Deceleration;
        #endregion

        //checks
        #region Ground check
        [Header("Ground check")]
        public LayerMask whatIsGround;
        public bool isGrounded;
        public float extraGroundCheckDistance = 0.01f;

        public void GroundCheck()
        {
            //send 2 raycast at the limits of the player's feet to check if the player is grounded
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(playerWidth / 2, 0, 0), Vector2.down, playerHeight / 2 + extraGroundCheckDistance, whatIsGround);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(playerWidth / 2, 0, 0), Vector2.down, playerHeight / 2 + extraGroundCheckDistance, whatIsGround);
            if (hitLeft.collider != null || hitRight.collider != null)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        #endregion

        #region Head Check
        [Header("Head Check")]
        //public LayerMask whatIsOverHead;
        public bool isHeadBumped;
        public float extraHeadCheckDistance = 0.01f;

        public void HeadCheck()
        {
            //send 2 raycast at the limits of the player's head to check if the players has hit a ceiling
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(playerWidth / 2, 0, 0), Vector2.up, playerHeight / 2 + extraHeadCheckDistance, whatIsGround);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(playerWidth / 2, 0, 0), Vector2.up, playerHeight / 2 + extraHeadCheckDistance, whatIsGround);
            if (hitLeft.collider != null || hitRight.collider != null)
            {
                //ceiling
                //check if both are diffrent
                if (hitLeft == hitRight)
                {
                    //push the player to the side that is false to exactly fit 
                }
                //else do nothing
            }
            else
            {
                //no ceiling
            }
        }
        #endregion

        #region Wall Detection
        [Header("Wall Check")]
        public LayerMask whatIsWall;
        public bool isHuggingWall = false;
        public float LastTimeWalled;


        public void WallCheck()
        {
            isHuggingWall = WallDetectionUpper() || WallDetectionMiddle() || WallDetectionLower();//wallSlideScript.isWallSliding
            if (isHuggingWall)
            {
                canJump = true;
                LastTimeWalled = Time.time;
            }
        }
        public bool WallDetectionUpper()
        {
            return Physics2D.Raycast(transform.position + new Vector3(0, playerHeight / 2, 0), new Vector2(transform.localScale.x, 0f), playerWidth / 2 + extraGroundCheckDistance, whatIsWall);
        }
        public bool WallDetectionMiddle()
        {
            return Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0f), playerWidth / 2 + extraGroundCheckDistance, whatIsWall);
        }
        public bool WallDetectionLower()
        {
            return Physics2D.Raycast(transform.position - new Vector3(0, playerHeight / 2, 0), new Vector2(transform.localScale.x, 0f), playerWidth / 2 + extraGroundCheckDistance, whatIsWall);
        }

        #endregion

        //AirBorne Movement
        #region  Air Movement 
        [Header("Air Movement ")]
        public float a_MaxHSpeed;
        public float a_Acceleration;
        public float a_Deceleration;
        #endregion

        #region  Jump Apex Movement 
        [Header("Jump Apex Movement ")]
        public float j_MaxHSpeed;
        public float j_Acceleration;
        public float j_Deceleration;
        #endregion


        //Jump        
        #region Jump Input
        [Header("Jump Inputs")]
        public KeyCode jumpKey;
        public bool jumpInput;
        public bool jumpInputDown;
        public bool jumpInputUp;

        public void GetJumpInput()
        {
            jumpInput = Input.GetKey(jumpKey);
            jumpInputDown = Input.GetKeyDown(jumpKey);
            jumpInputUp = Input.GetKeyUp(jumpKey);
            if (jumpInputDown)
            {
                jumpPressTime = Time.time;
                willBufferJump = true;
            }
        }
        #endregion

        #region  Jump Values
        [Header("Jump")]
        public float jumpForce;
        public bool jumpInputConfirmed;
        public bool jumpReset;
        #endregion

        #region Variable Jump
        [Header("Variable Jump")]
        public float jumpTimeCounter;
        public float jumpTime;
        //public bool isJumping;
        public void VariableJump()
        {
            if (jumpInputUp)
            {
                //isJumping = false;
                //set jumping animation
                //playerAnimator.SetBool("isJumping", isJumping);
                isWallJumping = false;
            }
        }
        #endregion

        #region Jump Buffer
        [Header("Jump Buffer")]
        public float jumpPressTime;
        public float jumpBufferTime;
        public bool willBufferJump;
        public void JumpBuffer()
        {
            if (Time.time - jumpPressTime > jumpBufferTime)
            {
                willBufferJump = false;
            }
        }
        #endregion

        #region Cyote Time
        [Header("Cyote Time")]
        public float LastGrounded;
        public float cyoteTime;
        public bool canJump;

        #endregion

        #region wall Jump
        [Header("wall Jump")]
        public float wallJumpDuration;
        public float wallJumpPressTime;
        public bool isWallJumping;
        public Vector2 jumpDirection;
        public Vector2 wallJumpDirection;

        #endregion


        //fall 
        #region Fall Controll
        [Header("Fall Controll")]
        public float maxFallSpeed;
        public float fasterFallMultiplier;
        public float jumpApexThreshhold;
        public float jumpApexGravityMultiplier;
        #endregion
        //Actions

    
        #region
        #endregion
    }
}
/*
 * add H movement parallel state
 * add jump apex lerp and bonus movement
 * add actions 
 */

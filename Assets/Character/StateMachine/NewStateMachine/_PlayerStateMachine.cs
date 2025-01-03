
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace StateMachine
{
    public class _PlayerStateMachine : MonoBehaviour
    {
        public _PlayerBaseState _currentState;

        _PlayerStateFactory _states;

        public string currentSuperState;
        public string currentSubState;
        
        //Awake
        private void InitializeState()
        {
            _states = new _PlayerStateFactory(this);
            //Debug.Log("SM awake mvt");
            //_currentState = _states.Movement();            
            _currentState = _states.Grounded();            
            _currentState.EnterState();
        }

        private void Awake()
        {
            GetComponents();
            InitializeState();
            //max falling speed is always negative
            maxFallSpeed = -Mathf.Abs(maxFallSpeed);
        }
        //Update
        private void Update()
        {
            //rays casts
            if (!isLedgeBumping)            
                GroundCheck();
            HeadCheck();
            WallCheck();
            //inputs
            GetHInputs();
            GetVInputs();
            GetJumpInput();
            //actions
            GetDashInput();
            GetAttackInput();
            GetInterractionInput();

            //
            //JumpBuffer();
            //logic
            _currentState.UpdateStates();
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdateState();
        }




        //Get Componenets
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
            isHuggingWall = WallDetectionUpper() || WallDetectionMiddle() || WallDetectionLower();
            if (isHuggingWall)
            {
                //canCyoteJump = true;
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
        public bool isJumping;

        public void GetJumpInput()
        {
            jumpInput = Input.GetKey(jumpKey);
            jumpInputDown = Input.GetKeyDown(jumpKey);
            jumpInputUp = Input.GetKeyUp(jumpKey);            
            if (jumpInputUp)
            {
                willBufferJump = false;
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
        #endregion

        #region Jump Buffer
        [Header("Jump Buffer")]
        public float jumpPressTime;
        public float jumpBufferTime;
        public bool willBufferJump;
        public void JumpBuffer()
        {
            /*
            if (Time.time - jumpPressTime > jumpBufferTime || jumpInputUp)
            {
                willBufferJump = false;
            }
            /**/
        }
        #endregion

        #region Cyote Time
        [Header("Cyote Time")]
        public float LastGrounded;
        public float cyoteTime;
        public bool canCyoteJump;

        #endregion

        //Wall
        #region wall Jump
        [Header("wall Jump")]
        public float wallJumpDuration;
        public float wallJumpPressTime;
        public bool isWallJumping;
        public Vector2 jumpDirection;
        public Vector2 wallJumpDirection;

        #endregion

        #region Wall Slide

        [Header("Wall Slide")]
        public bool isWallSliding;
        public float wallSlidingSpeed;
        #endregion

        //fall 
        #region Fall Controll
        [Header("Fall Controll")]
        public float maxFallSpeed;
        public float fasterFallMultiplier;
        //public float jumpApexThreshhold;
        //public float jumpApexGravityMultiplier;
        //public float fallMultiplier;
        #endregion

        //Actions        

        #region Dash
        [Header("Dash")]
        public float dashForce;
        public float dashTime;
        public bool canDash;
        public bool isDashing;
        public float dashDirection;
        public KeyCode dashKey;
        public bool dashInputDown;
        public void GetDashInput()
        {
            dashInputDown = Input.GetKeyDown(dashKey);
        }
        public IEnumerator Dash()
        {
            Debug.Log("starting dash");
            //set vars
            canDash = false;
            isDashing = true;
            //playerAnimator.SetBool("Dashing", isDashing);
            //save gravity
            float originalGravity = playerRb.gravityScale;
            playerRb.gravityScale = 0f;
            playerRb.constraints.Equals(RigidbodyConstraints2D.FreezePositionY);
            //set air friction 
            //float originalDrag = inputsScript.playerRb.drag;
            //inputsScript.playerRb.drag = drag;
            //stop jumping
            isJumping = false;
            //set jumping animation to stop
            //playerAnimator.SetBool("isJumping", isJumping);
            //null velocity
            playerRb.velocity = Vector2.zero;
            //set dash direction if is wall sliding
            if (isWallSliding)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
            //dash 
            playerRb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * dashForce, 0f);
            yield return new WaitForSeconds(dashTime);
            //reset everything
            playerRb.constraints.Equals(RigidbodyConstraints2D.None);
            playerRb.constraints.Equals(RigidbodyConstraints2D.FreezePosition);
            playerRb.drag = 0f;
            playerRb.gravityScale = originalGravity;
            //playerRb.drag = originalDrag;
            isDashing = false;
            //playerAnimator.SetBool("Dashing", isDashing);
            yield return new WaitForSeconds(dashTime);

        }
        #endregion

        #region Attack
        [Header("Attack")]
        public KeyCode attackKey;
        public bool AttackInput;
        //attack refrences
        public GameObject atkObj;
        public Animator atkAnimator;
        //attack variables
        public float atkRange;
        public float atkTime;
        [HideInInspector] public float atkDistance;
        [HideInInspector] public Vector2 atkPosition;
        [HideInInspector] public float atkRotation;
        public void GetAttackInput()
        {
            AttackInput = Input.GetKeyDown(attackKey);
        }
        /*
        public void AttackInput()
        {
            if (AttackInput)
            {
                if (verticalInput != 0)
                {
                    //attack vertically
                    atkDistance = Mathf.Sign(verticalInput) * (playerHeight / 2 + atkRange);
                    atkPosition = new Vector2(transform.position.x, transform.position.y + atkDistance);
                    atkRotation = 90f * Mathf.Sign(verticalInput) * Mathf.Sign(transform.localScale.x);//rotation is based on localScale.x
                }
                else
                {
                    //attack horizontally
                    atkDistance = Mathf.Sign(transform.localScale.x) * (playerWidth / 2 + atkRange);
                    atkPosition = new Vector2(transform.position.x + atkDistance, transform.position.y);
                    atkRotation = 0f;
                }
                //set position and rotation
                atkObj.transform.position = atkPosition;
                atkObj.transform.eulerAngles = new Vector3(0f, 0f, atkRotation);
                //attack and disable attack after attackTime
                atkObj.SetActive(true);
                atkAnimator.SetBool("Attack", true);
                Invoke(nameof(StopAttacking), atkTime);
            }
        }
        public void StopAttacking()
        {
            atkAnimator.SetBool("Attack", false);
            atkObj.SetActive(false);

        }
        /**/

        #endregion

        #region Interract
        [Header("Interract")]
        public KeyCode InterractionKey;
        
        public void GetInterractionInput()
        {

        }
        #endregion

        #region Ledge Bump
        [Header("Ledge Bump")]
        public float bumpForce;
        public bool isLedgeBumping;
        public float bumpTime;        
        #endregion







        #region
        #endregion
    }
}
/*
 * add H movement parallel state
 * add jump apex lerp and bonus movement
 * add actions 
 * add animations
 */

//make wall jump on the same wall can go higher like HK = make player can move during jump and can jump heigher 
//add new mechanic to wall slide is that the player can run to the wall and gain extra hight and jump distance 
//wall jump doesnt continue jumping like a normal jump = wall jumping only adds X force and player jumps normally
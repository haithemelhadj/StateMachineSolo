using UnityEngine;

public class _PlayerStateMachine : MonoBehaviour
{
    _PlayerStateFactory _states;
    //current state
    public _PlayerBaseState _currentState;
    //parallel state
    //public _PlayerBaseState _currentParallelState;
    //_PlayerBaseState attackParallelState;
    //_PlayerBaseState iFramesParallelState;
    public _PlayerStateConfig playerStates;
    [Header("visualising")]
    public string currentActiveState;

    //Awake
    private void InitializeState()
    {
        _states = new _PlayerStateFactory(this, playerStates);
        _currentState = _states.GetState(_States.Grounded);
        //attackParallelState = _states.Attack();
        //iFramesParallelState = _states.iFrames();
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
        Checks();

        //inputs
        GetMovementInputs();
        GetDashInput();
        GetActionInputs();

        //states logic
        StatesLogicHandler();
    }
    private void FixedUpdate()
    {
        _currentState.FixedUpdateState();
    }


    #region Update Methods
    private void StatesLogicHandler()
    {
        _currentState.UpdateState();
        ParallelStatesHandler();
    }
    private void ParallelStatesHandler()
    {
        if (AttackInputDown)
        {
            //attackParallelState.EnterState();
            //Debug.Log("attacking");
        }
    }
    public void GetActionInputs()
    {

        GetAttackInput();
        GetInterractionInput();
    }

    public void GetMovementInputs()
    {
        GetWalkSpeedInput();
        GetHInputs();
        GetVInputs();
        GetJumpInput();
    }

    private void Checks()
    {
        GroundCheck();
        HeadCheck();
        WallCheck();
    }

    #endregion



    //Get Componenets
    #region Get Components
    [Header("Components")]
    [SerializeField] public Rigidbody2D playerRb;
    [SerializeField] public CapsuleCollider2D capsuleCollider;
    [SerializeField] public Animator playerAnimator;
    [SerializeField] public float playerHeight;
    [SerializeField] public float playerWidth;
    public void GetComponents()
    {
        playerRb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<Animator>();
        playerWidth = capsuleCollider.size.x * transform.localScale.x;
        playerHeight = capsuleCollider.size.y * transform.localScale.y;
    }
    #endregion

    //Movement
    #region Movement Input
    [Header("Movement Inputs")]
    [SerializeField] public float horizontalInput;

    public void GetHInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        float x = Mathf.Abs(horizontalInput) * c_MaxHSpeed;
        playerAnimator.SetFloat("HInput", x);
    }

    public float verticalInput;
    public void GetVInputs()
    {
        verticalInput = Input.GetAxis("Vertical");
        playerAnimator.SetFloat("YInput", verticalInput);
    }
    #endregion

    #region  Current Movement 
    [Header("Current Movement")]
    [SerializeField] public float c_HSpeed;
    [SerializeField] public float c_MaxHSpeed;
    [SerializeField] public float c_Acceleration;
    [SerializeField] public float c_Deceleration;
    #endregion

    #region  Ground Movement 
    [Header("run Movement")]
    public float r_MaxHSpeed;
    public float r_Acceleration;
    public float r_Deceleration;
    #endregion

    #region  Ground Movement 
    [Header("walk Movement")]
    public float w_MaxHSpeed;
    public float w_Acceleration;
    public float w_Deceleration;
    #endregion

    //checks
    #region Ground check
    [Header("Ground check")]
    public LayerMask whatIsGround;
    [SerializeField] public bool isGrounded;
    [SerializeField] public bool isHeadBumping;
    public float extraGroundCheckDistance = 0.01f;

    public void GroundCheck()
    {
        //send 2 raycast at the limits of the player's feet to check if the player is grounded
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(playerWidth / 2, 0, 0), Vector2.down, playerHeight / 2 + extraGroundCheckDistance, whatIsGround);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(playerWidth / 2, 0, 0), Vector2.down, playerHeight / 2 + extraGroundCheckDistance, whatIsGround);
        if (hitLeft.collider != null || hitRight.collider != null)
        {
            isGrounded = true;
            dashReset = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    void OnDrawGizmos()
    {
        Vector3 rightRayOrigin = transform.position + new Vector3(playerWidth / 2, 0, 0);
        Vector3 leftRayOrigin = transform.position - new Vector3(playerWidth / 2, 0, 0);

        float rayLength = playerHeight / 2 + extraGroundCheckDistance;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(rightRayOrigin, rightRayOrigin + Vector3.down * rayLength);
        Gizmos.DrawLine(leftRayOrigin, leftRayOrigin + Vector3.down * rayLength);
    }

    #endregion

    #region Head Check
    [Header("Head Check")]
    [SerializeField] public bool isHeadBumped;
    public float extraHeadCheckDistance = 0.01f;

    public void HeadCheck()
    {
        //send 2 raycast at the limits of the player's head to check if the players has hit a ceiling
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(playerWidth / 2, 0, 0), Vector2.up, playerHeight / 2 + extraHeadCheckDistance, whatIsGround);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(playerWidth / 2, 0, 0), Vector2.up, playerHeight / 2 + extraHeadCheckDistance, whatIsGround);
        if (hitLeft.collider != null || hitRight.collider != null)
        {
            //ceiling
            isHeadBumping = true;
            //check if both are diffrent
            if (hitLeft != hitRight)
            {
                //push the player to the side that is false to exactly fit 
            }
            //else do nothing
        }
        else
        {
            //no ceiling
            isHeadBumping = false;
        }
    }
    #endregion

    #region Wall Detection
    [Header("Wall Check")]
    public LayerMask whatIsWall;
    [SerializeField] public bool isHuggingWall = false;
    [SerializeField] public float LastTimeWalled;
    public void WallCheck()
    {
        isHuggingWall = (WallDetectionUpper() && WallDetectionMiddle()) || (WallDetectionLower() && WallDetectionMiddle());
        if (isHuggingWall)
        {
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

    #region walk Speed Input

    public KeyCode walkInput;
    public bool walkInputDown;
    public bool holdToWalk;
    bool iswalking = false;

    public void GetWalkSpeedInput()
    {
        if (holdToWalk)
        {

            //Hold to walk
            if (Input.GetKey(walkInput))
            {
                c_MaxHSpeed = w_MaxHSpeed;
                c_Acceleration = w_Acceleration;
                c_Deceleration = w_Deceleration;
            }
            else
            {
                c_MaxHSpeed = r_MaxHSpeed;
                c_Acceleration = r_Acceleration;
                c_Deceleration = r_Deceleration;
            }
        }
        else
        {
            //press to switch between states
            if (Input.GetKeyDown(walkInput))
            {
                iswalking = !iswalking;
            }
            if (iswalking)
            {
                c_MaxHSpeed = w_MaxHSpeed;
                c_Acceleration = w_Acceleration;
                c_Deceleration = w_Deceleration;
            }
            else
            {
                c_MaxHSpeed = r_MaxHSpeed;
                c_Acceleration = r_Acceleration;
                c_Deceleration = r_Deceleration;
            }
        }
    }





    #endregion

    #region animator
    public void InitAnimatorVals()
    {

    }

    #endregion


    //AirBorne Movement
    #region  Air Movement 
    //[Header("falling  Movement ")]
    //public float f_MaxHSpeed;
    //public float f_Acceleration;
    //public float f_Deceleration;
    #endregion

    #region  Jump Apex Movement 
    //[Header("Jump Apex Movement ")]
    //public float j_MaxHSpeed;
    //public float j_Acceleration;
    //public float j_Deceleration;
    #endregion


    //Jump
    #region Jump Input
    [Header("Jump Inputs")]
    public KeyCode jumpKey;
    [SerializeField] public bool jumpInput;
    [SerializeField] public bool jumpInputDown;
    [SerializeField] public bool jumpInputUp;
    [SerializeField] public bool isJumping;

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
    [SerializeField] public bool jumpInputConfirmed;
    [SerializeField] public bool jumpReset;
    #endregion

    #region Variable Jump
    [Header("Variable Jump")]
    public float maxJumpTime;
    [SerializeField] public float jumpTimeCounter;
    #endregion

    #region Jump Buffer
    [Header("Jump Buffer")]
    public float jumpBufferTime;
    [SerializeField] public float jumpPressTime;
    [SerializeField] public bool willBufferJump;
    #endregion

    #region Cyote Time
    [Header("Cyote Time")]
    public float cyoteTime;
    [SerializeField] public float LastGrounded;
    [SerializeField] public bool canCyoteJump;

    #endregion


    #region Fall Controll
    [Header("Fall Controll")]
    public float maxFallSpeed;
    public float fasterFallMultiplier;
    #endregion

    //wall

    #region wall Jump
    [Header("wall Jump")]
    public float wallJumpDuration;
    [SerializeField] public float wallJumpPressTime;
    [SerializeField] public bool isWallJumping;
    [SerializeField] public Vector2 jumpDirection;
    public Vector2 wallJumpDirection;

    #endregion

    #region Wall Slide

    [Header("Wall Slide")]
    public float wallSlidingSpeed;
    [SerializeField] public bool isWallSliding;
    #endregion



    //Actions        
    #region Dash
    [Header("Dash")]
    //public bool canDash;
    public KeyCode dashKey;
    [SerializeField] public bool dashInputDown;
    public void GetDashInput()
    {
        dashInputDown = Input.GetKeyDown(dashKey);
    }

    public float dashForce;
    public float dashTime;
    public float dashCd;

    [SerializeField] public float lastDashFinishTime;
    [SerializeField] public bool dashReset;
    [SerializeField] public float dashDirection;
    [SerializeField] public bool isDashing;
    [SerializeField] public float lastDash;
    [SerializeField] public bool dashCounter;

    public bool canDashCheck()
    {
        //if already dashed give time to redash
        if (Time.time - lastDashFinishTime > dashCd)
        {
            //if dashed in air can't dash again until grounded
            if (dashReset)
            {
                //if is dashing can't dash mid dash
                if (!isDashing)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void DashLimiting()
    {
        //on grounded dashcounter=1
        //on dash dashCounter=0
        //on dash lastDash=timt.time
        //on dash press if(dashcd) ==> dash
        dashCounter = false;
        if (isGrounded) dashCounter = true;

    }

    #endregion

    #region Attack
    [Header("Attack")]
    public KeyCode attackKey;
    [SerializeField] public bool AttackInputDown;
    //attack refrences
    public GameObject atkObj;
    [SerializeField] public Animator atkAnimator;
    //attack variables
    public float atkRange;
    public float atkTime;
    [SerializeField] public float atkDistance;
    [SerializeField] public Vector2 atkPosition;
    [SerializeField] public float atkRotation;
    public void GetAttackInput()
    {
        AttackInputDown = Input.GetKeyDown(attackKey);
    }
    public void StopAttack()
    {
        playerAnimator.SetBool("Attack", false);
        atkObj.SetActive(false);

    }
    #endregion

    #region Defend And Parry
    public KeyCode defendKey;
    public bool defendInput;
    public bool defendInputDown;
    public bool defendInputUp;
    public GameObject parryObj;
    public GameObject defendObj;

    public void GetDefendInput()
    {
        defendInput = Input.GetKey(defendKey);
        defendInputDown = Input.GetKeyDown(defendKey);
        defendInputUp = Input.GetKeyUp(defendKey);

    }
    public void StartParry()
    {
        playerAnimator.SetBool("Parry", true);

        parryObj.SetActive(true);
    }
    public void StopParry()
    {
        playerAnimator.SetBool("Parry", false);

        parryObj.SetActive(false);
    }
    public void StartDefend()
    {
        playerAnimator.SetBool("Defend", true);
        defendObj.SetActive(true);
    }
    public void StopDefend()
    {
        playerAnimator.SetBool("Defend", false);
        defendObj.SetActive(false);
    }


    #endregion

    #region

    #endregion

    #region Interract
    [Header("Interract")]
    public KeyCode InterractionKey;
    public bool Interraction;

    public void GetInterractionInput()
    {
        Interraction = Input.GetKeyDown(InterractionKey);
    }
    #endregion


    //other
    #region Ledge Bump
    [Header("Ledge Bump")]
    public float bumpForce;
    public float bumpTime;
    [SerializeField] public bool isLedgeBumping;
    #endregion

    #region Getting Hit
    [Header("Getting Hit")]
    public float knockBackforce;
    public float yPushForce;
    #endregion

    #region i-Frames
    [Header("i-Frames")]
    public float iFramesDuration;
    public float lastHitTime;
    public bool isInIframes;

    public void ExitIFrames()
    {
        isInIframes = false;
        playerAnimator.SetBool("Getting Hit", false);
    }

    #endregion



    #region collsion management
    [Header("")]
    [Header("collsion management")]
    public LayerMask enemyLayers;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if ((collision.gameObject.layer==enemyLayers))

        if (collision.gameObject.CompareTag("Enemy") && !isInIframes)
        {
            Debug.Log("got hit");
            //getting hit knock back 
            playerRb.velocity = Vector3.zero;
            Vector2 knockBackDirection = (transform.position - collision.transform.position).normalized;
            Debug.Log("knock back dir: " + knockBackDirection);
            knockBackDirection = knockBackDirection * knockBackforce;
            knockBackDirection.y = yPushForce;
            playerRb.AddForce(knockBackDirection, ForceMode2D.Impulse);
            //trigger animation
            playerAnimator.SetBool("Getting Hit", true);
            //iFramesParallelState.EnterState();
        }
    }

    #endregion
}

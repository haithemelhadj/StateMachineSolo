using UnityEngine;

public class Context : MonoBehaviour
{

    public void ContextStart()
    {
        //Get Components
        GetComponents();
    }
    public void ContextUpdate()
    {
        GetInputs(inputsHandler);
        Checks();
    }

    #region Refrences
    [Header("-----REFRENCES-----")]
    public AnimatorController animatorController;
    public InputsHandler inputsHandler;
    #endregion

    #region Get Components
    [Header("-----GEt COMPONENTS-----")]
    public Rigidbody2D Rb;
    public CapsuleCollider2D capsuleCollider;
    public Animator Animator;
    public void GetComponents()
    {
        Rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Animator = GetComponent<Animator>();
        Width = capsuleCollider.size.x * transform.localScale.x;
        Height = capsuleCollider.size.y * transform.localScale.y;
        localScale = transform.localScale;
    }
    #endregion

    #region Values
    [Header("-----VALUES-----")]
    public bool HoldToWalk;
    public float Height;
    public float Width;
    public Vector3 localScale;
    public LayerMask whatIsGround;
    public float extraGroundCheckDistance = 0.01f;
    public float extraHeadCheckDistance = 0.01f;
    #endregion



    #region Flags
    [Header("-----FLAGS-----")]
    public bool isFacingRight;
    public bool isHeadBumping;
    public bool canDoubleJump;
    public bool isTouchingWall;
    public bool isWallSliding;
    public bool isWallJumping;
    public bool isDashing;
    #endregion

    #region Get Inputs

    #region KeyCodes & Flags

    [Header("-----Input keyCodes-----")]
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public KeyCode speedChangeKey = KeyCode.C;
    public KeyCode interactionKey = KeyCode.E;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.LeftShift;
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode defendKey = KeyCode.Mouse1;

    [Header("-----Input FLAGS-----")]
    public float hInput;
    public float vInput;
    //public bool speedChangeInputDown;
    public bool walkSpeedInput;
    public bool interactionInputDown;
    public bool jumpInput;
    public bool dashInputDown;
    public bool attackInputDown;
    public bool defendInput;

    #endregion
    public void GetInputs(InputsHandler handler)
    {
        hInput = handler.GetAxisInputs(horizontalAxis);
        vInput = handler.GetAxisInputs(verticalAxis);
        SetAnimatorMoveSpeed();

        if (HoldToWalk)
            walkSpeedInput = handler.GetKey(speedChangeKey);
        else if (handler.GetKeyDown(speedChangeKey))
            walkSpeedInput = !walkSpeedInput;

        jumpInput = handler.GetKey(jumpKey);
        if (handler.GetKeyUp(jumpKey))
            willBufferJump = false;
        defendInput = handler.GetKey(defendKey);

        dashInputDown = handler.GetKeyDown(dashKey);
        interactionInputDown = handler.GetKeyDown(interactionKey);
        attackInputDown = handler.GetKeyDown(attackKey);

    }

    #region animator move input update
    public void SetAnimatorMoveSpeed()
    {
        Animator.SetFloat("HInput", Mathf.Abs(hInput * currentMoveSpeed));
        Animator.SetFloat("YInput", Mathf.Abs(vInput * currentMoveSpeed));
    }

    #endregion

    #endregion

    #region Checks 
    //checks
    #region Ground check
    [Header("Ground check")]
    public float LastTimeGrounded;
    public float LastGrounded;
    public bool isGrounded;
    public void Checks()
    {
        GroundCheck();
        HeadCheck();
        WallCheck();
    }

    public void GroundCheck()
    {
        //send 2 raycast at the limits of the player's feet to check if the player is grounded
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(Width / 2, 0, 0), Vector2.down, Height / 2 + extraGroundCheckDistance, whatIsGround);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(Width / 2, 0, 0), Vector2.down, Height / 2 + extraGroundCheckDistance, whatIsGround);
        if (hitLeft.collider != null || hitRight.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    void OnDrawGizmos()
    {
        Vector3 rightRayOrigin = transform.position + new Vector3(Width / 2, 0, 0);
        Vector3 leftRayOrigin = transform.position - new Vector3(Width / 2, 0, 0);

        float rayLength = Height / 2 + extraGroundCheckDistance;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(rightRayOrigin, rightRayOrigin + Vector3.down * rayLength);
        Gizmos.DrawLine(leftRayOrigin, leftRayOrigin + Vector3.down * rayLength);
    }

    #endregion

    #region Head Check

    public void HeadCheck()
    {
        //send 2 raycast at the limits of the player's head to check if the players has hit a ceiling
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(Width / 2, 0, 0), Vector2.up, Height / 2 + extraHeadCheckDistance, whatIsGround);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(Width / 2, 0, 0), Vector2.up, Height / 2 + extraHeadCheckDistance, whatIsGround);
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
    public bool isHuggingWall = false;
    public float LastTimeWalled;
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
        return Physics2D.Raycast(transform.position + new Vector3(0, Height / 2, 0), new Vector2(transform.localScale.x, 0f), Width / 2 + extraGroundCheckDistance, whatIsWall);
    }
    public bool WallDetectionMiddle()
    {
        return Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0f), Width / 2 + extraGroundCheckDistance, whatIsWall);
    }
    public bool WallDetectionLower()
    {
        return Physics2D.Raycast(transform.position - new Vector3(0, Height / 2, 0), new Vector2(transform.localScale.x, 0f), Width / 2 + extraGroundCheckDistance, whatIsWall);
    }
    #endregion

    #endregion

    #region Move
    [Header("Move")]
    public float currentMoveSpeed;
    public float currentMaxMoveSpeed;
    public float currentAcceleration;
    public float currentDeceleration;

    #endregion
    #region 

    #endregion

    #region Fall
    [Header("Fall")]
    public float fasterFallMultiplier;
    public float maxFallSpeed;

    #endregion

    #region Jump
    [Header("Jump")]
    public float jumpPressTime;
    public float jumpTimeCounter;
    public float jumpForce;
    public float maxJumpTime;
    public Vector2 jumpDirection;


    #region Variable Jump
    [Header("Variable Jump")]


    #endregion


    #region Buffer Jump
    [Header("Buffer Jump")]
    public bool willBufferJump;
    public float jumpBufferTime = 0.1f;

    #endregion

    #region Cyote Jump
    [Header("Cyote Jump")]
    public bool canCyoteJump;
    public float cyoteTime;

    #endregion
    #region wall jump
    [Header("Wall Jump")]
    public float wallJumpPressTime;
    public float wallJumpDuration;
    public Vector2 wallJumpDirection;

    #endregion

    #endregion

    #region
    [Header("Wall Slide")]
    public float wallSlidingSpeed;
    #endregion

    #region Dash

    [Header("Dash")]
    public float dashForce;
    public float dashTime;
    public float dashCd;

    public float lastDashFinishTime;
    public bool dashReset;
    public float dashDirection;
    public float lastDash;
    public bool dashCounter;

    public bool canDashCheck()
    {
        //if already dashed give time to re-dash
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
    #endregion
}


using UnityEngine;

public class PlayerInputs : MonoBehaviour
{

    [Header("Components")]
    public Rigidbody2D playerRb;
    public CapsuleCollider2D capsuleCollider;
    public Animator playerAnimator;
    public float playerHeight;
    public float playerWidth;
    public void Awake()
    {
        //get scripts
        //jumpScript = GetComponent<JumpScript>();
        //wallSlideScript = GetComponent<WallSliding>();
        //get components

        //get size values
    }
    public void GetComponents()
    {
        playerRb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<Animator>();
        playerWidth = capsuleCollider.size.x * transform.localScale.x;
        playerHeight = capsuleCollider.size.y * transform.localScale.y;
    }

    public void Update()
    {
        GroundCheck();
        HeadCheck();
        GetHInputs();
        GetVInputs();
        GetJumpInput();

    }


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
    }
    #endregion

    #region General Checks
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

            //cyote time
            //jumpScript.canJump = true;
            //jumpScript.LastGrounded = Time.time;
        }
        else
        {
            isGrounded = false;
        }
        //playerAnimator.SetBool("isGrounded", isGrounded);
    }


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

    #region WallDetection
    [Header("Wall Check")]

    [Header("Ledge Detection")]
    public LayerMask whatIsWall;
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


    public float LastWalled;
    bool isHuggingWall = false;

    public void WallCheck()
    {
        isHuggingWall = WallDetectionUpper() || WallDetectionMiddle() || WallDetectionLower();//wallSlideScript.isWallSliding
        if (isHuggingWall)
        {
            //canJump = true;
            LastWalled = Time.time;
        }
    }
    #endregion

    #region 
    #endregion
}

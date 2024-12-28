using UnityEngine;

public class Inputs : MonoBehaviour
{
    [Header("Refrences")]
    public JumpScript jumpScript;
    public WallSliding wallSlideScript;

    [Header("Components")]
    public Rigidbody2D playerRb;
    public CapsuleCollider2D capsuleCollider;
    public Animator playerAnimator;
    public float playerHeight;
    public float playerWidth;

    [Header("Inputs")]
    public float horizontalInput;
    public float verticalInput;
    public bool jumpInput;
    public bool jumpInputDown;
    public bool jumpInputUp;
    public bool dashInput;
    public bool AttackInput;

    private void Awake()
    {
        //get scripts
        jumpScript = GetComponent<JumpScript>();
        wallSlideScript = GetComponent<WallSliding>();
        //get components
        playerRb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<Animator>();
        //get values
        playerWidth = capsuleCollider.size.x;
        playerHeight = capsuleCollider.size.y;
    }


    // remove this update
    private void Update()
    {
        HeadCheck();
        //set animator Y velocity
        playerAnimator.SetFloat("Yvelocity", playerRb.velocity.y);
        if (!wallSlideScript.isLedgeBumping)
        {
            GroundCheck();
        }

        GetHInputs();
        GetVInputs();
        GetJumpInput();
        GetDashInput();
        GetAttackInput();
    }
    #region movement

    public void GetHInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        playerAnimator.SetFloat("Hvelocity", Mathf.Abs(horizontalInput));
    }

    public void GetVInputs()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    public KeyCode jumpKey;
    public void GetJumpInput()
    {
        jumpInput = Input.GetKey(jumpKey);
        jumpInputDown = Input.GetKeyDown(jumpKey);
        jumpInputUp = Input.GetKeyUp(jumpKey);
    }
    #endregion

    #region Actions
    public KeyCode dashKey;
    public void GetDashInput()
    {
        dashInput = Input.GetKeyDown(dashKey);
    }

    public KeyCode attackKey;
    public void GetAttackInput()
    {
        AttackInput = Input.GetKeyDown(attackKey);
    }
    public void GetInterractionInput()
    {

    }
    #endregion

    #region General Checks
    [Header("Checks")]
    public LayerMask whatIsGround;
    public bool isGrounded;
    public float extraGroundCheckDistance;
    public void GroundCheck()
    {
        //send 2 raycast at the limits of the player's feet to check if the player is grounded
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(playerWidth / 2, 0, 0), Vector2.down, playerHeight / 2 + extraGroundCheckDistance, whatIsGround);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(playerWidth / 2, 0, 0), Vector2.down, playerHeight / 2 + extraGroundCheckDistance, whatIsGround);
        if (hitLeft.collider != null || hitRight.collider != null)
        {
            isGrounded = true;
            //jump reset
            //jumpScript.jumpReset = true;
            //cyote time
            jumpScript.canJump = true;
            jumpScript.LastGrounded = Time.time;
        }
        else
        {
            isGrounded = false;
        }
        playerAnimator.SetBool("isGrounded", isGrounded);
    }

    //head bump
    public void HeadCheck()
    {
        //send 2 raycast at the limits of the player's head to check if the players has hit a ceiling
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(playerWidth / 2, 0, 0), Vector2.up, playerHeight / 2 + extraGroundCheckDistance, whatIsGround);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(playerWidth / 2, 0, 0), Vector2.up, playerHeight / 2 + extraGroundCheckDistance, whatIsGround);
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


}

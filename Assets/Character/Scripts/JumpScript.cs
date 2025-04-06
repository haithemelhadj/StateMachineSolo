using UnityEngine;

public class JumpScript : MonoBehaviour
{
    [Header("Refrences")]
    public Inputs inputsScript;
    public WallSliding wallSlideScript;
    public Actions ActionsScript;
    public Movement movementscript;

    private void Awake()
    {
        //get scripts
        inputsScript = GetComponent<Inputs>();
        ActionsScript = GetComponent<Actions>();
        wallSlideScript = GetComponent<WallSliding>();
        movementscript = GetComponent<Movement>();
    }

    private void Update()
    {
        if (ActionsScript.isDashing) return; //if dashing stop movement
        WallCheck();
        VariableJump();
        CyoteTime();
        JumpBuffer();
        JumpInput();
        FallControll();
    }



     #region  Jump 
    [Header("Jump")]
    public float jumpForce;
    //public bool jumpConditions;
    public bool jumpInputConfirmed;
    public bool jumpReset;
    [Header("wall Jump")]
    public float wallJumpDuration;
    public float wallJumpPressTime;
    public bool isWallJumping;
    public Vector2 jumpDirection;
    public Vector2 wallJumpDirection;
    #region Variable Jump
    [Header("Variable Jump")]
    public float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;
    public void VariableJump()
    {
        if (inputsScript.jumpInputUp)
        {
            isJumping = false;
            //set jumping animation
            inputsScript.playerAnimator.SetBool("isJumping", isJumping);
            isWallJumping = false;
        }
    }
    #endregion

    #region Jump Buffer
    //-----------Jump Buffer
    [Header("Jump Buffer")]
    public float jumpPressTime;
    public float jumpBufferTime;
    public bool willJump;
    public void JumpBuffer()
    {
        if (Time.time - jumpPressTime > jumpBufferTime)
        {
            willJump = false;
        }

    }
    #endregion

    #region Cyote Time
    //-----------cyote time
    [Header("Cyote Time")]
    public float LastGrounded;
    public float cyoteTime;
    public bool canJump;
    public void CyoteTime()
    {
        //if (!canJump) return;
        if (Time.time - LastGrounded > cyoteTime || Time.time - LastWalled > cyoteTime)
        {
            canJump = false;
        }
    }
    #endregion

    public void JumpInput()
    {        
        // get jump input and set values 
        if (inputsScript.jumpInputDown)
        {
            jumpPressTime = Time.time;
            willJump = true;
            //put jump direction based on player state
            if (isHuggingWall)
            {
                wallJumpPressTime = Time.time;
                isWallJumping = true;
            }
        }
        // jump with get key down and up and resets jump press when player is grounded
        if (willJump && jumpReset && (inputsScript.isGrounded || canJump || isHuggingWall))
        {
            jumpInputConfirmed = true;
            jumpReset = false;
        }
        // get jump key up to not jump
        if (inputsScript.jumpInputUp)
        {
            jumpInputConfirmed = false;
            willJump = false;
            jumpReset = true;
        }

        //if player is pressing jump 
        if (jumpInputConfirmed)
        {
            //check if the player can jump
            if (inputsScript.isGrounded || canJump || isHuggingWall)
            {
                jumpTimeCounter = jumpTime;  
                isJumping = true;
                //set jumping animation
                inputsScript.playerAnimator.SetBool("isJumping", isJumping);
            }

            if (isJumping)
            {
                //check if walljumping duration is not over and set jump direction
                if (isWallJumping || (Time.time - wallJumpPressTime < wallJumpDuration && isHuggingWall))
                {
                    isWallJumping = Time.time - wallJumpPressTime < wallJumpDuration;
                    jumpDirection = new Vector2(-transform.localScale.x * wallJumpDirection.x, wallJumpDirection.y);
                }
                else
                {
                    isWallJumping = false;
                    jumpDirection = new Vector2(inputsScript.playerRb.velocity.x, jumpForce);
                }
                //check if jump duration is not over
                if (jumpTimeCounter > 0)
                {
                    Jumping(jumpDirection);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else //else stop jumping
                {
                    isWallJumping = false;
                    isJumping = false;
                    jumpInputConfirmed = false;
                    //set jumping animation
                    inputsScript.playerAnimator.SetBool("isJumping", isJumping);
                }
            }
        }
    }

    public void Jumping(Vector2 JumpDirection)
    {
        inputsScript.playerRb.velocity = JumpDirection;
        canJump = false;
    }

    #endregion



    #region WallCheck
    public float LastWalled;
    bool isHuggingWall = false;

    public void WallCheck()
    {
        isHuggingWall = wallSlideScript.WallDetectionUpper() || wallSlideScript.WallDetectionMiddle() || wallSlideScript.WallDetectionLower();//wallSlideScript.isWallSliding
        if (isHuggingWall)
        {
            canJump = true;
            LastWalled = Time.time;
        }
    }
    #endregion

    #region Fall Controll
    [Header("Fall Controll")]
    public float maxFallSpeed;
    public float fallMultiplier;
    public void FallControll()
    {
        //make fall speed faster
        if (inputsScript.playerRb.velocity.y < 0 && inputsScript.playerRb.velocity.y > -maxFallSpeed)
        {
            if (fallMultiplier == 0f) fallMultiplier = 1f;
            inputsScript.playerRb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        //limit fall speed 
        if (inputsScript.playerRb.velocity.y < -maxFallSpeed)
        {
            inputsScript.playerRb.velocity = new Vector2(inputsScript.playerRb.velocity.x, -maxFallSpeed);
        }
    }
    #endregion
}

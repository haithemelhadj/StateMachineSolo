using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public CapsuleCollider2D capsuleCollider;


    #region animation vars


    #endregion

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerWidth = capsuleCollider.size.x;
        playerHeight = capsuleCollider.size.y;
    }

    private void Update()
    {
        GroundCheck();


        MovementInput();
        if (isdashing) return;
        DashInput();
        VariableJump();
        JumpInput();
        CyoteTime();
        JumpBuffer();


    }

    private void FixedUpdate()
    {
        if (isdashing) return;
        Movement();
        HSpeedLimit();
        Jump();
        FastDrop();
        FallSpeedLimit();
    }


    #region Horizontal Movement
    [Header("Horizontal Movement")]
    public float speed;
    //public float acceleration;
    public float deceleration;
    public float horizontalInput;
    public float VerticalinputDir;

    public void MovementInput()
    {
        //get player inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //VerticalinputDir = Input.GetAxis("Vertical");
    }

    public void Movement()
    {
        //move player
        if (horizontalInput != 0f)
            playerRb.velocity += new Vector2(horizontalInput * speed, playerRb.velocity.y) * Time.deltaTime;
        else //slow player to stop
            playerRb.velocity = Vector3.Lerp(playerRb.velocity, new Vector3(0, playerRb.velocity.y, 0), deceleration * Time.deltaTime);

        //flip character and keep it that way when no inputs
        if (horizontalInput > 0 && !isFacingRight)
            Flip();
        if (horizontalInput < 0 && isFacingRight)
            Flip();


    }

    public bool isFacingRight;
    public void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight;

    }

    public float maxHSpeed;
    private void HSpeedLimit()
    {
        if (horizontalInput != 0)
        {
            playerRb.velocity = new Vector2(maxHSpeed * Mathf.Sign(horizontalInput), playerRb.velocity.y);
        }
    }



    #endregion

    #region Vertical Movement
    #region Jump
    [Header("Vertical Movement")]
    //---------Jump Input
    public float jumpForce;


    public void JumpInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            willJump = true; //set jump press to true
            pressTime = Time.time; //set the time of the jump press
            if (isGrounded)
            {
                jumpTimeCounter = jumpTime;
                isJumping = true;
                Jump();
            }
            if (isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    Jump();
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
        }
    }


    [Header("Variable Jump")]
    public float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;
    public void VariableJump()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    //-------Jump
    public void Jump()
    {
        //if jump conditions are met jump
        if (willJump && isGrounded || canJump)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            canJump = false;
        }
    }

    //--------jump buffer
    [Header("Jump Buffer")]
    public float jumpBufferTime;
    public float pressTime;
    public bool willJump;
    public void JumpBuffer()
    {
        if (Time.time - pressTime > jumpBufferTime)
        {
            willJump = false; //if the time between the jump press and the current time is greater than the buffer time, set jump press to false
        }
    }

    //-----------cyote time
    [Header("Cyote Time")]
    public float LastGrounded;
    public float cyoteTime;
    public bool canJump;
    public void CyoteTime()
    {
        if (Time.time - LastGrounded < cyoteTime)
        {
            canJump = false;
        }
    }




    #endregion
    [Header("Fall Controll")]
    public float maxFallSpeed;
    public void FallSpeedLimit()
    {
        if (playerRb.velocity.y < -maxFallSpeed)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -maxFallSpeed);
        }
    }

    public void FastDrop()
    {
        if (playerRb.velocity.y < 2f)
        {
            playerRb.velocity += Vector2.up * Physics2D.gravity.y * 5 * Time.deltaTime;
        }
    }

    #endregion


    #region General Checks
    [Header("Checks")]
    public LayerMask whatIsGround;
    public bool isGrounded;
    public float playerHeight;
    public float playerWidth;
    public float extraGroundCheckDistance;
    public void GroundCheck()
    {
        //send 2 raycast at the limits of the player's feet to check if the player is grounded
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(playerWidth / 2, 0, 0), Vector2.down, playerHeight / 2 + extraGroundCheckDistance, whatIsGround);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(playerWidth / 2, 0, 0), Vector2.down, playerHeight / 2 + extraGroundCheckDistance, whatIsGround);
        if (hitLeft.collider != null || hitRight.collider != null)
        {
            //Debug.Log(playerRb.velocity.y);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            //cyote time
            LastGrounded = Time.time;
            canJump = true;
        }
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

    #region Dash


    [Header("Dash")]
    public float dashForce;
    public float dashTime;
    public bool canDash;
    public bool isdashing;

    public float drag;
    public void DashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        if (isGrounded)
        {
            canDash = true;
        }
    }

    public IEnumerator Dash()
    {
        //set vars
        canDash = false;
        isdashing = true;
        //save gravity
        float originalGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0f;
        //null velocity
        playerRb.velocity = Vector2.zero;
        //set air friction 
        playerRb.drag = drag;
        //dash
        //playerRb.AddForce(new Vector2(-transform.localScale.x * dashForce, 0), ForceMode2D.Impulse);    
        playerRb.velocity = new Vector2(-transform.localScale.x * dashForce, 0f);
        yield return new WaitForSeconds(dashTime);
        //rest everything
        playerRb.drag = 0f;
        playerRb.gravityScale = originalGravity;
        isdashing = false;
        yield return new WaitForSeconds(dashTime);

    }


    //add CD to dash

    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(playerWidth / 2, 0, 0), transform.position + new Vector3(playerWidth / 2, 0, 0) + Vector3.down * (playerHeight / 2 + extraGroundCheckDistance));
        Gizmos.DrawLine(transform.position - new Vector3(playerWidth / 2, 0, 0), transform.position - new Vector3(playerWidth / 2, 0, 0) + Vector3.down * (playerHeight / 2 + extraGroundCheckDistance));
    }


    /*
     bounces a lot when in tight space and jumps
     head bump push to the side
     dash
     */
}

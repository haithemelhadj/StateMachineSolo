using System.Collections;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public Inputs inputsScript;
    public JumpScript jumpScript;
    public WallSliding wallSlideScript;

    private void Awake()
    {
        inputsScript = GetComponent<Inputs>();
        jumpScript = GetComponent<JumpScript>();
        wallSlideScript = GetComponent<WallSliding>();
    }

    private void Update()
    {
        DashInput();
        AttackInput();
    }

    #region Dash


    [Header("Dash")]
    public float dashForce;
    public float dashTime;
    public bool canDash;
    public bool isDashing;
    public float dashDirection;

    //public float drag;
    public void DashInput()
    {
        if (inputsScript.dashInput && canDash)
        {
            StartCoroutine(Dash());
        }
        //stop dasing when hitting a wall ( when enabled the player cannot dash from wall)
        //if (isDashing && wallSlideScript.isWallSliding)
        //{
        //    StopCoroutine(Dash());
        //    isDashing = false;
        //    inputsScript.playerAnimator.SetBool("Dashing", isDashing);
        //}
        if (!isDashing && (inputsScript.isGrounded || wallSlideScript.isWallSliding))
        {
            canDash = true;
        }
    }

    public IEnumerator Dash()
    {
        //set vars
        canDash = false;
        isDashing = true;
        inputsScript.playerAnimator.SetBool("Dashing", isDashing);
        //save gravity
        float originalGravity = inputsScript.playerRb.gravityScale;
        inputsScript.playerRb.gravityScale = 0f;
        inputsScript.playerRb.constraints.Equals(RigidbodyConstraints2D.FreezePositionY);
        //set air friction 
        //float originalDrag = inputsScript.playerRb.drag;
        //inputsScript.playerRb.drag = drag;
        //stop jumping
        jumpScript.isJumping = false;
        //set jumping animation to stop
        inputsScript.playerAnimator.SetBool("isJumping", jumpScript.isJumping);
        //null velocity
        inputsScript.playerRb.velocity = Vector2.zero;
        //set dash direction if is wall sliding
        if (wallSlideScript.isWallSliding)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        //dash 
        inputsScript.playerRb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * dashForce, 0f);
        yield return new WaitForSeconds(dashTime);
        //reset everything
        inputsScript.playerRb.constraints.Equals(RigidbodyConstraints2D.None);
        inputsScript.playerRb.constraints.Equals(RigidbodyConstraints2D.FreezePosition);
        inputsScript.playerRb.drag = 0f;
        inputsScript.playerRb.gravityScale = originalGravity;
        //inputsScript.playerRb.drag = originalDrag;
        isDashing = false;
        inputsScript.playerAnimator.SetBool("Dashing", isDashing);
        yield return new WaitForSeconds(dashTime);

    }
    #endregion

    #region Attack
    [Header("Attack")]
    //attack refrences
    public GameObject atkObj;
    public Animator atkAnimator;
    //attack variables
    public float atkRange;
    public float atkTime;
    [HideInInspector] public float atkDistance;
    [HideInInspector] public Vector2 atkPosition;
    [HideInInspector] public float atkRotation;

    public void AttackInput()
    {
        if (inputsScript.AttackInput)
        {
            if (inputsScript.verticalInput != 0)
            {
                //attack vertically
                atkDistance = Mathf.Sign(inputsScript.verticalInput) * (inputsScript.playerHeight / 2 + atkRange);
                atkPosition = new Vector2(transform.position.x, transform.position.y + atkDistance);
                atkRotation = 90f * Mathf.Sign(inputsScript.verticalInput) * Mathf.Sign(transform.localScale.x);//rotation is based on localScale.x
            }
            else
            {
                //attack horizontally
                atkDistance = Mathf.Sign(transform.localScale.x) * (inputsScript.playerWidth / 2 + atkRange);
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

    #endregion

    //make wall jump on the same wall can go higher like HK = make player can move during jump and can jump heigher 
    //add new mechanic to wall slide is that the player can run to the wall and gain extra hight and jump distance 
    //wall jump doesnt continue jumping like a normal jump = wall jumping only adds X force and player jumps normally
}

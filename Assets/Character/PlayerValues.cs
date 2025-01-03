using UnityEngine;
[CreateAssetMenu]
public class PlayerValues : ScriptableObject
{




    /*
    //Awake
    #region Get Components
    [Header("Components")]
    public Rigidbody2D playerRb;
    public CapsuleCollider2D capsuleCollider;
    //public Animator playerAnimator;
    public float playerHeight;
    public float playerWidth;
    #endregion
    /**/

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

    #endregion

    #region Head Check
    [Header("Head Check")]
    //public LayerMask whatIsOverHead;
    public bool isHeadBumped;
    public float extraHeadCheckDistance = 0.01f;

    #endregion

    #region Wall Detection
    [Header("Wall Check")]
    public LayerMask whatIsWall;
    public bool isHuggingWall = false;
    public float LastTimeWalled;


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
    #endregion

    #region Cyote Time
    [Header("Cyote Time")]
    public float LastGrounded;
    public float cyoteTime;
    public bool canJump;

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
    #region Dash
    [Header("Dash")]
    public float dashForce;
    public float dashTime;
    public float dashDirection;
    public bool canDash;
    public bool isDashing;
    public bool dashInput;
    public KeyCode dashKey;
    
    #endregion

    #region Attack
    public KeyCode attackKey;
    public bool AttackInput;
    
    #endregion

    #region Interract
    public KeyCode InterractionKey;

    #endregion

    /**/

}
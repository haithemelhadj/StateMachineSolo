using UnityEngine;

public class AiStateMachine : MonoBehaviour
{
    [Header("Visualizing")]
    [SerializeField] public string currentActiveState;
    [SerializeField] public bool isGrounded;
    [Header("State Management")]
    public AiBaseState _currentState;
    AiStateFactory _states;

    //[SerializeField] protected string currentSuperState;
    //[SerializeField] protected string currentSubState;

    [Header("Refrences")]
    [SerializeField] public Transform targetPlayer;

    [Header("Components")]
    [SerializeField] public Rigidbody2D selfRb;
    [SerializeField] public CapsuleCollider2D mobCollider;
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public Animator animator;

    [Header("Params")]
    [SerializeField] public float mobHeight;
    [SerializeField] public float mobWidth;


    private void Awake()
    {
        GetComponents();
        InitializeState();

    }

    private void Update()
    {

        //logic
        _currentState.UpdateStates();
    }
    private void FixedUpdate()
    {
        _currentState.FixedUpdateState();
    }

    private void InitializeState()
    {
        _states = new AiStateFactory(this);
        _currentState = _states.Patrol();
        _currentState.EnterState();
    }
    private void GetComponents()
    {
        selfRb = GetComponent<Rigidbody2D>();
        mobCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        mobWidth = mobCollider.size.x;
        mobHeight = mobCollider.size.y;
        //targetPlayer = GameObject.FindGameObjectWithTag("player").transform;
        targetPlayer = GameObject.Find("SF Player").transform;
    }

    public void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x = -transform.localScale.x;
        transform.localScale = currentScale;
    }

    #region Checks

    [Header("checks")]
    [SerializeField] public LayerMask whatIsGround;
    public float extraCheckDistance = 0.01f;

    public bool GroundCheck()
    {
        //send 2 raycast at the limits of the player's feet to check if the player is grounded
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(mobWidth / 2, 0, 0), Vector2.down, mobHeight / 2 + extraCheckDistance, whatIsGround);
        Debug.DrawRay(transform.position + new Vector3(mobWidth / 2, 0, 0), Vector2.down * (mobHeight / 2 + extraCheckDistance), color: Color.blue);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(mobWidth / 2, 0, 0), Vector2.down, mobHeight / 2 + extraCheckDistance, whatIsGround);
        Debug.DrawRay(transform.position - new Vector3(mobWidth / 2, 0, 0), Vector2.down * (mobHeight / 2 + extraCheckDistance), color: Color.blue);
        isGrounded = (hitLeft.collider != null || hitRight.collider != null);
        return isGrounded;
    }
    
    public bool WallCheck()
    {
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position + (Vector3)mobCollider.offset + new Vector3((mobWidth / 2 + detectionRange) * Mathf.Sign(transform.localScale.x), 0, 0), Vector2.right * Mathf.Sign(transform.localScale.x), extraCheckDistance, whatIsGround);
        Debug.DrawRay(transform.position + (Vector3)mobCollider.offset + new Vector3((mobWidth / 2 + detectionRange) * Mathf.Sign(transform.localScale.x), 0, 0), Vector2.right * Mathf.Sign(transform.localScale.x) * extraCheckDistance, color: Color.green);
        return wallCheck;
    }

    public bool LedgeCheck()
    {
        RaycastHit2D ldgeCheck = Physics2D.Raycast(transform.position + (Vector3)mobCollider.offset + new Vector3((mobWidth / 2 + detectionRange) * Mathf.Sign(transform.localScale.x), 0, 0), Vector2.down, mobHeight / 2 + extraCheckDistance, whatIsGround);
        Debug.DrawRay(transform.position + (Vector3)mobCollider.offset + new Vector3((mobWidth / 2 + detectionRange) * Mathf.Sign(transform.localScale.x), 0, 0), Vector2.down * (mobHeight / 2 + extraCheckDistance), color: Color.red);
        return ldgeCheck;
    }

    #endregion

    #region Dectection

    [Header("FOV & Detection")]
    public float detectionRange = 1f;
    public float fovAngle = 60f;
    public float reactionTime = 0.2f;
    #endregion


    #region Idle

    [Header("Idle")]
    public float maxIdleTime = 5f;
    [SerializeField] public float randIdleWaitTime;
    [SerializeField] public float idleEnterTime;
    #endregion

    #region patrol

    [Header("Patrol")]
    public float patrolSpeed;

    public float maxPatrolTime = 5f;
    [SerializeField] public float randPatrolTime;
    [SerializeField] public float patrolEnterTime;
    #endregion


    #region Chase
    [Header("Chase")]
    public float chaseSpeed;
    public float catchDistance = 1;
    [SerializeField] public Vector2 playerlastSeenPos;
    #endregion

    #region Search
    [Header("Search")]
    public float searchSpeed;
    public float maxSearchTime = 5f;
    [SerializeField] public float randSearchTime;
    [SerializeField] public float searchEnterTime;
    #endregion

    #region Attack
    [Header("Attack")]
    public float attackDistance;
    #endregion

    #region Death
    //[Header("Death")]
    #endregion

    #region
    #endregion

    #region
    #endregion

    #region
    #endregion

}

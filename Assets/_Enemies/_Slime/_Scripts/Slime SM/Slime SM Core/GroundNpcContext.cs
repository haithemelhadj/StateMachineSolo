using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ground Npc Context", menuName = "States List/Ground Npc /Context")]

public class GroundNpcContext : MonoBehaviour
{
    public float tickRate = 0.2f;
    public void ContextStart()
    {
        //Get Components
        GetComponents();
        StartCoroutine(ChecksCoortine(tickRate));

    }
    public void ContextUpdate()
    {
        CheckOnUpdate();
        SetAnimatorMoveVelocitySpeed();
    }


    #region Rootine Updates
    public IEnumerator ChecksCoortine(float tick)
    {
        while (true)
        {
            //ChecksByTicks();
            yield return new WaitForSeconds(tick);
        }
    }
    public void CheckOnUpdate()
    {
        ChecksByTicks();
    }

    private void ChecksByTicks()
    {
        GroundCheck();
        HeadCheck();
        WallCheck();
        LedgeCheck();
        currentTarget = Detected.Count > 0 ? Detected[0] : null;
    }
    #endregion



    #region Methods

    #region Get Components
    public void GetComponents()
    {
        Rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Width = capsuleCollider.size.x * transform.localScale.x;
        Height = capsuleCollider.size.y * transform.localScale.y;
        localScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animatorController = GetComponent<AnimatorController>();
    }
    #endregion


    #region Chat Ai logic 

    [Header("----Detection Settings----")]
    public Transform eyePosition; // assign AI’s eye (empty GameObject) in inspector
    [SerializeField] private LayerMask visionMask;

    // The outputs you asked for
    public Transform targetInSight;
    public bool hasTarget;

    private void Update()
    {
        targetInSight = IsAnObjectInSight();
        hasTarget = targetInSight != null;
        if (hasTarget)
        {
            Debug.Log("Target in sight: " + targetInSight.name);

        }
    }

    public Transform IsAnObjectInSight()
    {
        foreach (Transform target in new List<Transform>(Detected))
        {
            if (target == null) continue;

            Debug.Log("los: " + HasLineOfSight(target));
            if (IsInFOV(target) && HasLineOfSight(target))
            {
                return target;
            }
        }
        return null;
        ////alt
        //for (int i = 0; i < Detected.Count; i++)
        //{
        //    Transform target = Detected[i];
        //    if (target == null) continue;

        //    if (IsInFOV(target) && HasLineOfSight(target))
        //    {
        //        return target;
        //    }
        //}
        //return null;
    }

    public bool IsInFOV(Transform target)
    {
        SortTransformsByDistance();

        Vector2 directionToTarget = target.position - transform.position;
        float distance = directionToTarget.magnitude;

        if (distance > detectionRange) // check target is in distance
            return false;

        // Convert lookDirection (degrees) into a vector
        float radians = lookDirection * Mathf.Deg2Rad;
        Vector2 forward = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        // Calculate angle between forward and direction to target
        float angle = Vector2.Angle(forward, directionToTarget);

        return angle < (fovAngle / 2f);
    }

    private bool HasLineOfSight(Transform target)
    {
        if (eyePosition == null)
            eyePosition = this.transform; // default to AI's position if no eye assigned

        Vector2 eye = eyePosition.position;
        Collider2D targetCollider = target.GetComponent<Collider2D>();
        if (targetCollider == null) return false;
        //{
        //    Debug.LogWarning("Target has no Collider2D component.");
        //    return false;

        //}
        Bounds bounds = targetCollider.bounds;

        Vector2[] targetPoints = new Vector2[3];
        targetPoints[0] = new Vector2(bounds.center.x, bounds.max.y); // top
        targetPoints[1] = bounds.center;                             // middle
        targetPoints[2] = new Vector2(bounds.center.x, bounds.min.y); // bottom

        foreach (Vector2 point in targetPoints)
        {
            RaycastHit2D hit = Physics2D.Raycast(eye, point - eye, detectionRange, visionMask);
            
            if (hit.collider != null && hit.collider.transform == target) // this if always returns false
            {
                return true; // at least one ray has clear vision
            }
            else
            {
                Debug.DrawLine(eye, point, Color.red); // visualize blocked ray
            }
        }
        return false;
    }

    #endregion

    #region Detected Objects List
    [Header("Detected Objects")]
    public List<Transform> Detected;
    public string[] detectableTags;
    public void SortTransformsByDistance()
    {
        if (Detected == null || Detected.Count == 0)
            return;

        Vector3 myPos = transform.position;

        Detected.Sort((a, b) =>
            Vector3.Distance(myPos, a.position).CompareTo
            (Vector3.Distance(myPos, b.position)));
    }
    #endregion


    public void SetAnimatorMoveVelocitySpeed()
    {
        animatorController.UpdateAnimatorFloat("Hvelocity", Mathf.Abs(Rb.velocity.x));
        animatorController.UpdateAnimatorFloat("Yvelocity", Rb.velocity.y);
    }

    public void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x = -transform.localScale.x;
        transform.localScale = currentScale;
    }

    #endregion



    [Header("Visualizing")]
    public string currentActiveState;
    [Header("State Management")]
    public AiBaseState _currentState;
    AiStateFactory _states;


    [Header("Refrences")]
    public Transform currentTarget;

    [Header("Components")]
    public CapsuleCollider2D capsuleCollider;
    public AnimatorController animatorController;
    public Rigidbody2D Rb;
    public SpriteRenderer spriteRenderer;



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



    #region Checks 
    [Header("-------CHECKS------")]
    public float extraCheckDistance = 0.01f;
    [Header("Flags")]
    public bool isHeadBumping;
    public bool isGrounded;
    public bool isHuggingWall;
    public bool isNearEdge;

    public void LedgeCheck()
    {
        RaycastHit2D ldgeCheck = Physics2D.Raycast(
            transform.position + (Vector3)capsuleCollider.offset + new Vector3((Width / 2 + ledgeDtectionRange) * Mathf.Sign(transform.localScale.x), 0, 0)
            , Vector2.down
            , Height / 2 + extraCheckDistance
            , whatIsGround);
        //Debug.DrawRay(transform.position + (Vector3)capsuleCollider.offset + new Vector3((Width / 2 + ledgeDtectionRange) * Mathf.Sign(transform.localScale.x), 0, 0), Vector2.down * (Height / 2 + extraCheckDistance), color: Color.red);
        isNearEdge = !ldgeCheck;
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
        animatorController.UpdateAnimatorBool("isGrounded", isGrounded);
    }

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
    public float LastTimeWalled;
    public void WallCheck()
    {
        isHuggingWall = WallDetectionMiddle() && (WallDetectionUpper() || (WallDetectionLower()));
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


    private void OnDrawGizmos()

    {
        // draw ground check ray cast
        #region ground check 
        Vector3 rightRayOrigin = transform.position + new Vector3(Width / 2, 0, 0);
        Vector3 leftRayOrigin = transform.position - new Vector3(Width / 2, 0, 0);

        float rayLength = Height / 2 + extraGroundCheckDistance;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(rightRayOrigin, rightRayOrigin + Vector3.down * rayLength);
        Gizmos.DrawLine(leftRayOrigin, leftRayOrigin + Vector3.down * rayLength);
        #endregion
        //draw wall detection ray cast
        #region wall detection 
        Vector3 wallRayUpOrigin = transform.position + new Vector3(0, Height / 2, 0);
        Vector3 wallRayOrigin = transform.position;// + new Vector3(0, Height / 2, 0);
        Vector3 wallRayBotOrigin = transform.position - new Vector3(0, Height / 2, 0);
        Vector3 wallRayDirection = new Vector3(transform.localScale.x, 0f, 0f).normalized;

        float wallRayLength = Width / 2 + extraGroundCheckDistance;

        Gizmos.color = Color.blue; // Different color for clarity
        Gizmos.DrawLine(wallRayUpOrigin, wallRayUpOrigin + wallRayDirection * wallRayLength);
        Gizmos.DrawLine(wallRayOrigin, wallRayOrigin + wallRayDirection * wallRayLength);
        Gizmos.DrawLine(wallRayBotOrigin, wallRayBotOrigin + wallRayDirection * wallRayLength);
        #endregion
        // Draw Ledge check ray cast
        #region Ledge check 

        Vector3 ledgeOrigin = transform.position + (Vector3)capsuleCollider.offset +
                         new Vector3((Width / 2 + ledgeDtectionRange) * Mathf.Sign(transform.localScale.x), 0, 0);

        Vector3 direction = Vector2.down * (Height / 2 + extraCheckDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(ledgeOrigin, ledgeOrigin + direction);

        #endregion

        //draw FOV detection range
        #region FOV 
        Transform origin = transform;

        // Draw detection range circle

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin.position, detectionRange);


        // Convert lookDirection (degrees) into a vector
        float radians = lookDirection * Mathf.Deg2Rad;
        Vector2 forward = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        //Vector2 forward = currentContext.transform.right * Mathf.Sign(currentContext.transform.localScale.x);

        // Forward direction (taking scale into account)
        //Vector2 forward = origin.right * Mathf.Sign(origin.localScale.x);

        // Half angle
        float halfFOV = fovAngle / 2f;

        // Left boundary
        Quaternion leftRot = Quaternion.AngleAxis(-halfFOV, Vector3.forward);
        Vector3 leftBoundary = leftRot * forward * detectionRange;

        // Right boundary
        Quaternion rightRot = Quaternion.AngleAxis(halfFOV, Vector3.forward);
        Vector3 rightBoundary = rightRot * forward * detectionRange;

        // Draw FOV lines
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin.position, origin.position + leftBoundary);
        Gizmos.DrawLine(origin.position, origin.position + rightBoundary);

        // Optional: forward line
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin.position, origin.position + (Vector3)forward * detectionRange);
        #endregion
    }


    #region Dectection

    [Header("---------FOV & Detection-------------")]
    public float detectionRange = 1f;
    public float ledgeDtectionRange = 1f;
    public float fovAngle = 60f;
    public float lookDirection;
    public float reactionTime = 0.2f;
    #endregion




    #region Idle

    [Header("Idle")]
    public float maxIdleTime = 5f;
    public float randIdleWaitTime;
    public float idleEnterTime;
    #endregion

    #region patrol
    //[Header("Patrol")]
    //public float maxPatrolTime = 5f;
    //public float randPatrolTime;
    //public float patrolEnterTime;
    #endregion


    #region Chase
    //[Header("Chase")]
    //public float chaseSpeed;
    //public float catchDistance = 1;
    public Vector2 targetLastSeenPos;
    #endregion

    #region Search
    //[Header("Search")]
    //public float searchSpeed;
    //public float maxSearchTime = 5f;
    //public float randSearchTime;
    //public float searchEnterTime;
    #endregion

    #region Attack
    [Header("Attack")]
    public float attackDistance = 1f;

    #endregion






}

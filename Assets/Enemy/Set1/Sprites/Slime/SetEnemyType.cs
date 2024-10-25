using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using static EnemyType;
public class SetEnemyType : Action
{
    public EnemyType enemyType;
    [Header("Components")]
    public SharedGameObject selfGameobject;
    public Rigidbody2D mobRb;
    public Animator mobAnimator;
    public CapsuleCollider2D mobCollider;
    public SharedFloat mobHeight;
    public SharedFloat mobWidth;
    public LayerMask whatIsGround;
    [Header("Stats")]
    public _movementType movementType;
    public float tickSpeed;
    public SharedFloat health;
    public SharedFloat defence;
    public SharedFloat dodgeChance;


    [Header("Patrol")]
    public _patrolType patrolType;
    public SharedFloat patrolSpeed;
    public SharedFloat extraGroundCheckDistance;
    public SharedFloat scanRange = 1f;

    [Header("Chase")]
    public _detectionType detectionType;
    public SharedFloat detectionDistance;
    public SharedFloat chaseSpeed;

    [Header("attack")]
    public _attackType attackType;
    public SharedFloat attackSpeed;
    public SharedFloat attackRange;
    public SharedFloat attackTime;
    public SharedFloat attackDmg;
    //public SharedInt[] attacks;

    //[Header("Extras")]
    //public SharedBool canJump;
    //public SharedFloat agroRange;
    //public SharedFloat Aggressiveness;
    //public SharedFloat Fear;// gets lower and when it reaches 0 the mob runs from player
    //public SharedFloat Intelligence;//
    public override void OnAwake()
    {
        //components
        selfGameobject.Value = gameObject;
        mobAnimator = gameObject.GetComponent<Animator>();//.mobAnimator;
        mobCollider = gameObject.GetComponent<CapsuleCollider2D>();//enemyType.mobCollider;
        mobRb = gameObject.GetComponent<Rigidbody2D>();//enemyType.mobRb;
        mobWidth.Value = mobCollider.size.x;//enemyType.mobWidth;
        mobHeight.Value = mobCollider.size.y;
        whatIsGround = enemyType.whatIsGround;
        //stats
        movementType = enemyType.movementType;
        tickSpeed = enemyType.tickSpeed;
        health.Value = enemyType.health;
        defence.Value = enemyType.defence;
        dodgeChance.Value = enemyType.dodgeChance;
        //patrol
        patrolType = enemyType.patrolType;
        patrolSpeed.Value = enemyType.patrolSpeed;
        //Debug.Log(patrolSpeed + " set");
        extraGroundCheckDistance.Value = enemyType.extraGroundCheckDistance;
        scanRange.Value = enemyType.scanRange;


        //chase
        detectionType = enemyType.detectionType;
        detectionDistance.Value = enemyType.detectionDistance;
        chaseSpeed.Value = enemyType.chaseSpeed;
        //attack
        attackType = enemyType.attackType;
        attackSpeed.Value = enemyType.attackSpeed;
        attackRange.Value = enemyType.attackRange;
        attackTime.Value = enemyType.attackTime;
        attackDmg.Value = enemyType.attackDmg;
        //attacks = enemyType.attacks[];
    }
    //public override void OnStart()
    //{
    //    base.OnStart();
    //}
    //public override TaskStatus OnUpdate()
    //{
    //    return TaskStatus.Success;
    //}
}

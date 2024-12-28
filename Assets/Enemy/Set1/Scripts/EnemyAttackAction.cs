using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
//using System.Diagnostics;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EnemyAttackAction : Action
{
    

    //attack refrences
    public GameObject atkObj;
    public Animator atkAnimator;
    public SharedTransform target;
    //attack variables
    public SharedFloat atkRange;
    public SharedFloat atkTime;
    //public float atkDistance;
    public Vector2 atkPosition;
    public float atkRotation;
    //public SharedCollider collider;
    public SharedCapsulCollider2D mobCollider;
    public SharedFloat mobHeight;
    public SharedFloat mobWidth;
    //
    public float cd;
    public bool isAttacking;

    public override void OnAwake()
    {
        

    }
    #region Attack
    public override void OnStart()
    {
        Debug.Log("starts attack 0");
        mobCollider = GetComponent<CapsuleCollider2D>();
        // attack horizontally
        float atkDistance = Mathf.Sign(transform.localScale.x) * (mobWidth.Value / 2 + atkRange.Value);
        atkPosition = new Vector2(transform.position.x + atkDistance, transform.position.y);
        atkRotation = 0f;
        //set position and rotation
        atkObj.transform.position = atkPosition;
        atkObj.transform.eulerAngles = new Vector3(0f, 0f, atkRotation);
        //attack and disable attack after attackTime
        atkObj.SetActive(true);
        atkAnimator.SetBool("Attack", true);
        //MonoBehaviour.Invoke(nameof(StopAttacking), 0.5f);// atkTime);

    }
    #endregion
    /**/
    /*
    public override void OnStart()
    {
        #region
        //Debug.Log("attack2");
        //base.OnStart();
        mobCollider = GetComponent<CapsuleCollider2D>();
        //collider.Value = mobCollider;
        //mobWidth = mobCollider.Value.size.x;
        //mobHeight = mobCollider.size.y;
        /
        
        
        
        Debug.Log("onstart");
        //cd = atkTime.Value;
        #endregion
    }

    public override TaskStatus OnUpdate()
    {
        #region
        //if close enough
        //set attacking true
        if (Vector3.SqrMagnitude(transform.position - target.Value.position) > 0.1f && !isAttacking && cd<=0)
        {
            Debug.Log("start atk");
            isAttacking = true;
            #region
            Debug.Log("attack1");
            //collider.Value = mobCollider;
            //mobWidth = mobCollider.Value.size.x;
            //mobHeight = mobCollider.size.y;

            //attack horizontally
            float atkDistance = Mathf.Sign(transform.localScale.x) * (mobWidth.Value / 2 + atkRange.Value);
            atkPosition = new Vector2(transform.position.x + atkDistance, transform.position.y);
            atkRotation = 0f;
            //set position and rotation
            atkObj.transform.position = atkPosition;
            atkObj.transform.eulerAngles = new Vector3(0f, 0f, atkRotation);
            //attack and disable attack after attackTime
            atkObj.SetActive(true);
            atkAnimator.SetBool("Attack", true);
            //MonoBehaviour.Invoke(nameof(StopAttacking), atkTime);
            cd = atkTime.Value;
            #endregion


            


        }

        #endregion
        #region
        cd -= Time.deltaTime;
        if (cd < 0f)
        {
            Debug.Log("cd reset");
            cd = 0f;
            isAttacking = false;
            atkAnimator.SetBool("Attack", false);
            atkObj.SetActive(false);
            Debug.Log("disbale atk");
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
        #endregion
        //return TaskStatus.Running;
    }


    /**/

    public void StopAttacking()
    {
        Debug.Log("stop atk0");
        atkAnimator.SetBool("Attack", false);
        atkObj.SetActive(false);

    }




}
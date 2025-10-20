using UnityEngine;
//using SM;

public abstract class GroundNpcState : ScriptableObject
{

    public float duration;
    public string animationName;
    protected float enterTime { get; set; }
    protected float updateTime { get; set; }
    protected float fixedTime { get; set; }
    protected float lateTime { get; set; }
    protected float exitTime { get; set; }

    protected GroundNpcStateMachine stateMachine;
    protected GroundNpcStateFactory factory;
    protected GroundNpcContext currentContext;

    public void Initialize(GroundNpcStateMachine stateMachine, GroundNpcStateFactory factory, GroundNpcContext currentContext)
    {
        this.stateMachine = stateMachine;
        this.factory = factory;
        this.currentContext = currentContext;
    }
    public virtual void OnEnter()
    {
        //Debug.Log("Enter State: " + this.ToString());
        stateMachine.currentStateName = this.ToString();
        enterTime = Time.time;
        if (animationName != "") currentContext.animatorController.PlayAnimation(animationName);
    }


    public virtual void OnUpdate()
    {
        updateTime += Time.deltaTime;
    }

    public virtual void OnFixedUpdate()
    {
        fixedTime += Time.deltaTime;
    }
    public virtual void OnLateUpdate()
    {
        lateTime += Time.deltaTime;
        CheckSwitchState();
    }

    public virtual void OnExit()
    {
        exitTime = Time.time;
    }
    public virtual void CheckSwitchState()
    {

    }
    protected void SwitchState(GroundNpcState newState)
    {
        OnExit();
        newState.OnEnter();
        stateMachine.currentState = newState;
    }



    public virtual void OnTriggerEnter2D(Collider2D other) { /*Debug.Log("te");*/ }
    public virtual void OnTriggerExit2D(Collider2D other) { /*Debug.Log("tx");*/ }
    public virtual void OnCollisionEnter2D(Collision2D collision) { /*Debug.Log("ce");*/ }
    public virtual void OnCollisionExit2D(Collision2D collision) { /*Debug.Log("cx");*/ }


    #region Passthrough Methods

    /// <summary>
    /// Removes a gameobject, component, or asset.
    /// </summary>
    /// <param name="obj">The type of Component to retrieve.</param>
    protected static void Destroy(UnityEngine.Object obj)
    {
        UnityEngine.Object.Destroy(obj);
    }

    /// <summary>
    /// Returns the component of type T if the game object has one attached, null if it doesn't.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetComponent<T>() where T : Component { return stateMachine.GetComponent<T>(); }

    /// <summary>
    /// Returns the component of Type <paramref name="type"/> if the game object has one attached, null if it doesn't.
    /// </summary>
    /// <param name="type">The type of Component to retrieve.</param>
    /// <returns></returns>
    protected Component GetComponent(System.Type type) { return stateMachine.GetComponent(type); }

    /// <summary>
    /// Returns the component with name <paramref name="type"/> if the game object has one attached, null if it doesn't.
    /// </summary>
    /// <param name="type">The type of Component to retrieve.</param>
    /// <returns></returns>
    protected Component GetComponent(string type) { return stateMachine.GetComponent(type); }
    #endregion
}


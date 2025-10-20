using UnityEngine;

//namespace SM
//{
public class StateMachine : MonoBehaviour
{
    public string customName;
    [Header("-----DEBUGGING-----")]
    public string currentStateName;

    #region  Current Movement Values
    [Header("Current Movement")]
    [SerializeField] public float c_HSpeed;
    [SerializeField] public float c_MaxHSpeed;
    [SerializeField] public float c_Acceleration;
    [SerializeField] public float c_Deceleration;
    #endregion



    

    #region Refrences
    [Header("-----STATE MACHINE-----")]
    public StateFactory factory;
    public Context currentContext;
    public _States currentEnumState;


    public StatesList playerStates;
    public _States initalState;
    [HideInInspector] public State mainState;
    [HideInInspector] public State currentState;
    [HideInInspector] public State currentParallelState;
    #endregion

    private void Initialize()
    {
        factory = new StateFactory(this, playerStates);
        currentState = factory.GetState(initalState);
        currentState.OnEnter();
    }
    private void Awake()
    {
        //Initialize States
        Initialize();
    }


    private void Start()
    {
        currentContext.ContextStart();
    }

    void Update()
    {
        currentContext.ContextUpdate();
        if (currentState != null)
            currentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.OnFixedUpdate();
    }

    private void LateUpdate()
    {
        if (currentState != null)
            currentState.OnLateUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentState?.OnTriggerEnter2D(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        currentState?.OnTriggerExit2D(other);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState?.OnCollisionEnter2D(collision);
    }

    
    private void OnCollisionExit2D(Collision2D collision)
    {
        currentState?.OnCollisionExit2D(collision);
    }

}

//}

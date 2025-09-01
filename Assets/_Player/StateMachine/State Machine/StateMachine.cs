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


    public StatesList playerStates;
    [HideInInspector] public State mainState;
    [HideInInspector] public State currentState;
    [HideInInspector] public State currentParallelState;
    #endregion

    private void Initialize()
    {
        factory = new StateFactory(this, playerStates);
        currentState = factory.GetState(_States.Fall);
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


}

//}

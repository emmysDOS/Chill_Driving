using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private InputManager _inputManager;
    private wayPointHandler _wayPointHandler;
    
    [Header("Throttle")]
    [SerializeField] private float targetSpeed;
    public float currentSpeed;
    
    private float _maxSpeed;
    [Space]
    [SerializeField] private float maxFwSpeed;
    [SerializeField] private float maxBwSpeed;
    
    private float _acceleration;
    [Space]
    [SerializeField] private float throttleAcceleration;
    [SerializeField] private float restAcceleration;

    [Space(2)]
    [Header("Steering")]
    [SerializeField] private float targetTurn;
    [SerializeField] private float currentTurn;
    
    [Space]
    [SerializeField] private float turnFactor;
    [SerializeField] private float turnAcceleration;
    [SerializeField] private float positiveTurnAcceleration;
    [SerializeField] private float negativeTurnAcceleration;


    [Space(2)]
    [Header("WayPomts")]
    public Transform[] wayPoints;
    public GameObject wayPointsParent;
    public Transform currentWayPoint;
    public byte currentWp;
    public float apSpeed;
    public float maxTurnAp;
    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _wayPointHandler = GetComponent<wayPointHandler>();
        wayPoints = new Transform[wayPointsParent.transform.childCount];
        for (int i = 0; i < wayPoints.Length; i++)
            wayPoints[i] = wayPointsParent.transform.GetChild(i);
    }
    private void FixedUpdate()
    {
        HandleThrottle(_inputManager.motorInput);
        HandleSteering(_inputManager.turnInput);
        //FollowWayPoint();
    }
    private void HandleThrottle(float motorInput)
    {
        targetSpeed = _maxSpeed * motorInput;
        currentSpeed = Mathf.SmoothStep(currentSpeed, targetSpeed, Time.deltaTime * _acceleration);

        //Calculate max speed depending on car's direction
        if (motorInput > 0)
            _maxSpeed = maxFwSpeed;
        else
            _maxSpeed = maxBwSpeed;
        
        if (motorInput != 0)
            _acceleration = throttleAcceleration;
        else
            _acceleration = restAcceleration;
        
        transform.position += transform.forward * currentSpeed  * _acceleration * Time.deltaTime;
    }

    private void HandleSteering(float turnInput)
    {
        targetTurn = turnFactor * turnInput;
        currentTurn = Mathf.SmoothStep(currentTurn, targetTurn, Time.deltaTime * turnAcceleration);

        bool isForward = false;
        isForward = currentSpeed > 0;

        if (isForward)
            turnInput = -turnInput;
        
        //This makes stopping turning faster than turning
        if (turnInput != 0)
            turnAcceleration = positiveTurnAcceleration;
        else
            turnAcceleration = negativeTurnAcceleration;

        //Applies rotation
        transform.Rotate(0, currentTurn, 0,Space.World);
    }

    private void FollowWayPoint()
    {
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, wp.transform.rotation, maxTurnAp);
       
        for (int i = 0; i < wayPoints.Length; i++)
        {
            if (transform.position != wayPoints[i].transform.position)
                transform.position = Vector3.MoveTowards(transform.position, wayPoints[i].transform.position, apSpeed * Time.deltaTime );

                
            currentWayPoint = wayPoints[i];
        }
    }
}

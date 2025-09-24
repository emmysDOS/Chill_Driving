using System;
using UnityEngine;

public class WheelMeshHandler : MonoBehaviour
{
    private InputManager _inputManager;
    private CarController _carController;

    [SerializeField] private Transform[] turnWheels;
    [SerializeField] private Transform[] wheels;

    [SerializeField] private GameObject turnWheelsParent;
    [SerializeField] private GameObject WheelsParent;

    [SerializeField] private float turnAcceleration;
    [SerializeField] private byte maxTurn;

    private int _yRot;
    public float speedFactor;

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _carController = GetComponent<CarController>();

        turnWheels = new Transform[ turnWheelsParent.transform.childCount];
        for (int i = 0; i < turnWheels.Length; i++)
            turnWheels[i] = turnWheelsParent.transform.GetChild(i);
        
        wheels = new Transform[WheelsParent.transform.childCount];
        for (int i = 0; i < WheelsParent.transform.childCount; i++)
            wheels[i] = WheelsParent.transform.GetChild(i);
    }

    private void Update()
    {
        HandleTurn();
        //HandleSpeed();
    }

    private void HandleTurn()
    {
        if (_inputManager.turnInput > 0)
            _yRot = maxTurn;
        else if (_inputManager.turnInput < 0)
            _yRot =  -maxTurn;
        else
            _yRot = 0;
        
        foreach (Transform wheel in turnWheels)
            wheel.localRotation = Quaternion.Lerp(wheel.localRotation, Quaternion.Euler(0, _yRot, 90), turnAcceleration);
    }
    //Speed needs to be fixed
    private void HandleSpeed()
    {
        float rotation = _carController.currentSpeed * speedFactor;
        foreach (Transform wheel in wheels)
            wheel.localRotation = new Quaternion(wheel.localRotation.x,wheel.localRotation.y , rotation,90);
    }
}

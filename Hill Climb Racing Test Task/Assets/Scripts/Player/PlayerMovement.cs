using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Rigidbodies")]
    [SerializeField] private Rigidbody2D frontWheelRb;
    [SerializeField] private Rigidbody2D backWheelRb;
    [SerializeField] private Rigidbody2D vehicleRb;

    [Header("Vehicle Settings")] 
    [SerializeField] private float vehicleSpeed = 100f;
    [SerializeField] private float rotationSpeed = 300f;

    [Header("Pedals Events")]
    public ButtonEvents buttonEvents;

    private bool _isGasPressed;
    private bool _isBrakePressed;

    private float _moveInput; //For keyboard input

    private void Start()
    {
        buttonEvents.OnGasButtonHoldStart += GasPressed;
        buttonEvents.OnGasButtonHoldEnd += GasReleased;
        buttonEvents.OnBrakeButtonHoldStart += BrakePressed;
        buttonEvents.OnBrakeButtonHoldEnd += BrakeReleased;
    }

    private void GasPressed()
    {
        _isGasPressed = true;
    }

    private void GasReleased()
    {
        _isGasPressed = false;
    }

    private void BrakePressed()
    {
        _isBrakePressed = true;
    }

    private void BrakeReleased()
    {
        _isBrakePressed = false;
    }

    private void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        ManageControls();
    }

    private void ManageControls()
    {
        if (_isGasPressed)
        {
            ApplyGas();
        }

        if (_isBrakePressed)
        {
            ApplyBrake();
        }

        if (!_isGasPressed && !_isBrakePressed)
        {
            NeutralDriving();
        }
    }

    private void ApplyGas()
    {
        frontWheelRb.AddTorque(-vehicleSpeed * Time.fixedDeltaTime);
        backWheelRb.AddTorque(-vehicleSpeed * Time.fixedDeltaTime);
        vehicleRb.AddTorque(rotationSpeed * Time.fixedDeltaTime);
    }

    private void ApplyBrake()
    {
        frontWheelRb.AddTorque(vehicleSpeed * Time.fixedDeltaTime);
        backWheelRb.AddTorque(vehicleSpeed * Time.fixedDeltaTime);
        vehicleRb.AddTorque(-rotationSpeed * Time.fixedDeltaTime);
    }

    //For keyboard input
    private void NeutralDriving() 
    {
        frontWheelRb.AddTorque(-_moveInput * vehicleSpeed * Time.fixedDeltaTime);
        backWheelRb.AddTorque(-_moveInput * vehicleSpeed * Time.fixedDeltaTime);
        vehicleRb.AddTorque(_moveInput * rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnDestroy()
    {
        buttonEvents.OnGasButtonHoldStart -= GasPressed;
        buttonEvents.OnGasButtonHoldEnd -= GasReleased;
        buttonEvents.OnBrakeButtonHoldStart -= BrakePressed;
        buttonEvents.OnBrakeButtonHoldEnd -= BrakeReleased;
    }
}
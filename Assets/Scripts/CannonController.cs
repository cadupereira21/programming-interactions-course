using System;
using UnityEngine;

public class CannonController : MonoBehaviour {
    
    [SerializeField]
    private Transform barrelTransform;
    
    [Header("Barrel Tilt Settings")]
    [SerializeField]
    private float tiltSpeed = 10f;

    [SerializeField]
    private float maxTiltAngle;
    
    [SerializeField]
    private float minTiltAngle;

    [Header("Canon Rotation Settings")]

    [SerializeField] 
    private float rotationSpeed;

    [SerializeField]
    private float maxRotationAngle;
    
    [SerializeField]
    private float minRotationAngle;
    
    [Header("Wheels Rotation Settings")]
    
    [SerializeField]
    private CanonWheels canonWheels;

    [SerializeField] 
    private float wheelsRotationSpeed;
    
    private SimpleControls _controls;
    
    private Vector2 _currentTilt = Vector2.zero;

    private Vector2 _currentRotation;

    private void Awake() {
        _controls = new SimpleControls();
        barrelTransform.localEulerAngles = Vector3.zero;
    }

    private void Start() {
        _currentRotation = this.transform.localEulerAngles;
        maxRotationAngle += _currentRotation.y;
        minRotationAngle += _currentRotation.y;
        
        Debug.Log(_currentRotation);
        Debug.Log(maxRotationAngle);
        Debug.Log(minRotationAngle);
    }

    private void OnEnable() {
        _controls.Enable();
    }

    private void OnDisable() {
        _controls.Disable();
    }

    private void Update() {
        Vector2 input = _controls.gameplay.move.ReadValue<Vector2>();
        if (input.y != 0) {
            TiltBarrel(input.y);
        }

        if (input.x != 0) {
            RotateCanon(input.x);
        }
    }
    
    private void TiltBarrel(float direction) {
        float tiltDelta = direction * tiltSpeed * Time.deltaTime;
        _currentTilt.x = Mathf.Clamp(_currentTilt.x + tiltDelta, minTiltAngle, maxTiltAngle);
        barrelTransform.localEulerAngles = _currentTilt;
    }
    
    private void RotateCanon(float direction) {
        float rotationDelta = direction * rotationSpeed * Time.deltaTime;
        _currentRotation.y = Mathf.Clamp(_currentRotation.y + rotationDelta, minRotationAngle, maxRotationAngle);
        if (_currentRotation.y > minRotationAngle + 0.1 && _currentRotation.y < maxRotationAngle - 0.1) {
            RotateWheels(direction);
        }
        this.transform.localEulerAngles = _currentRotation;
    }
    
    private void RotateWheels(float direction) {
        float rotationDelta = direction * wheelsRotationSpeed * Time.deltaTime;
        
        canonWheels.leftBackWheel.transform.Rotate(Vector3.right, rotationDelta);
        canonWheels.rightBackWheel.transform.Rotate(Vector3.right, -rotationDelta);
        canonWheels.leftFrontWheel.transform.Rotate(Vector3.right, rotationDelta);
        canonWheels.rightFrontWheel.transform.Rotate(Vector3.right, -rotationDelta);
    }

    [Serializable]
    public struct CanonWheels {
        public GameObject leftBackWheel;
        public GameObject rightBackWheel;
        public GameObject leftFrontWheel;
        public GameObject rightFrontWheel;
    }
}

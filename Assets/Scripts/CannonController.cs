using System;
using UnityEngine;

public class CannonController : MonoBehaviour {
    
    [SerializeField]
    private Transform barrelTransform;
    
    [SerializeField]
    private float tiltSpeed = 10f;

    [SerializeField]
    private float maxTiltAngle;
    
    [SerializeField]
    private float minTiltAngle;
    
    private SimpleControls _controls;
    
    private float _currentTilt;

    private void Awake() {
        _controls = new SimpleControls();
        barrelTransform.localEulerAngles = Vector3.zero;
    }

    private void OnEnable() {
        _controls.Enable();
    }

    private void OnDisable() {
        _controls.Disable();
    }

    private void Update() {
        Vector2 input = _controls.gameplay.move.ReadValue<Vector2>();
        if (input != Vector2.zero) {
            TiltBarrel(input.y);
        }
    }
    
    private void TiltBarrel(float direction) {
        float tiltDelta = direction * tiltSpeed * Time.deltaTime;
        _currentTilt = Mathf.Clamp(_currentTilt + tiltDelta, minTiltAngle, maxTiltAngle);
        barrelTransform.localEulerAngles = new Vector3(_currentTilt, 0f, 0f);
    }
}

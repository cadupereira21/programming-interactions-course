using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour {
    
    [SerializeField] private Transform barrelTransform;
    
    [Header("Barrel Tilt Settings")]
    [SerializeField] private float tiltSpeed = 10f;
    [SerializeField] private float maxTiltAngle;
    [SerializeField] private float minTiltAngle;

    [Header("Cannon Rotation Settings")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxRotationAngle;
    [SerializeField] private float minRotationAngle;
    
    [Header("Wheels Rotation Settings")]
    [SerializeField] private CannonWheels cannonWheels;
    [SerializeField] private float wheelsRotationSpeed;

    [Header("Fire Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private float fireForce = 30f;
    [SerializeField] private float fireCooldown = 1f;
    
    private Vector2 _currentTilt = Vector2.zero;
    private Vector2 _currentRotation;

    private bool _canFire = true;

    private void Awake() {
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
    
    public void TiltBarrel(float direction) {
        float tiltDelta = direction * tiltSpeed * Time.deltaTime;
        _currentTilt.x = Mathf.Clamp(_currentTilt.x + tiltDelta, minTiltAngle, maxTiltAngle);
        barrelTransform.localEulerAngles = _currentTilt;
    }

    public void Rotate(float direction) {
        float rotationDelta = direction * rotationSpeed * Time.deltaTime;
        _currentRotation.y = Mathf.Clamp(_currentRotation.y + rotationDelta, minRotationAngle, maxRotationAngle);
        if (_currentRotation.y > minRotationAngle + 0.1 && _currentRotation.y < maxRotationAngle - 0.1) {
            RotateWheels(direction);
        }
        this.transform.localEulerAngles = _currentRotation;
    }
    
    public void Fire() {
        if (!_canFire) {
            Debug.Log("[CannonController] Cannon is on cooldown.");
            return;
        }
        
        _canFire = false;
        GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPosition.position, projectileSpawnPosition.rotation);
        
        if (projectileInstance.TryGetComponent(out Rigidbody rb)) {
            rb.AddForce(projectileSpawnPosition.forward * fireForce, ForceMode.Impulse);
        } else {
            Debug.LogError("Projectile prefab does not have a Rigidbody component.");
        }

        this.StartCoroutine(WaitToFire());
    }
    
    private void RotateWheels(float direction) {
        float rotationDelta = direction * wheelsRotationSpeed * Time.deltaTime;
        
        cannonWheels.leftBackWheel.transform.Rotate(Vector3.right, rotationDelta);
        cannonWheels.rightBackWheel.transform.Rotate(Vector3.right, -rotationDelta);
        cannonWheels.leftFrontWheel.transform.Rotate(Vector3.right, rotationDelta);
        cannonWheels.rightFrontWheel.transform.Rotate(Vector3.right, -rotationDelta);
    }

    private IEnumerator WaitToFire() {
        yield return new WaitForSeconds(fireCooldown);
        _canFire = true;
    }

    [Serializable]
    public struct CannonWheels {
        public GameObject leftBackWheel;
        public GameObject rightBackWheel;
        public GameObject leftFrontWheel;
        public GameObject rightFrontWheel;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float lookSenstivity = 3f;

    [SerializeField]
    GameObject fpsCamera;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraUpAndDownRotation = 0f;
    private float currentCameraUpAndDownRotation = 0f;

    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate Movement Velocity as 3D Vector
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;

        // Final Movement Velocity
        Vector3 _movementVelocity = (_movementHorizontal + _movementVertical).normalized * speed;

        // Apply Movement
        Move(_movementVelocity);

        // Calculate Rotation as a 3D Vector for turning around
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0, _yRotation, 0) * lookSenstivity;

        // Apply Rotation
        Rotate(_rotationVector);

        // Calculate Camera up down rotation 
        float _cameraUpDownRotation = Input.GetAxis("Mouse Y") * lookSenstivity;

        // Apply rotation
        CameraRotation(_cameraUpDownRotation);
    
    }

    private void FixedUpdate()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if(fpsCamera != null)
        {
            currentCameraUpAndDownRotation -= cameraUpAndDownRotation;
            currentCameraUpAndDownRotation = Mathf.Clamp(currentCameraUpAndDownRotation, -85, 85);

            fpsCamera.transform.localEulerAngles = new Vector3(currentCameraUpAndDownRotation, 0, 0);
        }
    }

    void Move(Vector3 movementVelocity)
    {
        velocity = movementVelocity;
    }

    void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }

    void CameraRotation(float cameraUpDownRotation)
    {
        cameraUpAndDownRotation = cameraUpDownRotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacterController : MonoBehaviour
{
    [SerializeField]
    private float accelerationForce = 10;

    [SerializeField]
    private float maxSpeed = 2;

    [SerializeField]
    [Tooltip("1 = instant turning, 0 = no turning")]
    [Range(0,1)]
    private float turnSpeed = 0.1f;

    [SerializeField]
    private PhysicMaterial movingPhysicsMaterial, stoppingPhysicsMaterial;

    private new Rigidbody rigidbody;
    private Vector2 input;
    private new Collider collider;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        UpdateInput();
    }

    private void FixedUpdate()
    {
        // The horizontal and vertical input from a gamepad / joystick
        // stored in a Vector3 because we will use it to move a player on an X/Z plane.
        var inputDirection = new Vector3(input.x, 0, input.y);

        // I am assuming this is essentially converting the forward vector of a 3D
        // camera to a 2D x/Z plane? I made these variable names up based on this assumption...
        var cameraFlattenedForward = Camera.main.transform.forward;
        cameraFlattenedForward.y = 0;

        // This function "creates a rotation with  the specified forward and up directions)
        // https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
        var cameraRotation = Quaternion.LookRotation(cameraFlattenedForward);

        // If I move a character based on this input, it moves relative to the camera
        // instead of the world
        Vector3 cameraRelativeInput = cameraRotation * inputDirection;

        collider.material = inputDirection.magnitude > 0 ? movingPhysicsMaterial : stoppingPhysicsMaterial;

        if (rigidbody.velocity.magnitude < maxSpeed)
        {
            rigidbody.AddForce(cameraRelativeInput * accelerationForce, ForceMode.Acceleration);
        }

        if (inputDirection.magnitude > 0)
        {
            var targetRotation = Quaternion.LookRotation(cameraRelativeInput);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed);
        }

        Debug.Log($"Player velocity: {rigidbody.velocity}");
    }
    private void UpdateInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }
}

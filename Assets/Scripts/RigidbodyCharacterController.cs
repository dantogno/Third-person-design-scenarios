using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void FixedUpdate()
    {
        // The horizontal and vertical input from a gamepad / joystick
        // stored in a Vector3 because we will use it to move a player on an X/Z plane.
        var movementDirection = new Vector3(input.x, 0, input.y);

        var cameraFlattenedForward = Camera.main.transform.forward;
        cameraFlattenedForward.y = 0;

        // This function "creates a rotation with  the specified forward and up directions)
        // https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
        var cameraRotation = Quaternion.LookRotation(cameraFlattenedForward);

        // If I move a character based on this input, it moves relative to the camera
        // instead of the world
        Vector3 cameraRelativeMovementDirection = cameraRotation * movementDirection;

        UpdatePhysicsMaterial(cameraRelativeMovementDirection);
        Move(cameraRelativeMovementDirection);
        RotateToFaceMovementDirection(cameraRelativeMovementDirection);
    }

    /// <summary>
    /// Rotate the character to face the direction it is trying to move in.
    /// </summary>
    /// <param name="cameraRelativeInputDirection"></param>
    private void RotateToFaceMovementDirection(Vector3 cameraRelativeInputDirection)
    {
        if (cameraRelativeInputDirection.magnitude > 0)
        {
            var targetRotation = Quaternion.LookRotation(cameraRelativeInputDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed);
        }
    }

    /// <summary>
    /// If the character is not trying to move, apply a physics material with
    /// greater friction. If the playes is trying to move, aplly a physics material with less
    /// friction.
    /// </summary>
    /// <param name="movementDirection">Movement input</param>
    private void UpdatePhysicsMaterial(Vector3 movementInput)
    {
        collider.material =
            movementInput.magnitude > 0 ? movingPhysicsMaterial : stoppingPhysicsMaterial;
    }

    /// <summary>
    /// Move the character in the given direction based on it's max speed and acceleration.
    /// </summary>
    /// <param name="moveDirection">Direction to move in.</param>
    private void Move(Vector3 moveDirection)
    {
        if (rigidbody.velocity.magnitude < maxSpeed)
        {
            rigidbody.AddForce(moveDirection * accelerationForce, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// Called by Unity InputSystem component when movement input is detected.
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
}

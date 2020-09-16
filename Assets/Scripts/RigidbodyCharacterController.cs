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
        var inputDirection = new Vector3(input.x, 0, input.y);

        var cameraFlattenedForward = Camera.main.transform.forward;
        cameraFlattenedForward.y = 0;

        var cameraRotation = Quaternion.LookRotation(cameraFlattenedForward);

        Vector3 cameraRelativeInput = cameraRotation * inputDirection;

        collider.material = inputDirection.magnitude > 0 ? movingPhysicsMaterial : stoppingPhysicsMaterial;

        if (rigidbody.velocity.magnitude < maxSpeed)
        {
            rigidbody.AddForce(cameraRelativeInput * accelerationForce, ForceMode.Acceleration);
        }

        Debug.Log($"Player velocity: {rigidbody.velocity}");
    }
    private void UpdateInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }
}

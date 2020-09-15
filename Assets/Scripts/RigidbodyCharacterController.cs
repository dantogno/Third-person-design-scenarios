using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacterController : MonoBehaviour
{
    [SerializeField]
    private float accelerationForce = 2;

    private new Rigidbody rigidbody;
    private Vector2 input;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateInput();
    }

    private void FixedUpdate()
    {
        var inputDirection = new Vector3(input.x, 0, input.y);
        rigidbody.AddForce(inputDirection * accelerationForce, ForceMode.Acceleration);
    }
    private void UpdateInput()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
    }
}

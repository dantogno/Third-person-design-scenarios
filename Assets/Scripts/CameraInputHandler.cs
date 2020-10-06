using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputHandler : MonoBehaviour
{
    private CinemachineFreeLook cinemachineFreeLook;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        //Debug.Log($"Look X: {input.x} Mouse Y: {input.y}");
        cinemachineFreeLook.m_XAxis.Value += input.x;
        cinemachineFreeLook.m_YAxis.Value += input.y;
    }
}

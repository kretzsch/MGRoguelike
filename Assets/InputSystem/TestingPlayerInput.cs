using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TestingPlayerInput : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.FPS.Enable();
        playerInputActions.FPS.Jump.performed += Jump;
    }
    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.FPS.Movement.ReadValue<Vector2>();
        Debug.Log(inputVector);
        float speed = 5f;
        _rigidBody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }


    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Debug.Log("Jumping" + callbackContext.phase);
            _rigidBody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}

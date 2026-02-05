using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    InputAction btuAction;
    InputAction wheelAction;

    [SerializeField]
    GameObject BTU;
    [SerializeField]
    GameObject wheel;

    void OnEnable()
    {
        btuAction = InputSystem.actions.FindAction("BTU");
        wheelAction = InputSystem.actions.FindAction("Wheel");


        btuAction.performed += OnBTUPerformed;
        btuAction.canceled += OnCanceled;
        wheelAction.performed += OnWheelPerformed;
        wheelAction.canceled += OnCanceled;

        btuAction.Enable();
    }

    void OnDisable()
    {
        btuAction.performed -= OnBTUPerformed;
        btuAction.canceled -= OnCanceled;
        btuAction.Disable();
    }

    void OnBTUPerformed(InputAction.CallbackContext context)
    {
        DisableAll();
        BTU.SetActive(true);
    }
    void OnWheelPerformed(InputAction.CallbackContext context)
    {
        DisableAll();
        wheel.SetActive(true);
    }

    void OnCanceled(InputAction.CallbackContext context)
    {
        DisableAll();
    }

    void DisableAll()
    {
        BTU.SetActive(false);
        wheel.SetActive(false);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerInput : MonoBehaviour
{
    InputAction btuAction;
    InputAction wheelAction;
    InputAction boxingGloveAction;

    [SerializeField]
    GameObject BTU;
    [SerializeField]
    GameObject wheel;
    [SerializeField]
    GameObject boxingGlove;

    void OnEnable()
    {
        btuAction = InputSystem.actions.FindAction("BTU");
        wheelAction = InputSystem.actions.FindAction("Wheel");
        boxingGloveAction = InputSystem.actions.FindAction("BoxingGlove");

        btuAction.performed += OnBTUPerformed;
        btuAction.canceled += OnBTUCanceled;
        wheelAction.performed += OnWheelPerformed;
        wheelAction.canceled += OnWheelCanceled;
        boxingGloveAction.performed += OnBoxingGlovePerformed;
        boxingGloveAction.canceled += OnBoxingGloveCanceled;
    }

    void OnDisable()
    {
        btuAction.performed -= OnBTUPerformed;
        btuAction.canceled -= OnBTUCanceled;
        wheelAction.performed -= OnWheelPerformed;
        wheelAction.canceled -= OnWheelCanceled;
        boxingGloveAction.performed -= OnBoxingGlovePerformed;
        boxingGloveAction.canceled -= OnBoxingGloveCanceled;
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
    void OnBoxingGlovePerformed(InputAction.CallbackContext context)
    {
        DisableAll();
        boxingGlove.SetActive(true);
    }

    void OnBTUCanceled(InputAction.CallbackContext context)
    {
        BTU.SetActive(false);
    }
    void OnWheelCanceled(InputAction.CallbackContext context)
    {
        wheel.SetActive(false);
    }
    void OnBoxingGloveCanceled(InputAction.CallbackContext context)
    {
        boxingGlove.SetActive(false);
    }

    void DisableAll()
    {
        BTU.SetActive(false);
        wheel.SetActive(false);
        boxingGlove.SetActive(false);
    }
}

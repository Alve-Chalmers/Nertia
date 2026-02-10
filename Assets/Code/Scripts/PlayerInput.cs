using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerInput : MonoBehaviour
{
    InputAction btuAction;
    InputAction wheelAction;
    InputAction boxingGloveAction;
    InputAction gliderAction;
    InputAction dyingAction;

    [SerializeField]
    GameObject BTU;
    [SerializeField]
    GameObject wheel;
    [SerializeField]
    GameObject boxingGlove;
    [SerializeField]
    GameObject glider;
    [SerializeField]
    GameObject dying;

    void OnEnable()
    {
        btuAction = InputSystem.actions.FindAction("BTU");
        wheelAction = InputSystem.actions.FindAction("Wheel");
        boxingGloveAction = InputSystem.actions.FindAction("BoxingGlove");
        gliderAction = InputSystem.actions.FindAction("Glider");
        dyingAction = InputSystem.actions.FindAction("Dying");

        btuAction.performed += OnBTUPerformed;
        btuAction.canceled += OnBTUCanceled;
        wheelAction.performed += OnWheelPerformed;
        wheelAction.canceled += OnWheelCanceled;
        boxingGloveAction.performed += OnBoxingGlovePerformed;
        boxingGloveAction.canceled += OnBoxingGloveCanceled;
        gliderAction.performed += OnGliderPerformed;
        gliderAction.canceled += OnGliderCanceled;
        dyingAction.performed += OnDyingPerformed;
        dyingAction.performed += OnDyingCancelled;
    }

    void OnDisable()
    {
        btuAction.performed -= OnBTUPerformed;
        btuAction.canceled -= OnBTUCanceled;
        wheelAction.performed -= OnWheelPerformed;
        wheelAction.canceled -= OnWheelCanceled;
        boxingGloveAction.performed -= OnBoxingGlovePerformed;
        boxingGloveAction.canceled -= OnBoxingGloveCanceled;
        gliderAction.performed -= OnGliderPerformed;
        gliderAction.canceled -= OnGliderCanceled;
        dyingAction.performed -= OnDyingPerformed;
        dyingAction.performed -= OnDyingCancelled;
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
    void OnGliderPerformed(InputAction.CallbackContext context)
    {
        DisableAll();
        glider.SetActive(true);
    }

    void OnDyingPerformed(InputAction.CallbackContext context)
    {
        DisableAll();
        dying.SetActive(true);
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
    void OnGliderCanceled(InputAction.CallbackContext context)
    {
        glider.SetActive(false);
    }

    void OnDyingCancelled(InputAction.CallbackContext context)
    {
        dying.SetActive(false);
    }


    void DisableAll()
    {
        BTU.SetActive(false);
        wheel.SetActive(false);
        boxingGlove.SetActive(false);
        glider.SetActive(false);
        dying.SetActive(false);
    }
}

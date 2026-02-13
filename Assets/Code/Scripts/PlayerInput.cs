using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] SOEventPlayerAbilityType useAbility;
    [SerializeField] SOEventPlayerAbilityType cancelAbility;

    // We store the actions here
    private InputAction btuAction;
    private InputAction wheelAction;
    private InputAction boxingGloveAction;
    private InputAction gliderAction;
    private InputAction dyingAction;

    private void OnEnable() 
    {
        // 1. Find the actions
        // (Using 'InputSystem.actions' assumes you are using the global default actions)
        var actions = InputSystem.actions;
        if (actions == null)
        {
            Debug.LogError("PlayerInput: No InputSystem actions found!");
            return;
        }

        btuAction = actions.FindAction("BTU");
        wheelAction = actions.FindAction("Wheel");
        boxingGloveAction = actions.FindAction("BoxingGlove");
        gliderAction = actions.FindAction("Glider");
        dyingAction = actions.FindAction("Dying");

        // 2. Subscribe (Safely)
        // We check if the action was found before subscribing to avoid NullReferenceExceptions
        SubscribeAction(btuAction, OnBtuPerformed, OnBtuCanceled);
        SubscribeAction(wheelAction, OnWheelPerformed, OnWheelCanceled);
        SubscribeAction(boxingGloveAction, OnBoxingPerformed, OnBoxingCanceled);
        SubscribeAction(gliderAction, OnGliderPerformed, OnGliderCanceled);
        SubscribeAction(dyingAction, OnDyingPerformed, OnDyingCanceled);
    }

    private void OnDisable()
    {
        // 3. Unsubscribe
        // This is CRITICAL. Since we use named methods, C# knows exactly what to remove.
        UnsubscribeAction(btuAction, OnBtuPerformed, OnBtuCanceled);
        UnsubscribeAction(wheelAction, OnWheelPerformed, OnWheelCanceled);
        UnsubscribeAction(boxingGloveAction, OnBoxingPerformed, OnBoxingCanceled);
        UnsubscribeAction(gliderAction, OnGliderPerformed, OnGliderCanceled);
        UnsubscribeAction(dyingAction, OnDyingPerformed, OnDyingCanceled);
    }

    // --- Helper Methods to keep code clean ---
    
    private void SubscribeAction(InputAction action, System.Action<InputAction.CallbackContext> perform, System.Action<InputAction.CallbackContext> cancel)
    {
        if (action != null)
        {
            action.performed += perform;
            action.canceled += cancel;
        }
    }

    private void UnsubscribeAction(InputAction action, System.Action<InputAction.CallbackContext> perform, System.Action<InputAction.CallbackContext> cancel)
    {
        if (action != null)
        {
            action.performed -= perform;
            action.canceled -= cancel;
        }
    }

    // --- Named Callbacks ---
    // These methods provide a stable memory address for the event system to track.

    // BTU
    private void OnBtuPerformed(InputAction.CallbackContext ctx) => useAbility.Raise(PlayerAbilityType.BTU);
    private void OnBtuCanceled(InputAction.CallbackContext ctx) => cancelAbility.Raise(PlayerAbilityType.BTU);

    // Wheel
    private void OnWheelPerformed(InputAction.CallbackContext ctx) => useAbility.Raise(PlayerAbilityType.WHEEL);
    private void OnWheelCanceled(InputAction.CallbackContext ctx) => cancelAbility.Raise(PlayerAbilityType.WHEEL);

    // Boxing Glove
    private void OnBoxingPerformed(InputAction.CallbackContext ctx) => useAbility.Raise(PlayerAbilityType.BOXING_GLOVE);
    private void OnBoxingCanceled(InputAction.CallbackContext ctx) => cancelAbility.Raise(PlayerAbilityType.BOXING_GLOVE);

    // Glider
    private void OnGliderPerformed(InputAction.CallbackContext ctx) => useAbility.Raise(PlayerAbilityType.GLIDER);
    private void OnGliderCanceled(InputAction.CallbackContext ctx) => cancelAbility.Raise(PlayerAbilityType.GLIDER);

    // Dying
    private void OnDyingPerformed(InputAction.CallbackContext ctx) => useAbility.Raise(PlayerAbilityType.DYING);
    private void OnDyingCanceled(InputAction.CallbackContext ctx) => cancelAbility.Raise(PlayerAbilityType.DYING);
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputAction btuAction;
    InputAction wheelAction;
    InputAction boxingGloveAction;
    InputAction gliderAction;
    InputAction dyingAction;

    [SerializeField] SOEventPlayerAbilityType useAbility;
    [SerializeField] SOEventPlayerAbilityType cancelAbility;

    void OnEnable()
    {
        btuAction = InputSystem.actions.FindAction("BTU");
        wheelAction = InputSystem.actions.FindAction("Wheel");
        boxingGloveAction = InputSystem.actions.FindAction("BoxingGlove");
        gliderAction = InputSystem.actions.FindAction("Glider");
        dyingAction = InputSystem.actions.FindAction("Dying");
        
        btuAction.performed += _ => useAbility.Raise(PlayerAbilityType.BTU);
        btuAction.canceled += _ => cancelAbility.Raise(PlayerAbilityType.BTU);
        wheelAction.performed += _ => useAbility.Raise(PlayerAbilityType.WHEEL);
        wheelAction.canceled += _ => cancelAbility.Raise(PlayerAbilityType.WHEEL);
        boxingGloveAction.performed += _ => useAbility.Raise(PlayerAbilityType.BOXING_GLOVE);
        boxingGloveAction.canceled += _ => cancelAbility.Raise(PlayerAbilityType.BOXING_GLOVE);
        gliderAction.performed += _ => useAbility.Raise(PlayerAbilityType.GLIDER);
        gliderAction.canceled += _ => cancelAbility.Raise(PlayerAbilityType.GLIDER);
        dyingAction.performed += _ => useAbility.Raise(PlayerAbilityType.DYING);
        dyingAction.canceled += _ => cancelAbility.Raise(PlayerAbilityType.DYING);
    }

    void OnDisable()
    {
        btuAction.performed -= _ => useAbility.Raise(PlayerAbilityType.BTU);
        btuAction.canceled -= _ => cancelAbility.Raise(PlayerAbilityType.BTU);
        wheelAction.performed -= _ => useAbility.Raise(PlayerAbilityType.WHEEL);
        wheelAction.canceled -= _ => cancelAbility.Raise(PlayerAbilityType.WHEEL);
        boxingGloveAction.performed -= _ => useAbility.Raise(PlayerAbilityType.BOXING_GLOVE);
        boxingGloveAction.canceled -= _ => cancelAbility.Raise(PlayerAbilityType.BOXING_GLOVE);
        gliderAction.performed -= _ => useAbility.Raise(PlayerAbilityType.GLIDER);
        gliderAction.canceled -= _ => cancelAbility.Raise(PlayerAbilityType.GLIDER);
        dyingAction.performed -= _ => useAbility.Raise(PlayerAbilityType.DYING);
        dyingAction.canceled -= _ => cancelAbility.Raise(PlayerAbilityType.DYING);
    }
}

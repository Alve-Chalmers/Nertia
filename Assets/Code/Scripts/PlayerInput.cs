using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] SOEventPlayerAbilityType useAbility;
    [SerializeField] SOEventPlayerAbilityType cancelAbility;

    [Header("")]
    [SerializeField] UnlockedAbilities unlockedAbilities;
    [SerializeField] SOEventBool paused;


    private static readonly Dictionary<string, PlayerAbilityType> abilityDict = new() 
    {
        {"SelfDestruct", PlayerAbilityType.SELF_DESTRUCT},
        {"BTULeft", PlayerAbilityType.BTU_LEFT},
        {"BTURight", PlayerAbilityType.BTU_RIGHT},
        {"Wheel", PlayerAbilityType.WHEEL},
        {"BoxingGlove", PlayerAbilityType.BOXING_GLOVE},
        {"Glider", PlayerAbilityType.GLIDER},
        {"GrapplingHook", PlayerAbilityType.GRAPPLING_HOOK},
    };

    private void OnEnable()
    {
        var actions = InputSystem.actions;
        if (actions == null)
        {
            Debug.LogError("PlayerInput: No InputSystem actions found!");
            return;
        }

        foreach (var a in abilityDict)
        {
            var action = actions.FindAction(a.Key);
            if (action != null)
            {
                action.performed += OnActionPerformed;
                action.canceled += OnActionCanceled;
            }
        }

        paused.Subscribe(OnPause);
    }

    private void OnDisable()
    {
        var actions = InputSystem.actions;
        if (actions == null)
            return;

        foreach (var a in abilityDict)
        {
            var action = actions.FindAction(a.Key);
            if (action != null)
            {
                action.performed -= OnActionPerformed;
                action.canceled -= OnActionCanceled;
            }
        }

        paused.Unsubscribe(OnPause);
    }

    private void OnActionPerformed(InputAction.CallbackContext ctx)
    {
        if (Time.timeScale == 0) return;

        if (ctx.action != null && abilityDict.TryGetValue(ctx.action.name, out var abilityType))
        {
            if (!unlockedAbilities.Abilities.Contains(abilityType))
                return;
            if (abilityType == PlayerAbilityType.SELF_DESTRUCT && Keyboard.current.shiftKey.isPressed)
                return;
            useAbility.Raise(abilityType);
        }
    }

    private void OnActionCanceled(InputAction.CallbackContext ctx)
    {
        if (Time.timeScale == 0) return;

        if (ctx.action != null && abilityDict.TryGetValue(ctx.action.name, out var abilityType))
        {
            if (unlockedAbilities.Abilities.Contains(abilityType))
                cancelAbility.Raise(abilityType);
        }
    }

    void OnPause(bool paused) {
        if (!paused) {
            foreach (var a in abilityDict.Values)
            {
                cancelAbility.Raise(a);
            }
        }
    }
}
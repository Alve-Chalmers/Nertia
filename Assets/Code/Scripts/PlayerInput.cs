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


    private static readonly Dictionary<string, PlayerAbilityType> abilityDict = new() 
    {
        {"Dying", PlayerAbilityType.DYING},
        {"BTU", PlayerAbilityType.BTU},
        {"Wheel", PlayerAbilityType.WHEEL},
        {"BoxingGlove", PlayerAbilityType.BOXING_GLOVE},
        {"Glider", PlayerAbilityType.GLIDER},
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
    }

    private void OnActionPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.action != null && abilityDict.TryGetValue(ctx.action.name, out var abilityType))
        {
            if (unlockedAbilities.Abilities.Contains(abilityType))
                useAbility.Raise(abilityType);
        }
    }

    private void OnActionCanceled(InputAction.CallbackContext ctx)
    {
        if (ctx.action != null && abilityDict.TryGetValue(ctx.action.name, out var abilityType))
        {
            if (unlockedAbilities.Abilities.Contains(abilityType))
                cancelAbility.Raise(abilityType);
        }
    }
}
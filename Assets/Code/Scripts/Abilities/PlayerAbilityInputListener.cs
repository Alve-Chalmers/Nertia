using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityInputListener : MonoBehaviour
{
    [SerializeField] protected SOEventPlayerAbilityType useAbility;
    [SerializeField] protected SOEventPlayerAbilityType cancelAbility;

    [Serializable]
    public struct AbilityMapping
    {
        public PlayerAbilityType type;
        public GameObject obj;
    }

    [SerializeField] private List<AbilityMapping> abilityMappings = new();

    private readonly Dictionary<PlayerAbilityType, GameObject> abilityObjects = new();
    private readonly List<PlayerAbilityType> heldAbilityOrder = new();
    private PlayerAbilityType? activeAbilityType;

    private void OnEnable()
    {
        abilityObjects.Clear();
        foreach (var mapping in abilityMappings)
        {
            if (mapping.obj == null || abilityObjects.ContainsKey(mapping.type))
            {
                continue;
            }

            abilityObjects[mapping.type] = mapping.obj;
            mapping.obj.SetActive(false);
        }

        heldAbilityOrder.Clear();
        activeAbilityType = null;

        useAbility.Subscribe(OnUse);
        cancelAbility.Subscribe(OnCancel);
    }

    private void OnDisable()
    {
        useAbility.Unsubscribe(OnUse);
        cancelAbility.Unsubscribe(OnCancel);
        heldAbilityOrder.Clear();
    }

    private void Update()
    {
        if (activeAbilityType.HasValue
            && abilityObjects.TryGetValue(activeAbilityType.Value, out GameObject activeObject)
            && !activeObject.activeSelf)
        {
            PlayerAbilityType endedAbilityType = activeAbilityType.Value;
            SetInactive(endedAbilityType);
            TryActivateHeldFallback(endedAbilityType);
        }
    }

    private void OnUse(PlayerAbilityType requestedAbilityType)
    {
        RememberHeldAbility(requestedAbilityType);
        TryActivateAbility(requestedAbilityType);
    }

    private void OnCancel(PlayerAbilityType abilityType)
    {
        heldAbilityOrder.Remove(abilityType);

        if (activeAbilityType != abilityType)
        {
            return;
        }

        SetInactive(abilityType);
        TryActivateHeldFallback();
    }

    private bool TryActivateAbility(PlayerAbilityType requestedAbilityType)
    {
        if (!abilityObjects.TryGetValue(requestedAbilityType, out GameObject requestedObject))
        {
            return false;
        }

        if (activeAbilityType == requestedAbilityType && requestedObject.activeSelf)
        {
            return true;
        }

        if (activeAbilityType.HasValue && activeAbilityType.Value != requestedAbilityType)
        {
            SetInactive(activeAbilityType.Value);
        }

        requestedObject.SetActive(true);
        activeAbilityType = requestedAbilityType;
        return true;
    }

    private void SetInactive(PlayerAbilityType abilityType)
    {
        if (abilityObjects.TryGetValue(abilityType, out GameObject obj))
        {
            obj.SetActive(false);
        }

        if (activeAbilityType == abilityType)
        {
            activeAbilityType = null;
        }
    }

    private void RememberHeldAbility(PlayerAbilityType abilityType)
    {
        if (!abilityObjects.ContainsKey(abilityType))
        {
            return;
        }

        heldAbilityOrder.Remove(abilityType);
        heldAbilityOrder.Add(abilityType);
    }

    private void TryActivateHeldFallback(PlayerAbilityType? exclude = null)
    {
        for (int i = heldAbilityOrder.Count - 1; i >= 0; i--)
        {
            PlayerAbilityType candidate = heldAbilityOrder[i];
            if (exclude.HasValue && candidate == exclude.Value)
            {
                continue;
            }

            if (TryActivateAbility(candidate))
            {
                return;
            }
        }
    }
}
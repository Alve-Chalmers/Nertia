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

    [SerializeField] 
    private List<AbilityMapping> abilityMappings = new();

    private Dictionary<PlayerAbilityType, GameObject> abilityToObj = new();


    void OnEnable()
    {
        abilityToObj.Clear();
        foreach (var mapping in abilityMappings)
        {
            if (!abilityToObj.ContainsKey(mapping.type))
            {
                abilityToObj.Add(mapping.type, mapping.obj);
            }
        }

        useAbility.Subscribe(OnUse);
        cancelAbility.Subscribe(OnCancel);
    }

    void OnDisable()
    {
        useAbility.Unsubscribe(OnUse);
        cancelAbility.Unsubscribe(OnCancel);
    }

    void OnUse(PlayerAbilityType ability)
    {
        if (abilityToObj.ContainsKey(ability))
        {
            abilityToObj[ability].SetActive(true);
        }
        foreach (var a in abilityToObj)
        {
            if (a.Key != ability)
            {
                a.Value.SetActive(false);
            }
        }
    }

    void OnCancel(PlayerAbilityType ability)
    {
        if (abilityToObj.ContainsKey(ability))
            abilityToObj[ability].SetActive(false);
    }
}
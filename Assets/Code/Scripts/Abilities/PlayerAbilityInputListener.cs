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
 
    void Awake()
    {
        useAbility.Subscribe(OnUse);
        cancelAbility.Subscribe(OnCancel);

        foreach (var mapping in abilityMappings)
        {
            if (!abilityToObj.ContainsKey(mapping.type))
            {
                abilityToObj.Add(mapping.type, mapping.obj);
            }
        }
    }

    void OnUse(PlayerAbilityType ability)
    {
        if (abilityToObj.ContainsKey(ability))
            abilityToObj[ability].SetActive(true);
    }

    void OnCancel(PlayerAbilityType ability)
    {
        if (abilityToObj.ContainsKey(ability))
            abilityToObj[ability].SetActive(false);
    }
}
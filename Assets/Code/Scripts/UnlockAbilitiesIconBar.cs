using System.Collections.Generic;
using UnityEngine;

public class UnlockAbilitiesIconBar : MonoBehaviour
{
    [SerializeField] UnlockedAbilities unlockedAbilities;

    [SerializeField] GameObject abilityIconPrefab;

    Dictionary<PlayerAbilityType, GameObject> icons = new();

    void Start()
    {
        foreach (var a in icons.Keys)
        {
            RemoveAbility(a);
        }

        foreach (var a in unlockedAbilities.Abilities)
        {
            AddAbility(a);
        }
    }

    void Update()
    {
        foreach (var a in unlockedAbilities.Abilities)
        {
            AddAbility(a);
        }

        foreach (var a in icons.Keys)
        {
            if (!unlockedAbilities.Abilities.Contains(a))
                RemoveAbility(a);
        }
    }

    void AddAbility(PlayerAbilityType ability)
    {
        if (icons.ContainsKey(ability))
            return;

        GameObject iconGameObject = Instantiate(abilityIconPrefab);
        iconGameObject.GetComponent<AbilityIcon>().SetData(ability);
        iconGameObject.transform.SetParent(transform, false);
        icons.Add(ability, iconGameObject);
    }

    void RemoveAbility(PlayerAbilityType ability)
    {
        if (!icons.ContainsKey(ability))
            return;
        
        Destroy(icons[ability]);
        icons.Remove(ability);
    }
}

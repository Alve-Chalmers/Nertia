using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnlockedAbilities", menuName = "ScriptableObjects/Data/UnlockedAbilities")]
public class UnlockedAbilities : ScriptableObject
{
    [SerializeField] ListOfAbilities defaultAbilities;

    private HashSet<PlayerAbilityType> _unlockedAbilities = null;

    public HashSet<PlayerAbilityType> Abilities
    {
        get
        {
            if (_unlockedAbilities == null)
            {
                _unlockedAbilities = new HashSet<PlayerAbilityType>(defaultAbilities.abilities);
            }
            return _unlockedAbilities;
        }
        set
        {
            _unlockedAbilities = value;
        }
    }

    public void UnlockAbility(PlayerAbilityType a)
    {
        _unlockedAbilities.Add(a);
    }
}
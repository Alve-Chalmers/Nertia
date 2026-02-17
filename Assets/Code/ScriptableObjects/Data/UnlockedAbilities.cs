using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnlockedAbilities", menuName = "ScriptableObjects/Data/UnlockedAbilities")]
public class UnlockedAbilities : ScriptableObject
{
    [SerializeField] StartingAbilities startingAbilities;

    private HashSet<PlayerAbilityType> _unlockedAbilities = null;

    public HashSet<PlayerAbilityType> Abilities
    {
        get
        {
            if (_unlockedAbilities == null)
            {
                _unlockedAbilities = new HashSet<PlayerAbilityType>(startingAbilities.startingAbilities);
            }
            return _unlockedAbilities;
        }
    }

    public void UnlockAbility(PlayerAbilityType a)
    {
        _unlockedAbilities.Add(a);
    }
}
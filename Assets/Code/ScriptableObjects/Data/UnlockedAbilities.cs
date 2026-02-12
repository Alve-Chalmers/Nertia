using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnlockedAbilities", menuName = "ScriptableObjects/Data/UnlockedAbilities")]
public class UnlockedAbilities : ScriptableObject
{
    // TODO: should not be hard coded like this.
    public List<PlayerAbilityType> Unlocked => new List<PlayerAbilityType>{
        PlayerAbilityType.BASE,
        PlayerAbilityType.DYING,
        PlayerAbilityType.BTU,
        PlayerAbilityType.WHEEL,
        PlayerAbilityType.BOXING_GLOVE,
        PlayerAbilityType.GLIDER,
    };

    public void UnlockAbility(PlayerAbilityType a)
    {
        Debug.Log("Unlocked " + a.ToString());
    }
}
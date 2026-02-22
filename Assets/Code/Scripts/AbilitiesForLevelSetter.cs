using UnityEngine;

public class AbilitiesForLevelSetter : MonoBehaviour
{
    [SerializeField] ListOfAbilities forThisLevel;
    [SerializeField] UnlockedAbilities unlockedAbilities;

    void Awake()
    {
        unlockedAbilities.Abilities = new(forThisLevel.abilities);
    }
}

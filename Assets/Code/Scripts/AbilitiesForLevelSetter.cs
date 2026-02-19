using UnityEngine;

public class AbilitiesForLevelSetter : MonoBehaviour
{
    [SerializeField] StartingAbilities forThisLevel;
    [SerializeField] UnlockedAbilities unlockedAbilities;

    void Awake()
    {
        unlockedAbilities.Abilities = new(forThisLevel.startingAbilities);
    }
}

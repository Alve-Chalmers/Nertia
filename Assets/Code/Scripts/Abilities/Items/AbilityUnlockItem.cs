using UnityEngine;

public class AbilityUnlockItem : MonoBehaviour
{
    [SerializeField] PlayerAbilityType abilityToUnlock;
    [SerializeField] UnlockedAbilities unlockedAbilities;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
            return;
        unlockedAbilities.UnlockAbility(abilityToUnlock);
        Destroy(gameObject); // TODO: animation?
    }
}

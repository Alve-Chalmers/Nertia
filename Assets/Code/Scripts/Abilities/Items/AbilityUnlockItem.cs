using UnityEngine;

public class AbilityUnlockItem : MonoBehaviour
{
    [SerializeField] PlayerAbilityType abilityToUnlock;
    [SerializeField] UnlockedAbilities unlockedAbilities;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            unlockedAbilities.UnlockAbility(abilityToUnlock);
            Destroy(gameObject); // TODO: animation?
        }
    }
}

using UnityEngine;

public class SelfDestruct : PlayerAbilityScript
{
    [SerializeField] SOEvent playerDeathEvent;
    protected override PlayerAbilityType Ability => PlayerAbilityType.SELF_DESTRUCT;

    protected override void OnEnable()
    {
        base.OnEnable();
        Die();
    }

    void Die()
    {
        playerDeathEvent.Raise();
    }
}

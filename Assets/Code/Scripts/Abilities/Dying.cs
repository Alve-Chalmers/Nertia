using UnityEngine;

public class Dying : PlayerAbilityScript
{
    [SerializeField] SOEvent playerDeathEvent;
    protected override PlayerAbilityType Ability => PlayerAbilityType.DYING;

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

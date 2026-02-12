using UnityEngine;

public class Dying : PlayerAbilityScript
{
    public GameObject player;
    public Transform respawnPoint;
    protected override PlayerAbilityType State => PlayerAbilityType.DYING;

    protected override void OnEnable()
    {
        base.OnEnable();
        Die();
    }

    void Die()
    {
        player.transform.position = respawnPoint.position;
    }
}

using UnityEngine;

public class Dying : PlayerForm
{
    public GameObject player;
    public Transform respawnPoint;
    protected override PlayerState State => PlayerState.DYING;

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

using UnityEngine;

public class Wheel : PlayerAbilityScript
{
    [SerializeField]
    Rigidbody2D prb;

    protected override PlayerAbilityType Ability => PlayerAbilityType.WHEEL;

    protected override void OnEnable()
    {
        base.OnEnable();
        prb.freezeRotation = false;
        prb.GetComponent<PlayerAligner>().align = false;
    }

    void OnDisable()
    {
        prb.freezeRotation = true;
        prb.GetComponent<PlayerAligner>().align = true;
    }
}

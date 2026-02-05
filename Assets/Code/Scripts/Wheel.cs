using UnityEngine;

public class Wheel : PlayerForm
{
    [SerializeField]
    Rigidbody2D prb;

    protected override PlayerState State => PlayerState.WHEEL;

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

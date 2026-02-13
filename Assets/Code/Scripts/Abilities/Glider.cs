using System;
using UnityEngine;

public class Glider : PlayerAbilityScript
{
    protected override PlayerAbilityType State => PlayerAbilityType.GLIDER;
    [SerializeField] float glideUpForce = 20f;
    [SerializeField] float glideDirectionForce = 10f;
    [SerializeField] Rigidbody2D prb;


    protected override void OnEnable()
    {
        base.OnEnable();
        if (!playerInfo.IsGrounded) {
            prb.AddForce(glideDirectionForce * playerInfo.DirectionX * Vector2.right, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (prb.linearVelocityY < 0)
        {
            float counterForce = Mathf.Abs(prb.linearVelocityY) * glideUpForce;
            prb.AddForce(counterForce * Vector2.up);
        }
    }
}

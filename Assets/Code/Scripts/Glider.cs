using System;
using Unity.VisualScripting;
using UnityEngine;

public class Glider : PlayerForm
{
    protected override PlayerState State => PlayerState.GLIDER;
    [SerializeField] float glideUpForce = 20f;
    [SerializeField] float glideDirectionForce = 10f;
    [SerializeField] Rigidbody2D prb;


    protected override void OnEnable()
    {
        base.OnEnable();
        prb.AddForce(glideDirectionForce * playerInfo.directionX * Vector2.right, ForceMode2D.Impulse);
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

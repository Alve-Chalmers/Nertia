using System;
using UnityEngine;

public class Glider : PlayerAbilityScript
{
    protected override PlayerAbilityType Ability => PlayerAbilityType.GLIDER;
    [SerializeField] float glideUpForce = 20f;
    [SerializeField] float glideDirectionForce = 10f;
    [SerializeField] float damping = 5f;
    [SerializeField] Rigidbody2D prb;
    [SerializeField] PlayerAligner playerAligner;

    float originalDampening;

    protected override void OnEnable()
    {
        base.OnEnable();
       /*  if (!playerInfo.IsGrounded && Mathf.Abs(playerInfo.Velocity.x) < 0.1f) {
            prb.AddForce(glideDirectionForce * playerInfo.DirectionX * Vector2.right, ForceMode2D.Impulse);
        } */
        playerAligner.alignToGroundNormal = false;
        originalDampening = prb.linearDamping;
        prb.linearDamping = damping;
    }

    void OnDisable()
    {
        playerAligner.alignToGroundNormal = true;
        prb.linearDamping = originalDampening;
    }

    void Update()
    {
        Vector3 rot = transform.eulerAngles;
        rot.z = 0;
        transform.eulerAngles = rot;
    }

    void FixedUpdate()
    {
        if (prb.linearVelocityY < 0)
        {
            float counterForce = glideUpForce;
            prb.AddForce(counterForce * Vector2.up);
            prb.AddForce(-prb.linearVelocityY * glideDirectionForce * Vector2.right * playerInfo.DirectionX);
        }
        Debug.Log(prb.linearVelocity);
        if (!playerInfo.IsGrounded)
            playerAligner.targetAngle = -Mathf.Sign(prb.linearVelocityX) * Mathf.Lerp(0, 30, Mathf.Abs(prb.linearVelocityX) / 30f);
        
        playerAligner.alignToGroundNormal = playerInfo.IsGrounded;
    }
}

using System.Collections;
using UnityEngine;

public class BTU : PlayerAbilityScript
{
    [SerializeField] Rigidbody2D prb;
    [SerializeField] float force;
    [SerializeField] float targetSpeed = 5f;
    [SerializeField] float maxSlopeAngle = 45f;
    [SerializeField] float startingTimeUntilMaxForce = 1f;
    [SerializeField] LayerMask ground;

    protected override PlayerAbilityType Ability => PlayerAbilityType.BTU;

    int dir = -1;

    float timeRunning = 0;

    protected override void OnEnable()
    {
        base.OnEnable();
        dir = -dir;
        timeRunning = 0;
    }

    void Update()
    {
        timeRunning += Time.deltaTime;
        transform.localScale = new Vector3(playerInfo.DirectionX, transform.localScale.y, transform.localScale.z);
    }

    void FixedUpdate()
    {
        if (!playerInfo.IsGrounded)
        {
            return;
        }

        Vector2 groundDir = Vector3.Cross(playerInfo.GroundNormal, Vector3.forward);
        float slopeAngle = Vector2.Angle(Vector2.up, playerInfo.GroundNormal);

        bool goingUpHill = Mathf.Sign(playerInfo.GroundNormal.x) != dir;

        if (prb.linearVelocity.magnitude >= targetSpeed)
        {
            prb.AddForce(-prb.linearVelocity * 3f);
            return;
        }

        Debug.DrawRay(transform.position, prb.linearVelocity, Color.cyan);

        float multiplier = Mathf.Pow(targetSpeed - prb.linearVelocity.magnitude, 2);

        // Gradually reduce force as slope approaches maxSlopeAngle
        float slopeReductionStartAngle = maxSlopeAngle - 20f; // Start reducing force at this angle
        if (goingUpHill && slopeAngle > slopeReductionStartAngle)
        {
            float t = Mathf.InverseLerp(slopeReductionStartAngle, maxSlopeAngle, slopeAngle);
            float slopeness = Mathf.Lerp(1, 0, t);
            multiplier *= slopeness;
        }

        prb.AddForce(Mathf.Min(timeRunning, startingTimeUntilMaxForce) * groundDir * force * dir * multiplier);
    }
}

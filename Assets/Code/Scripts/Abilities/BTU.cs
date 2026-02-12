using UnityEngine;

public class BTU : PlayerAbilityScript
{
    [SerializeField] Rigidbody2D prb;
    [SerializeField] float force;
    [SerializeField] float targetSpeed = 5f;
    [SerializeField] float maxSlopeAngle = 45f;
    float correctingStartDirForce = 10f;
    float minLinXVelForCorrectingForce = 1f;
    [SerializeField] SpriteRenderer DebugDirectionSprite;
    int dir = -1;

    protected override PlayerAbilityType State => PlayerAbilityType.BTU;

    protected override void OnEnable()
    {
        base.OnEnable();
        dir = -dir;

        bool goingUpHill = Mathf.Sign(playerInfo.GroundNormal.x) != dir;
        bool switchingDirection = Mathf.Sign(prb.linearVelocityX) != dir;

        Vector2 groundDir = Vector3.Cross(playerInfo.GroundNormal, Vector3.forward);
        float groundVelMag = Vector2.Dot(prb.linearVelocity, groundDir * dir);

        if (Mathf.Abs(groundVelMag) > minLinXVelForCorrectingForce &&
            (switchingDirection || goingUpHill)
            )
        {
            prb.AddForce(groundDir * dir * correctingStartDirForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        DebugDirectionSprite.flipX = dir == -1;

        if (!playerInfo.IsGrounded)
        {
            return;
        }

        Vector2 groundDir = Vector3.Cross(playerInfo.GroundNormal, Vector3.forward);
        float slopeAngle = Vector2.Angle(Vector2.up, playerInfo.GroundNormal);

        bool goingUpHill = Mathf.Sign(playerInfo.GroundNormal.x) != dir;
        // if (slopeAngle > maxSlopeAngle && goingUpHill)
        // {
        //     return;
        // }

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

        prb.AddForce(groundDir * force * dir * multiplier);
    }
}

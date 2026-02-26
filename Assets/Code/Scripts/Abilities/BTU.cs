using System;
using System.Collections;
using UnityEngine;

public abstract class BTU : PlayerAbilityScript
{
    [SerializeField] Rigidbody2D prb;
    [SerializeField] PlayerDirSetter pds;
    [SerializeField] float force = 10f;
    [SerializeField] float targetSpeed = 5f;
    [SerializeField] float maxSlopeAngle = 50f;
    [SerializeField] float startingTimeUntilMaxForce = 1f;
    [SerializeField] LayerMask ground;


    protected abstract int Dir {get;}

    float timeRunning = 0;

    protected override void OnEnable()
    {
        base.OnEnable();
        pds.setPlayerDirFromVel = false;
        playerInfo.DirectionX = Dir;

        timeRunning = 0;
    }

    void OnDisable()
    {
        pds.setPlayerDirFromVel = true;
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

        bool goingUpHill = Mathf.Sign(playerInfo.GroundNormal.x) != Dir;

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

        prb.AddForce(Mathf.Min(timeRunning, startingTimeUntilMaxForce) * groundDir * force * Dir * multiplier);
    }
}

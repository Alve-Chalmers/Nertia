using System;
using UnityEngine;

public class PlayerAligner : MonoBehaviour
{

    private Rigidbody2D rb;

    public bool align = true;

    [SerializeField]
    float castDistance = 2f;

    [SerializeField]
    float rotationSpeed = 10f;

    [SerializeField]
    float maxAlignmentRot;

    [SerializeField] LayerMask maskToHit;

    [SerializeField] PlayerInfo playerInfo;

    float targetAngleFromGroundNormal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        SetPlayerGroundNormal();

        if (!align)
        {
            return;
        }

        if (rb.linearVelocityX > 1f)
        {
            playerInfo.DirectionX = 1;
        }
        else if (rb.linearVelocityX < -1f)
        {
            playerInfo.DirectionX = -1;
        }

        float angleDiff = GetAlignmentAngleDiff(rb.rotation, Time.fixedDeltaTime);

        rb.MoveRotation(rb.rotation + angleDiff);
    }

    void SetPlayerGroundNormal()
    {
        Vector2 normalsSum = Vector2.zero;
        int hits = 0;
        const int rayAmount = 30;
        for (int i = 0; i < rayAmount; i++)
        {
            float angle = Mathf.Lerp(-180, 0, (float)i / rayAmount) * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            RaycastHit2D hit = Physics2D.Raycast(playerInfo.Position, dir, castDistance, maskToHit);
            if (hit.collider == null)
            {
                continue;
            }
            hits += 1;
            Debug.DrawLine(playerInfo.Position, playerInfo.Position + dir * castDistance, Color.black);
            normalsSum += hit.normal;
        }
        playerInfo.GroundNormal = Vector2.zero;

        targetAngleFromGroundNormal = 0;
        if (hits != 0)
        {
            Vector2 averageNormal = normalsSum / hits;
            Debug.DrawLine(playerInfo.Position, playerInfo.Position + averageNormal * castDistance, Color.yellow);
            targetAngleFromGroundNormal = Vector2.SignedAngle(Vector2.up, averageNormal);
            playerInfo.GroundNormal = averageNormal.normalized;
        }
    }

    float GetAlignmentAngleDiff(float currentRotation, float deltaTime)
    {
        float targetAngle = Mathf.Clamp(targetAngleFromGroundNormal, -maxAlignmentRot, maxAlignmentRot);

        float smoothedAngle = Mathf.MoveTowardsAngle(currentRotation, targetAngle, rotationSpeed * deltaTime);

        float angleDiff = Mathf.DeltaAngle(currentRotation, smoothedAngle);
        
        return angleDiff;
    }
}




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
    float maxRotationPerSecond = 90f;

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

        float angleDiff = GetAlignmentAngleDiff(rb.rotation);
        
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
            playerInfo.GroundNormal = averageNormal;
        }
    }

    public float GetAlignmentAngleDiff(float currentRotation)
    {
        float targetAngle = Mathf.Clamp(targetAngleFromGroundNormal, -maxAlignmentRot, maxAlignmentRot);
        
        // Smoothly interpolate to target angle
        float smoothedAngle = Mathf.LerpAngle(currentRotation, targetAngle, rotationSpeed * Time.fixedDeltaTime);
        
        // Clamp max rotation change per frame to prevent jumps
        float maxChange = maxRotationPerSecond * Time.fixedDeltaTime;
        float angleDiff = Mathf.DeltaAngle(currentRotation, smoothedAngle);
        angleDiff = Mathf.Clamp(angleDiff, -maxChange, maxChange);

        return angleDiff;
    }
}




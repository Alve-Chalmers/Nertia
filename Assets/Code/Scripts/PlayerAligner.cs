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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!align)
        {
            return;
        }

        if (rb.linearVelocityX > 1f)
        {
            playerInfo.DirectionX = 1;
        }
        else if (rb.linearVelocityX < 1f)
        {
            playerInfo.DirectionX = -1;
        }
        

        Vector2 normalsSum = Vector2.zero;
        float targetAngle = 0;
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
        if (hits != 0)
        {
            Vector2 averageNormal = normalsSum / hits;
            Debug.DrawLine(playerInfo.Position, playerInfo.Position + averageNormal * castDistance, Color.yellow);
            targetAngle = Vector2.SignedAngle(Vector2.up, averageNormal);
            playerInfo.GroundNormal = averageNormal;
        }

        targetAngle = Mathf.Clamp(targetAngle, -maxAlignmentRot, maxAlignmentRot);
        
        // Smoothly interpolate to target angle
        float currentAngle = rb.rotation;
        float smoothedAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.fixedDeltaTime);
        
        // Clamp max rotation change per frame to prevent jumps
        float maxChange = maxRotationPerSecond * Time.fixedDeltaTime;
        float angleDiff = Mathf.DeltaAngle(currentAngle, smoothedAngle);
        angleDiff = Mathf.Clamp(angleDiff, -maxChange, maxChange);
        
        rb.MoveRotation(currentAngle + angleDiff);
    }
}




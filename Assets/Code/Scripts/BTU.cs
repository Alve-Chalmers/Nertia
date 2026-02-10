using UnityEngine;

public class BTU : PlayerForm
{
    [SerializeField] Rigidbody2D prb;
    [SerializeField] float force;
    [SerializeField] float maxSpeed;
    [SerializeField] float maxSlopeAngle = 45f;
    [SerializeField] float slopeForceMultiplier = 2f;
    [SerializeField] float raySpacing = 0.3f;
    [SerializeField] float rayLength = 1f;
    int dir = -1;

    protected override PlayerState State => PlayerState.BTU;

    protected override void OnEnable()
    {
        base.OnEnable();
        dir = -dir;
    }

    void FixedUpdate()
    {
        int mask = LayerMask.GetMask("Ground");
        Vector2 down = -transform.up;
        Vector2 right = transform.right;
        
        // Cast multiple rays for better terrain detection
        Vector2 leftOrigin = (Vector2)transform.position - right * raySpacing;
        Vector2 centerOrigin = transform.position;
        Vector2 rightOrigin = (Vector2)transform.position + right * raySpacing;
        
        RaycastHit2D hitLeft = Physics2D.Raycast(leftOrigin, down, rayLength, mask);
        RaycastHit2D hitCenter = Physics2D.Raycast(centerOrigin, down, rayLength, mask);
        RaycastHit2D hitRight = Physics2D.Raycast(rightOrigin, down, rayLength, mask);
        
        Debug.DrawRay(leftOrigin, down * rayLength, Color.red);
        Debug.DrawRay(centerOrigin, down * rayLength, Color.green);
        Debug.DrawRay(rightOrigin, down * rayLength, Color.blue);
        
        // Average normals from all hits
        Vector2 avgNormal = Vector2.zero;
        int hitCount = 0;
        
        if (hitLeft.collider != null) { avgNormal += hitLeft.normal; hitCount++; }
        if (hitCenter.collider != null) { avgNormal += hitCenter.normal; hitCount++; }
        if (hitRight.collider != null) { avgNormal += hitRight.normal; hitCount++; }
        
        if (hitCount == 0)
        {
            return;
        }
        
        avgNormal /= hitCount;

        // Calculate slope angle from averaged ground normal
        float slopeAngle = Vector2.Angle(Vector2.up, avgNormal);
        
        // Don't move if slope is too steep
        if (slopeAngle > maxSlopeAngle)
        {
            return;
        }

        if (prb.linearVelocity.magnitude >= maxSpeed)
        {
            prb.AddForce(-prb.linearVelocity);
            return;
        }

        Vector2 groundDir = Vector3.Cross(avgNormal, Vector3.forward);
        Debug.DrawRay(transform.position, groundDir * 2f, Color.yellow);
        Debug.DrawRay(transform.position, prb.linearVelocity, Color.cyan);

        // Increase force on steeper slopes to counteract gravity
        float slopeMultiplier = 1f + (slopeAngle / maxSlopeAngle) * (slopeForceMultiplier - 1f);
        
        prb.AddForce(groundDir * force * dir * slopeMultiplier);
    }
}

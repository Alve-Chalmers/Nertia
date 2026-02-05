using UnityEngine;

public class PlayerAligner : MonoBehaviour
{

    private Rigidbody2D rb;

    public bool align = true;

    [SerializeField]
    float raySpacing = 0.3f;

    [SerializeField]
    float rayLength = 2f;

    [SerializeField]
    float rotationSpeed = 10f;

    [SerializeField]
    float maxRotationPerSecond = 90f;

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
        
        int mask = LayerMask.GetMask("Ground");
        Vector2 down = -transform.up;
        Vector2 right = transform.right;
        
        // Cast rays from left, center, and right
        Vector2 leftOrigin = (Vector2)transform.position - right * raySpacing;
        Vector2 centerOrigin = transform.position;
        Vector2 rightOrigin = (Vector2)transform.position + right * raySpacing;
        
        RaycastHit2D hitLeft = Physics2D.Raycast(leftOrigin, down, rayLength, mask);
        RaycastHit2D hitCenter = Physics2D.Raycast(centerOrigin, down, rayLength, mask);
        RaycastHit2D hitRight = Physics2D.Raycast(rightOrigin, down, rayLength, mask);
        
        Debug.DrawRay(leftOrigin, down * rayLength, Color.red);
        Debug.DrawRay(centerOrigin, down * rayLength, Color.green);
        Debug.DrawRay(rightOrigin, down * rayLength, Color.blue);
        
        // Average the normals from all hits
        Vector2 avgNormal = Vector2.zero;
        int hitCount = 0;
        
        if (hitLeft.collider != null) { avgNormal += hitLeft.normal; hitCount++; }
        if (hitCenter.collider != null) { avgNormal += hitCenter.normal; hitCount++; }
        if (hitRight.collider != null) { avgNormal += hitRight.normal; hitCount++; }

        float targetAngle;
        if (hitCount == 0)
        {
            targetAngle = 0f;
        }
        else
        {
            avgNormal /= hitCount;
            targetAngle = Vector2.SignedAngle(Vector2.up, avgNormal);
        }
        
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

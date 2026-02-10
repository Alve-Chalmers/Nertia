using UnityEngine;

public class PlayerAligner : MonoBehaviour
{

    private Rigidbody2D rb;

    public bool align = true;

    [SerializeField]
    float castRadius = 0.5f;

    [SerializeField]
    float castDistance = 2f;

    [SerializeField]
    float rotationSpeed = 10f;

    [SerializeField]
    float maxRotationPerSecond = 90f;

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

        if (rb.linearVelocityX > 0)
        {
            playerInfo.directionX = 1;
        }
        else if (rb.linearVelocityX < 0)
        {
            playerInfo.directionX = -1;
        }
        
        Vector2 down = -transform.up;
        Vector2 origin = transform.position;
        
        RaycastHit2D hit = Physics2D.CircleCast(origin, castRadius, down, castDistance, maskToHit);
        
        // Debug visualization
        Debug.DrawRay(origin, down * castDistance, Color.green);

        float targetAngle;
        if (hit.collider == null)
        {
            targetAngle = 0f;
        }
        else
        {
            targetAngle = Vector2.SignedAngle(Vector2.up, hit.normal);
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

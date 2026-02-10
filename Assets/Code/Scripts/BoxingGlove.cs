using Unity.VisualScripting;
using UnityEngine;

public class BoxingGlove : PlayerForm
{
    protected override PlayerState State => PlayerState.BOXING_GLOVE;
    [SerializeField] float checkingRange = 1;
    [SerializeField] float force = 15;
    [SerializeField] LayerMask maskToHit;
    [SerializeField] bool drawDebugRays;
    [SerializeField] Rigidbody2D prb;

    Vector2 dir;

    protected override void OnEnable()
    {
        base.OnEnable();
        Vector2? closestPoint = FindClosestContact();
        if (closestPoint == null) {
            gameObject.SetActive(false);
            return;
        }
        dir = (closestPoint.Value - playerInfo.Position).normalized;
        transform.position = playerInfo.Position + dir;
        transform.eulerAngles = Vector3.zero;
        prb.AddForce(-dir * force, ForceMode2D.Impulse);
    }

    void Update()
    {
        transform.position = playerInfo.Position + dir;
        transform.eulerAngles = Vector3.zero;
        DrawDebugRays();
    }

    Vector2 DirectionToClosestWall()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(playerInfo.Position, Vector2.up, checkingRange, maskToHit);
        RaycastHit2D hitDown = Physics2D.Raycast(playerInfo.Position, Vector2.down, checkingRange, maskToHit);
        RaycastHit2D hitLeft = Physics2D.Raycast(playerInfo.Position, Vector2.left, checkingRange, maskToHit);
        RaycastHit2D hitRight = Physics2D.Raycast(playerInfo.Position, Vector2.right, checkingRange, maskToHit);
        
        float closestDistance = float.MaxValue;
        Vector2 closestDirection = Vector2.zero;
        
        if (hitUp.collider != null && hitUp.distance < closestDistance)
        {
            closestDistance = hitUp.distance;
            closestDirection = Vector2.up;
        }
        if (hitDown.collider != null && hitDown.distance < closestDistance)
        {
            closestDistance = hitDown.distance;
            closestDirection = Vector2.down;
        }
        if (hitLeft.collider != null && hitLeft.distance < closestDistance)
        {
            closestDistance = hitLeft.distance;
            closestDirection = Vector2.left;
        }
        if (hitRight.collider != null && hitRight.distance < closestDistance)
        {
            closestDistance = hitRight.distance;
            closestDirection = Vector2.right;
        }
        
        return closestDirection;
    }

    Vector2? FindClosestContact()
    {
        // 1. Get all colliders within the maximum expansion range
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerInfo.Position, checkingRange, maskToHit);

        Collider2D closestCollider = null;
        Vector2? closestPoint = null;
        float shortestDistance = Mathf.Infinity;

        foreach (var hitCollider in hits)
        {
            // 2. Find the closest point on this specific collider surface to our center
            Vector2 pointOnSurface = hitCollider.ClosestPoint(playerInfo.Position);
            
            float dist = Vector2.Distance(playerInfo.Position, pointOnSurface);

            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                closestCollider = hitCollider;
                closestPoint = pointOnSurface;
            }
        }

        return closestPoint;
    }

    void DrawDebugRays()
    {
        if (!drawDebugRays || playerInfo == null) return;

        Vector2 origin = playerInfo.Position;
        Debug.DrawRay(origin, Vector2.up * checkingRange, Color.red);
        Debug.DrawRay(origin, Vector2.down * checkingRange, Color.green);
        Debug.DrawRay(origin, Vector2.left * checkingRange, Color.blue);
        Debug.DrawRay(origin, Vector2.right * checkingRange, Color.yellow);
    }

    void OnDrawGizmos()
    {
        if (!drawDebugRays) return;

        Vector2 origin = playerInfo != null ? playerInfo.Position : (Vector2)transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, Vector2.up * checkingRange);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(origin, Vector2.down * checkingRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(origin, Vector2.left * checkingRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, Vector2.right * checkingRange);
    }
}

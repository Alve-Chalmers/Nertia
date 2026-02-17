using Unity.VisualScripting;
using UnityEngine;

public class BoxingGlove : PlayerAbilityScript
{
    protected override PlayerAbilityType Ability => PlayerAbilityType.BOXING_GLOVE;
    [SerializeField] float checkingRange = 1;
    [SerializeField] float force = 15;
    [SerializeField, Tooltip("multiplies force by length(projection of direction to ground on down vector)^slopeForceReductionPower")] float slopeForceReductionPower = 1;
    [SerializeField] LayerMask maskToHit;
    [SerializeField] bool drawDebugRays;
    [SerializeField] Rigidbody2D prb;


    protected override void OnEnable()
    {
        base.OnEnable();
        Vector2? closestPoint = FindClosestContact();
        if (closestPoint == null) {
            gameObject.SetActive(false);
            return;
        }
        Vector2 dir = (closestPoint.Value - playerInfo.Position).normalized;
        transform.position = playerInfo.Position + dir;
        transform.eulerAngles = Vector3.zero;
        prb.AddForce(-dir * force, ForceMode2D.Impulse);
    }

    void Update()
    {
        transform.eulerAngles = Vector3.zero;
        DrawDebugRays();
    }

    Vector2? FindClosestContact()
    {
        // 1. Get all colliders within the maximum expansion range
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerInfo.Position, checkingRange, maskToHit);

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

using UnityEngine;

public struct GrappleTarget
{
    public bool HitHook;
    public Vector2? HitPoint;
    public Collider2D HookCollider;
}

public class GrappleDetector : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] float castDistance = 5;
    [SerializeField] float angleToTestAt = 45;
    [SerializeField] LayerMask obstructionMask;
    [SerializeField] LayerMask hookMask;

    GrappleHighlight currentHighlight;

    public GrappleTarget CurrentTarget { get; private set; }

    public Vector2 GetHookVec()
    {
        Vector2 dir = new Vector2(
            Mathf.Cos(angleToTestAt * Mathf.Deg2Rad),
            Mathf.Sin(angleToTestAt * Mathf.Deg2Rad));
        dir.x *= playerInfo.DirectionX;
        dir *= castDistance;
        return dir;
    }

    public GrappleTarget Raycast()
    {
        Vector2 dir = GetHookVec().normalized;
        LayerMask combined = hookMask.value | obstructionMask.value;
        RaycastHit2D hit = Physics2D.Raycast(playerInfo.Position, dir, castDistance, combined);

        if (hit.collider == null)
            return default;

        bool isHook = ((1 << hit.collider.gameObject.layer) & hookMask.value) != 0;
        return new GrappleTarget
        {
            HitHook = isHook,
            HitPoint = isHook ? (Vector2?)hit.collider.transform.position : hit.point,
            HookCollider = isHook ? hit.collider : null
        };
    }

    void Update()
    {
        GrappleTarget target = Raycast();
        CurrentTarget = target;

        GrappleHighlight newHighlight = null;
        if (target.HitHook && target.HookCollider != null)
            newHighlight = target.HookCollider.GetComponentInParent<GrappleHighlight>();

        if (newHighlight != currentHighlight)
        {
            if (currentHighlight != null)
                currentHighlight.SetHighlight(false);
            if (newHighlight != null)
                newHighlight.SetHighlight(true);
            currentHighlight = newHighlight;
        }
    }

    void OnDisable()
    {
        if (currentHighlight != null)
        {
            currentHighlight.SetHighlight(false);
            currentHighlight = null;
        }
    }

    void OnDrawGizmos()
    {
        if (playerInfo == null) return;
        Vector2 dir = new Vector2(
            Mathf.Cos(angleToTestAt * Mathf.Deg2Rad),
            Mathf.Sin(angleToTestAt * Mathf.Deg2Rad));
        dir.x *= playerInfo.DirectionX;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerInfo.Position, playerInfo.Position + dir.normalized * castDistance);
    }
}

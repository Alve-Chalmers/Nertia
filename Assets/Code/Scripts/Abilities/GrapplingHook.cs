using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplingHook : PlayerAbilityScript
{
    protected override PlayerAbilityType Ability => PlayerAbilityType.GRAPPLING_HOOK;
    [SerializeField] float castDistance = 5;
    [SerializeField] float angleToTestAt = 45;
    [SerializeField] LayerMask obstructionMask;
    [SerializeField] LayerMask hookMask;

    [SerializeField] Rigidbody2D prb;
    [SerializeField] HingeJoint2D playerHingeJoint;

    [SerializeField] LineRenderer lineRenderer;

    bool hitHook;
    Vector2? hitPoint;


    protected override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("grp");

        RaycastForHook();
        if (hitPoint == null)
            hitPoint = (Vector2)transform.position + GetHookVec();

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hitPoint.Value);

        if (!hitHook)
            return;

        prb.rotation = 0;
        prb.freezeRotation = false;
        playerHingeJoint.connectedAnchor = hitPoint.Value;
        playerHingeJoint.anchor = transform.InverseTransformPoint(hitPoint.Value);
        playerHingeJoint.enabled = true;
    }

    void OnDisable() {
        prb.freezeRotation = true;
        playerHingeJoint.enabled = false;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        if (hitPoint != null)
            lineRenderer.SetPosition(1, hitPoint.Value);
        else
            lineRenderer.SetPosition(1, (Vector2)transform.position + GetHookVec());
    }

    Vector2 GetHookVec() {
        Vector2 dir = new Vector2(Mathf.Cos(angleToTestAt*Mathf.Deg2Rad), Mathf.Sin(angleToTestAt*Mathf.Deg2Rad));
        dir.x *= playerInfo.DirectionX;
        dir *= castDistance;
        return dir;
    }

    void RaycastForHook()
    {
        Vector2 dir = GetHookVec().normalized;

        LayerMask hooksAndObstructionMask = hookMask.value | obstructionMask.value;
        RaycastHit2D hit = Physics2D.Raycast(playerInfo.Position, dir, castDistance, hooksAndObstructionMask);

        if (hit.collider == null)
        {
            hitHook = false;
            hitPoint = null;
            return;
        }


        hitHook = ((1 << hit.collider.gameObject.layer) & hookMask.value) != 0;
        hitPoint = hit.point;
    }

    void OnDrawGizmos()
    {
        Vector2 dir = new Vector2(Mathf.Cos(angleToTestAt*Mathf.Deg2Rad), Mathf.Sin(angleToTestAt*Mathf.Deg2Rad));
        dir.x *= playerInfo.DirectionX;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerInfo.Position, playerInfo.Position + dir.normalized * castDistance);
    }
}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplingHook : PlayerAbilityScript
{
    protected override PlayerAbilityType Ability => PlayerAbilityType.GRAPPLING_HOOK;
    [SerializeField] float castDistance = 5;
    [SerializeField] float angleToTestAt = 45;
    [SerializeField] LayerMask hooksAndObstructionMask;
    [SerializeField] LayerMask hookMask;

    [SerializeField] Rigidbody2D prb;


    protected override void OnEnable()
    {
        base.OnEnable();
        
        
    }

    void Update()
    {
        
    }

    Vector2? FindHook()
    {
        Vector2 dir = new Vector2(Mathf.Cos(angleToTestAt), Mathf.Sin(angleToTestAt));
        dir.x *= playerInfo.DirectionX;

        RaycastHit2D hit = Physics2D.Raycast(playerInfo.Position, dir, castDistance, hooksAndObstructionMask);

        if (hit.collider == null)
        {
            return null;
        }

        bool hitHook = ((1 << hit.collider.gameObject.layer) & hookMask.value) != 0;
        if (!hitHook)
        {
            return null;
        }

        return null;
    }

    void OnDrawGizmos()
    {
        
    }
}

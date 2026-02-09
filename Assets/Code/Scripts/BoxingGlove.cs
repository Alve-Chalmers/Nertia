using Unity.VisualScripting;
using UnityEngine;

public class BoxingGlove : PlayerForm
{
    protected override PlayerState State => PlayerState.BOXING_GLOVE;
    [SerializeField] float checkingRange;
    [SerializeField] LayerMask maskToHit;

    void OnEnable()
    {
        Vector2 dir = DirectionToClosestWall();
        transform.localPosition = dir;
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
}

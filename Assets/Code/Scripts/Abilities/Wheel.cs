using UnityEngine;

public class Wheel : PlayerAbilityScript
{
    protected override PlayerAbilityType Ability => PlayerAbilityType.WHEEL;
    [SerializeField] Rigidbody2D prb;
    [SerializeField] PlayerAligner playerAligner;
    [SerializeField] Collider2D playerBaseColliderToDisable;

    [SerializeField] Transform playerBaseSpriteToRotate;

    [Tooltip("How fast the sprite orbits to the ground position")]
    [SerializeField] float smoothSpeed = 15f; 

    Vector2 spriteInitialLocalPos;
    Vector3 spriteInitialLocalRot;
    Vector3 spriteWorldOffset;
    Transform spriteParent;

    Vector2 currentOffsetDirection; 

    protected override void OnEnable()
    {
        base.OnEnable();
        prb.freezeRotation = false;
        playerAligner.align = false;
        playerBaseColliderToDisable.enabled = false;
        spriteInitialLocalPos = playerBaseSpriteToRotate.localPosition;
        spriteInitialLocalRot = playerBaseSpriteToRotate.localEulerAngles;

        spriteParent = playerBaseSpriteToRotate.parent;
        playerBaseSpriteToRotate.parent = null;
        
        spriteWorldOffset = playerBaseSpriteToRotate.position - transform.position; 
        currentOffsetDirection = spriteWorldOffset.normalized;
    }

    void LateUpdate()
    {
        Vector2 normal = playerInfo.GroundNormal;
        if (playerInfo.GroundNormal.magnitude == 0) {
            normal = Vector2.up;
        }

        currentOffsetDirection = Vector3.Slerp(currentOffsetDirection, normal, Time.deltaTime * smoothSpeed);

        playerBaseSpriteToRotate.position = (Vector2)transform.position + currentOffsetDirection * spriteWorldOffset.magnitude;

        playerBaseSpriteToRotate.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, currentOffsetDirection));
    }

    void OnDisable()
    {
        playerBaseSpriteToRotate.parent = spriteParent;
        playerBaseSpriteToRotate.localPosition = spriteInitialLocalPos;
        playerBaseSpriteToRotate.localEulerAngles = spriteInitialLocalRot;
        prb.freezeRotation = true;
        playerAligner.align = true;
        playerBaseColliderToDisable.enabled = true;
        prb.rotation = 0;
    }
}
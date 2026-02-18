using UnityEngine;

public class Wheel : PlayerAbilityScript
{
    protected override PlayerAbilityType Ability => PlayerAbilityType.WHEEL;
    [SerializeField] Rigidbody2D prb;
    [SerializeField] PlayerAligner playerAligner;
    [SerializeField] Collider2D playerBaseColliderToDisable;
    [SerializeField] GameObject playerBaseSpriteToRotate;

    Vector2 spriteInitialLocalPos;
    Vector3 spriteInitialLocalRot;
    Vector2 spriteInitialDiff;

    protected override void OnEnable()
    {
        base.OnEnable();
        prb.freezeRotation = false;
        playerAligner.align = false;
        playerBaseColliderToDisable.enabled = false;
        spriteInitialDiff = playerBaseSpriteToRotate.transform.position - transform.position;
        spriteInitialLocalPos = playerBaseSpriteToRotate.transform.localPosition;
        spriteInitialLocalRot = playerBaseSpriteToRotate.transform.localEulerAngles;
    }

    void Update()
    {
        // playerBaseSpriteToRotate.transform.position = (Vector2)transform.position + spriteInitialDiff;

        float angleDiff = playerAligner.GetAlignmentAngleDiff(playerBaseSpriteToRotate.transform.eulerAngles.z);
        playerBaseSpriteToRotate.transform.RotateAround(transform.position, Vector3.forward, angleDiff);
    }

    void OnDisable()
    {
        playerBaseSpriteToRotate.transform.localPosition = spriteInitialLocalPos;
        playerBaseSpriteToRotate.transform.localEulerAngles = spriteInitialLocalRot;
        prb.freezeRotation = true;
        playerAligner.align = true;
        playerBaseColliderToDisable.enabled = true;
        prb.rotation = 0;
    }
}

using UnityEngine;

public class Wheel : PlayerAbilityScript
{
    protected override PlayerAbilityType Ability => PlayerAbilityType.WHEEL;
    [SerializeField] Rigidbody2D prb;
    [SerializeField] PlayerAligner playerAligner;
    [SerializeField] Transform playerBaseToRotate;
    [SerializeField] Collider2D playerBaseColliderToDisable;

    [Tooltip("How fast the sprite orbits to the ground position")]
    [SerializeField] float smoothSpeed = 15f;

    [Tooltip("Effective rotational inertia when wheel is active. Very low = bearing-like, no braking.")]
    [SerializeField] float wheelInertia = 0.0001f;
    [SerializeField] AudioSource audioSource;

    Vector2 spriteInitialLocalPos;
    Vector3 spriteInitialLocalRot;
    Vector3 spriteWorldOffset;

    Vector2 currentOffsetDirection;

    float lastAngularVelocity = 0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        prb.freezeRotation = false;
        playerAligner.align = false;
        spriteInitialLocalPos = playerBaseToRotate.localPosition;
        spriteInitialLocalRot = playerBaseToRotate.localEulerAngles;
        playerBaseColliderToDisable.enabled = false;

        spriteWorldOffset = playerBaseToRotate.position - transform.position;
        currentOffsetDirection = spriteWorldOffset.normalized;

        audioSource.pitch = 1;
        audioSource.Play();
    }

    void FixedUpdate()
    {
        prb.inertia = wheelInertia;
    }

    void Update()
    {
        Debug.Log(Mathf.Abs(prb.angularVelocity));
        audioSource.pitch = Mathf.SmoothStep(0.7f, 1.3f, Mathf.Abs(prb.angularVelocity) / 2000f);
        audioSource.volume = Mathf.Clamp01(Mathf.Abs(prb.angularVelocity) / 2000f);
        if (!playerInfo.IsGrounded) {
            audioSource.volume *= 0.5f;
        }
    }

    void LateUpdate()
    {
        Vector2 normal = playerInfo.GroundNormal;
        if (playerInfo.GroundNormal.magnitude == 0)
        {
            normal = Vector2.up;
        }

        currentOffsetDirection = Vector3.Slerp(currentOffsetDirection, normal, Time.deltaTime * smoothSpeed);

        playerBaseToRotate.position = (Vector2)transform.position + currentOffsetDirection * spriteWorldOffset.magnitude;
        playerBaseToRotate.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, currentOffsetDirection));
    }

    void OnDisable()
    {
        playerBaseToRotate.localPosition = spriteInitialLocalPos;
        playerBaseToRotate.localEulerAngles = spriteInitialLocalRot;
        prb.freezeRotation = true;
        playerAligner.align = true;
        prb.rotation = 0;
        playerBaseColliderToDisable.enabled = true;
        audioSource.Stop();
    }
}
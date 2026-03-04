using UnityEngine;

public class Glider : PlayerAbilityScript
{
    protected override PlayerAbilityType Ability => PlayerAbilityType.GLIDER;

    [Tooltip("How much velocity is lost each second (0-1). Small values = subtle drag.")]
    [SerializeField] float damping = 0.05f;

    [Tooltip("How much downward velocity is converted to horizontal. Like glide ratio - higher = more forward speed from falling.")]
    [SerializeField] float heightToHorizontalConversion = 8f;

    [Tooltip("Max fall speed (positive). Jellyfish floatiness - gentle terminal velocity.")]
    [SerializeField] float maxFallSpeed = 8f;

    [SerializeField] Rigidbody2D prb;
    [SerializeField] PlayerAligner playerAligner;

    float originalDamping;

    private SpriteRenderer sr;

    protected override void OnEnable()
    {
        base.OnEnable();
        playerAligner.alignToGroundNormal = false;
        originalDamping = prb.linearDamping;
        prb.linearDamping = 0f; // We apply damping manually for fine control
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void OnDisable()
    {
        playerAligner.alignToGroundNormal = true;
        prb.linearDamping = originalDamping;
    }

    void Update()
    {
        Vector3 rot = transform.eulerAngles;
        rot.z = 0;
        transform.eulerAngles = rot;
    }

    void FixedUpdate()
    {
        Vector2 vel = prb.linearVelocity;
        float dt = Time.fixedDeltaTime;

        if (vel.x < 0) {
            sr.flipX = true;
        } else {
            sr.flipX = false;
        }

        // 1. Always lose a tiny bit of velocity (damping)
        vel *= 1f - damping * dt;

        // 2. Trade height for X velocity - redirect downward motion into forward (wing lift)
        if (vel.y < 0)
        {
            float transfer = heightToHorizontalConversion * (-vel.y) * dt;
            int direction = Mathf.Abs(vel.x) > 0.5f ? (int)Mathf.Sign(vel.x) : playerInfo.DirectionX;
            vel.x += transfer * direction;
            vel.y += transfer; // Reduce fall rate - we're redirecting, not adding
        }

        // 3. Jellyfish floatiness - cap fall speed for gentle descent
        if (vel.y < -maxFallSpeed)
            vel.y = -maxFallSpeed;

        prb.linearVelocity = vel;

        // Visual: tilt based on horizontal speed
        if (!playerInfo.IsGrounded)
            playerAligner.targetAngle = -Mathf.Sign(prb.linearVelocityX) * Mathf.Lerp(0, 30, Mathf.Abs(prb.linearVelocityX) / 30f);

        playerAligner.alignToGroundNormal = playerInfo.IsGrounded;
    }
}

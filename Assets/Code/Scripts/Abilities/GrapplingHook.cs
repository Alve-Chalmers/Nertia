using UnityEngine;

public class GrapplingHook : PlayerAbilityScript
{
    protected override PlayerAbilityType Ability => PlayerAbilityType.GRAPPLING_HOOK;

    [SerializeField] GrappleDetector detector;
    [SerializeField] Rigidbody2D prb;
    [SerializeField] DistanceJoint2D playerDistanceJoint;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSource2;

    GrappleTarget target;

    protected override void OnEnable()
    {
        base.OnEnable();

        audioSource.Play();

        pds.setPlayerDirFromVel = false;

        target = detector.Raycast();
        if (target.HitPoint == null)
            target.HitPoint = (Vector2)transform.position + detector.GetHookVec();

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target.HitPoint.Value);

        if (!target.HitHook)
            return;

        prb.rotation = 0;
        prb.freezeRotation = false;
        playerDistanceJoint.connectedAnchor = target.HitPoint.Value;
        playerDistanceJoint.anchor = Vector2.zero;
        playerDistanceJoint.distance = Vector2.Distance(transform.position, target.HitPoint.Value);
        playerDistanceJoint.maxDistanceOnly = true;
        playerDistanceJoint.enabled = true;

        audioSource2.Play();
    }

    void OnDisable()
    {
        prb.rotation = 0;
        prb.freezeRotation = true;
        playerDistanceJoint.enabled = false;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        if (target.HitPoint != null && target.HitHook)
        {
            lineRenderer.SetPosition(1, target.HitPoint.Value);
            float currentDist = Vector2.Distance(transform.position, target.HitPoint.Value);
            if (currentDist < playerDistanceJoint.distance)
                playerDistanceJoint.distance = currentDist;
        }
        else
        {
            lineRenderer.SetPosition(1, (Vector2)transform.position + detector.GetHookVec());
        }
    }
}

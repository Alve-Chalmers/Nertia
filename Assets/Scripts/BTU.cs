using UnityEngine;

public class BTU : MonoBehaviour
{
    [SerializeField] Rigidbody2D prb;
    [SerializeField] float force;
    [SerializeField] float maxSpeed;
    [SerializeField] float slopeLimit;
    int dir = -1;

    void OnEnable()
    {
        dir = -dir;
    }

    void Update()
    {

        int mask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1f, mask);
        if (hit.collider == null)
        {
            return;
        }

        if (prb.linearVelocity.magnitude >= maxSpeed)
        {
            prb.AddForce(-prb.linearVelocity);
            return;
        }

        Vector2 groundDir = Vector3.Cross(hit.normal, Vector3.forward);
        Debug.DrawRay(transform.position, prb.linearVelocity, Color.green);

        prb.AddForce(groundDir * force * dir);
    }
}

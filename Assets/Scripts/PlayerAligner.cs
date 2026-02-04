using UnityEngine;

public class PlayerAligner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.zero;

        int mask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1f, mask);
        if (hit.collider != null)
        {
            Vector3 e = transform.eulerAngles;
            e.z = -Vector2.Angle(Vector2.up, hit.normal);
            transform.eulerAngles = e;
        }
    }
}

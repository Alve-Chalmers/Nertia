using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInfo pi;
    [SerializeField] Rigidbody2D prb;

    void Awake()
    {
        pi.Position = transform.position;
        pi.Velocity = prb.linearVelocity;
    }

    void Update()
    {
        pi.Position = transform.position;
        pi.Velocity = prb.linearVelocity;
    }
    
}

using UnityEngine;

public class PlayerDirSetter : MonoBehaviour
{
    [SerializeField] PlayerInfo pi;
    [SerializeField] Rigidbody2D rb;

    public bool setPlayerDirFromVel = true; 

    void Update()
    {
        if (!setPlayerDirFromVel)
        {
            return;
        }

        if (rb.linearVelocityX > 1f)
        {
            pi.DirectionX = 1;
        }
        else if (rb.linearVelocityX < -1f)
        {
            pi.DirectionX = -1;
        }
    }
}

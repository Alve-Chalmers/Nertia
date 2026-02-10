using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInfo pi;

    void Awake()
    {
        pi.Position = transform.position;
    }

    void Update()
    {
        pi.Position = transform.position;
    }

}

using UnityEngine;

public class PlayerDebugEyes : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;

    float startX;

    void Start()
    {
        startX = transform.localPosition.x;
    }

    void Update()
    {
        transform.localPosition = new Vector2(startX * playerInfo.directionX, transform.localPosition.y);
    }
}

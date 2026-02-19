using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] SpriteRenderer sr;


    void Start()
    {
        sr.flipX = playerInfo.DirectionX == -1;
    }

    void Update()
    {
        sr.flipX = playerInfo.DirectionX == -1;
    }
}

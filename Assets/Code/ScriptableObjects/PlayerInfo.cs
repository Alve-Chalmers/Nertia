using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "ScriptableObjects/PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    public PlayerState State = PlayerState.BASE;
    public Vector2 Position;
}

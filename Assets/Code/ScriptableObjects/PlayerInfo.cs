using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "ScriptableObjects/PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    [System.NonSerialized] public PlayerState State = PlayerState.BASE;
    [System.NonSerialized] public Vector2 Position;
}

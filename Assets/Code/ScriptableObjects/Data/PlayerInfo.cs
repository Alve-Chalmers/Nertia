using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "ScriptableObjects/Data/PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    [System.NonSerialized] public PlayerAbilityType? AbilityUsed = null;
    [System.NonSerialized] public Vector2 Position;
    [System.NonSerialized] public int DirectionX; // 1 is right, -1 is left
    [System.NonSerialized] public Vector2 GroundNormal;
    [System.NonSerialized] public Vector2 Velocity;
    public bool IsGrounded => GroundNormal.magnitude != 0;
}

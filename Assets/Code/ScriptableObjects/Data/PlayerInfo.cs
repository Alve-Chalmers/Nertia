using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "ScriptableObjects/Data/PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    PlayerAbilityType? _abilityUsed = null;

    public PlayerAbilityType? AbilityUsed
    {
        get
        {
            return _abilityUsed;
        }
        set
        {
            PreviousAbilityUsed = _abilityUsed;
            _abilityUsed = value;
        }
    }

    [System.NonSerialized, ShowInPlayMode] public PlayerAbilityType? PreviousAbilityUsed = null;

    [System.NonSerialized, ShowInPlayMode] public Vector2 Position;

    /// <summary>
    /// 1 is right, -1 is left
    /// </summary>
    [System.NonSerialized, ShowInPlayMode] public int DirectionX = 1;

    /// <summary>
    /// zero if no ground found
    /// </summary>
    [System.NonSerialized, ShowInPlayMode] public Vector2 GroundNormal; 

    [System.NonSerialized, ShowInPlayMode] public Vector2 Velocity;
    
    public bool IsGrounded => GroundNormal.magnitude != 0;
}

using UnityEngine;

abstract public class PlayerAbilityScript : MonoBehaviour
{
    [SerializeField]
    protected PlayerInfo playerInfo;
    [SerializeField]
    protected PlayerDirSetter pds;
    protected abstract PlayerAbilityType Ability { get; }
    
    protected virtual void OnEnable()
    {
        pds.setPlayerDirFromVel = true;
        
        if (playerInfo != null)
        {
            playerInfo.AbilityUsed = Ability;
        }
    }
}

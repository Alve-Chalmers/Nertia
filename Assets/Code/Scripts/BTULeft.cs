using UnityEngine;

public class BTULeft : BTU
{
    protected override PlayerAbilityType Ability => PlayerAbilityType.BTU_LEFT;
    protected override int Dir => -1;
}

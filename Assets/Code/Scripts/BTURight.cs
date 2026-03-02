using UnityEngine;

public class BTURight : BTU
{
    protected override PlayerAbilityType Ability => PlayerAbilityType.BTU_RIGHT;
    protected override int Dir => 1;
}

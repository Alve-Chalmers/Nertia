using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    [SerializeField] Text t;

    public void SetData(PlayerAbilityType ability)
    {
        t.text = ability.ToString();
    }
}

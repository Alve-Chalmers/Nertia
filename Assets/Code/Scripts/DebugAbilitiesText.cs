using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DebugAbilitiesText : MonoBehaviour
{
    [SerializeField] UnlockedAbilities unlockedAbilities;

    Text t;

    void Awake()
    {
        t = GetComponent<Text>();
    }

    void Start()
    {
        t.text = "";
        foreach (var a in unlockedAbilities.Abilities)
        {
            string key = "";
            Debug.LogWarning(a);
            switch (a)
            {
                case PlayerAbilityType.BTU: key = "Q"; break;
                case PlayerAbilityType.DYING: key = "K"; break;
                case PlayerAbilityType.WHEEL: key = "W"; break;
                case PlayerAbilityType.BOXING_GLOVE: key = "E"; break;
                case PlayerAbilityType.GLIDER: key = "R"; break;
            }
            t.text += key + " - " + a.ToString() + "\n";
        }
    }

    void Update()
    {
        t.text = "";
        foreach (var a in unlockedAbilities.Abilities)
        {
            t.text += a.ToString() + "\n";
        }
    }
}

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
            t.text += a.ToString() + "\n";
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

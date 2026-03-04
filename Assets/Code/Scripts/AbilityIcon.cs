using UnityEngine;

public class AbilityIcon : MonoBehaviour
{
    public void SetData(PlayerAbilityType ability)
    {
        string targetName = ability.ToString();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Debug.Log(child.name + " " + targetName);
            child.gameObject.SetActive(child.name == targetName);
        }
    }
}

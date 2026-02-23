using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneWhenEntered : MonoBehaviour
{
    [SerializeField] SceneField sceneToGoTo;
    [SerializeField] List<string> tagsToLookFor;

    [SerializeField] ListOfAbilities abilitiesRequired;
    [SerializeField] UnlockedAbilities unlockedAbilities;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!tagsToLookFor.Contains(col.tag))
            return;
        
        if (abilitiesRequired != null) {
            foreach (var a in abilitiesRequired.abilities)
            {
                if (!unlockedAbilities.Abilities.Contains(a))
                {
                    Debug.Log("Did not have required abilities");
                    return;
                }
            }
        }

        SceneManager.LoadScene(sceneToGoTo);
    }
}

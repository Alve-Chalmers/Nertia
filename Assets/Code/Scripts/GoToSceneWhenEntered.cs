using System.Collections.Generic;
using UnityEngine;

public class GoToSceneWhenEntered : MonoBehaviour
{
    [SerializeField] SceneField sceneToGoTo;
    [SerializeField] List<string> tagsToLookFor;

    [SerializeField] ListOfAbilities abilitiesRequired;
    [SerializeField] UnlockedAbilities unlockedAbilities;
    [SerializeField] SOEventString gotoSceneEvent;
    [SerializeField] SOEvent freezePlayerInPlace;


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

        freezePlayerInPlace.Raise();
        gotoSceneEvent.Raise(sceneToGoTo);
    }
}

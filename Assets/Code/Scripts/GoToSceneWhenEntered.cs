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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!tagsToLookFor.Contains(col.tag))
            return;
        SceneManager.LoadScene(sceneToGoTo);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Hazard : MonoBehaviour
{
    [SerializeField] List<string> tagsToLookFor;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!tagsToLookFor.Contains(col.tag))
            return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}

using UnityEngine;
using System.Collections.Generic;

public class Hazard : MonoBehaviour
{
    [SerializeField] List<string> tagsToLookFor;
    [SerializeField] SOEvent playerDeathEvent;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!tagsToLookFor.Contains(col.tag))
            return;
        playerDeathEvent.Raise();
    }
    
}

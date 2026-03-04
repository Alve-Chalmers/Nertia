using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Hazard : MonoBehaviour
{
    [SerializeField] List<string> tagsToLookFor;
    [SerializeField] SOEvent playerDeathEvent;
    [SerializeField] float delayBeforeEvent = 0.05f; // seconds to wait before raising the event

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!tagsToLookFor.Contains(col.tag))
            return;

            StartCoroutine(DelayedRaise());
        
    }

    IEnumerator DelayedRaise()
    {
        yield return new WaitForSeconds(delayBeforeEvent);
        playerDeathEvent.Raise();
    }
}


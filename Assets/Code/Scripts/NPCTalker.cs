using System.Collections;
using UnityEngine;

public class NPCTalker : MonoBehaviour
{
    [SerializeField] float minPlayerCloseTime = 1f;
    [SerializeField] SOEventVector2 npcTalkingEvent;
    [SerializeField] SOEventVector2 npcTalkingPlayerExitEvent;
    
    Coroutine waitAndCheck;

    bool playerIsClose = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
            return;

        playerIsClose = true;
        waitAndCheck = StartCoroutine(WaitAndCheck());
    }

    IEnumerator WaitAndCheck()
    {
        yield return new WaitForSeconds(minPlayerCloseTime);
        
        // could check e.g. player speed also
        if (playerIsClose)
        {
            TalkToPlayer();
        }
    }

    void TalkToPlayer()
    {
        npcTalkingEvent.Raise(transform.position);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
            return;

        playerIsClose = false;
        StopCoroutine(waitAndCheck);
        npcTalkingPlayerExitEvent.Raise(transform.position);
    }
}

using System.Collections;
using UnityEngine;

public class NPCTalker : MonoBehaviour
{
    [SerializeField] float minPlayerCloseTime = 1f;
    [SerializeField] Conversation conversationToHave;
    [SerializeField] SOEventConversation startConversationEvent;
    [SerializeField] SOEventConversation stopConversationEvent;
    
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
        startConversationEvent.Raise(conversationToHave);
    }

    void StopTalkingToPlayer()
    {
        stopConversationEvent.Raise(conversationToHave);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
            return;

        playerIsClose = false;
        StopCoroutine(waitAndCheck);

        StopTalkingToPlayer();
    }
}

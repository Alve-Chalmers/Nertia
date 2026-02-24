using System.Collections;
using UnityEngine;

public class NPCTalker : MonoBehaviour
{
    [SerializeField] float minPlayerCloseTime = 1f;
    [SerializeField] Conversation conversationToHave;
    [SerializeField] SOEventConversation startConversationEvent;
    [SerializeField] SOEventConversation stopConversationEvent;
    
    [Header("For looking at player")]
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] SpriteRenderer spriteToFlip;
    
    Coroutine waitAndCheck;

    bool playerIsClose = false;

    void Awake()
    {
        conversationToHave.hasBeen = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
            return;

        playerIsClose = true;
        waitAndCheck = StartCoroutine(WaitAndCheck());
    }

    void Update()
    {
        if (!playerIsClose)
            return;
        
        if (spriteToFlip != null && playerInfo != null)
            spriteToFlip.flipX = playerInfo.Position.x < transform.position.x;
    }

    IEnumerator WaitAndCheck()
    {
        yield return new WaitForSeconds(minPlayerCloseTime);
        
        // could check e.g. player speed also
        if (playerIsClose)
        {
            TryTalkToPlayer();
        }
    }

    void TryTalkToPlayer()
    {
        if (conversationToHave.hasBeen && !conversationToHave.canHaveAgain)
            return;
        
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

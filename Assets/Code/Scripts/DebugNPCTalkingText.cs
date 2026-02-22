using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DebugNPCTalkingText : MonoBehaviour
{
    [SerializeField] SOEventVector2 npcTalkingEvent;
    [SerializeField] SOEventVector2 npcTalkingPlayerExitEvent;
    [SerializeField] Text t;

    bool npcTalking = false;

    void OnEnable()
    {
        npcTalkingEvent.Subscribe(OnNpcTalking);   
        npcTalkingPlayerExitEvent.Subscribe(OnNpcTalkingPlayerExit);
    }

    void OnDisable()
    {
        npcTalkingEvent.Unsubscribe(OnNpcTalking);    
        npcTalkingPlayerExitEvent.Unsubscribe(OnNpcTalkingPlayerExit);
    }

    void OnNpcTalking(Vector2 npcPosition)
    {
        npcTalking = true;
        t.text = "Hello mr. robot! I want to talk to you. I am at position" + npcPosition.ToString();
    }

    void OnNpcTalkingPlayerExit(Vector2 npcPosition)
    {
        npcTalking = false;
        t.text = "Goodbye mr. robot!";
        StartCoroutine(WaitThenClearText());
    }

    IEnumerator WaitThenClearText()
    {
        yield return new WaitForSeconds(1);
        if (!npcTalking)
            t.text = "";
    }
}

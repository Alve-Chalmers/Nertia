using System.Collections;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] SOEventDialogLine showDialogLine;
    [SerializeField] Conversation conversation;
    [SerializeField] int dialogLineIndex;
    [SerializeField] SOEvent hideDialogUI;

    float baseShowLineTime = 2;
    float extraTimePerLetter = 0.1f;
    bool hasTriggered = false;

    bool showingTheDialogLine = false;
    bool willCloseTheDialog = true;

    void Awake()
    {
        showDialogLine.Subscribe(OnShowDialogLine);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player") || hasTriggered)
            return;

        willCloseTheDialog = true;

        DialogLine line = conversation.dialogLines[dialogLineIndex];
        if (line.eventToRaiseBefore != null)
            line.eventToRaiseBefore.Raise();
        showDialogLine.Raise(line);
        hasTriggered = true;
        showingTheDialogLine = true;

        float time = baseShowLineTime + line.words.Length * extraTimePerLetter;
        StartCoroutine(WaitAndCloseDialog(time));
    }

    void OnShowDialogLine(DialogLine d)
    {
        DialogLine thisLine = conversation.dialogLines[dialogLineIndex];
        if (d != thisLine && showingTheDialogLine)
        {
            willCloseTheDialog = false;
            showingTheDialogLine = false;
            if (thisLine.eventToRaiseAfter != null)
                thisLine.eventToRaiseAfter.Raise();
        }
    }

    IEnumerator WaitAndCloseDialog(float time)
    {
        yield return new WaitForSeconds(time);
        if (willCloseTheDialog)
        {
            DialogLine thisLine = conversation.dialogLines[dialogLineIndex];
            if (thisLine.eventToRaiseAfter != null)
                thisLine.eventToRaiseAfter.Raise();
            hideDialogUI.Raise();
        }
        willCloseTheDialog = true;
    }
}

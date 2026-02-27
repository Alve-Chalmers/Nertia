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
        showDialogLine.Raise(line);
        hasTriggered = true;

        float time = baseShowLineTime + line.words.Length * extraTimePerLetter;
        StartCoroutine(WaitAndCloseDialog(time));
    }

    void OnShowDialogLine(DialogLine d)
    {
        if (d != conversation.dialogLines[dialogLineIndex])
        {
            willCloseTheDialog = false;
        }
    }

    IEnumerator WaitAndCloseDialog(float time)
    {
        yield return new WaitForSeconds(time);
        if (willCloseTheDialog)
        {
            hideDialogUI.Raise();
        }
        willCloseTheDialog = true;
    }
}

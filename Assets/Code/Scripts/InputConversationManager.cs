using UnityEngine;
using UnityEngine.InputSystem;

public class InputConversationManager : MonoBehaviour
{
    // listens
    [SerializeField] SOEventConversation startConv;
    [SerializeField] SOEventConversation endConv;

    // raises
    [SerializeField] SOEventDialogLine showDialogLine;
    [SerializeField] SOEvent hideDialogUI;

    InputAction nextLineInputAction;

    Conversation currentConversation;
    int indexOfNextDialogLine = 0;

    void OnEnable()
    {
        nextLineInputAction = InputSystem.actions.FindAction("NextDialogLine");
        nextLineInputAction.performed += OnNextLineKeyInput;
    
        startConv.Subscribe(OnStartInputConversation);
        endConv.Subscribe(OnEndInputConversation);
    }

    void OnDisable()
    {        
        nextLineInputAction.performed -= OnNextLineKeyInput;
    
        startConv.Unsubscribe(OnStartInputConversation);
        endConv.Unsubscribe(OnEndInputConversation);
    }

    void OnStartInputConversation(Conversation c)
    {
        currentConversation = c;
        indexOfNextDialogLine = 0;
        ShowNextLine();
    }

    void OnEndInputConversation(Conversation c)
    {
        if (currentConversation == null)
            return;
        End();
    }

    void OnNextLineKeyInput(InputAction.CallbackContext _)
    {
        if (currentConversation == null)
            return;

        ShowNextLine();
    }

    void ShowNextLine()
    {
        if (currentConversation == null)
        {
            return;
        }

        if (indexOfNextDialogLine > 0)
        {
            DialogLine dl = currentConversation.dialogLines[indexOfNextDialogLine-1];
            if (dl.eventToRaiseAfter != null)
            {
                dl.eventToRaiseAfter.Raise();
            }
        }

        if (indexOfNextDialogLine >= currentConversation.dialogLines.Count)
        {
            currentConversation.hasBeen = true;
            End();
            return;
        }

        {    
            DialogLine dl = currentConversation.dialogLines[indexOfNextDialogLine];
            if (dl.eventToRaiseBefore != null)
            {
                dl.eventToRaiseBefore.Raise();
            }
            showDialogLine.Raise(dl);
            indexOfNextDialogLine += 1;
        }
    }

    void End()
    {
        if (currentConversation != null &&
            currentConversation.dialogLines != null &&
            indexOfNextDialogLine == currentConversation.dialogLines.Count)
        {
            currentConversation.hasBeen = true;
        }

        indexOfNextDialogLine = 0;
        currentConversation = null;
        hideDialogUI.Raise();
    }
}

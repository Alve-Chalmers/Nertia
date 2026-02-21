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
        End();
    }

    void OnNextLineKeyInput(InputAction.CallbackContext _)
    {
        ShowNextLine();
    }

    void ShowNextLine()
    {
        if (currentConversation == null)
        {
            return;
        }
        if (indexOfNextDialogLine >= currentConversation.dialogLines.Count)
        {
            End();
            return;
        }
        DialogLine d = currentConversation.dialogLines[indexOfNextDialogLine];
        showDialogLine.Raise(d);
        indexOfNextDialogLine += 1;
    }

    void End()
    {
        indexOfNextDialogLine = 0;
        
        hideDialogUI.Raise();
    }
}

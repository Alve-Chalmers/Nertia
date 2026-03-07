using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text talkingText;
    [SerializeField] GameObject children;
    [SerializeField] SOEventDialogLine showDialogLineEvent;
    [SerializeField] SOEvent hideDialogEvent;
    [SerializeField] GameObject spacebarPrompt;

    void OnEnable()
    {
        showDialogLineEvent.Subscribe(OnShowDialogLine);
        hideDialogEvent.Subscribe(OnHideDialog);

        children.SetActive(false);
    }


    void OnDisable()
    {
        showDialogLineEvent.Unsubscribe(OnShowDialogLine);
        hideDialogEvent.Unsubscribe(OnHideDialog);
    }

    void OnShowDialogLine(DialogLine dl)
    {
        children.SetActive(true);
        nameText.text = dl.talkerName;
        talkingText.text = dl.words;
        spacebarPrompt.SetActive(!dl.hideSpacebarPrompt);
    }

    void OnHideDialog()
    {
        children.SetActive(false);
    }
}

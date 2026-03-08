using UnityEngine;

public class GrappleHighlight : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] SpriteRenderer hookSR;

    void Start()
    {
        SetHighlight(false);
    }

    public void SetHighlight(bool highlight)
    {
        if (sr != null)
            sr.enabled = highlight;
    }

    public void SetHooked(bool hooked)
    {
        if (hookSR != null)
            hookSR.enabled = hooked;
    }
}

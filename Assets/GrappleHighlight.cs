using UnityEngine;

public class GrappleHighlight : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;

    void Start()
    {
        SetHighlight(false);
    }

    public void SetHighlight(bool highlight)
    {
        if (sr != null)
            sr.enabled = highlight;
    }
}

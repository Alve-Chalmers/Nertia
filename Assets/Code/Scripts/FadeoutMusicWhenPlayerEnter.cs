using UnityEngine;

public class FadeoutMusicArea : MonoBehaviour
{
    [SerializeField] SOEvent fadeOut;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player") || fadeOut == null)
            return;

        fadeOut.Raise();
    }
}

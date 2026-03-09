using UnityEngine;

public class EmitOutsideCamera : MonoBehaviour
{
    [SerializeField] private SOEvent outsideCameraEvent;
    [SerializeField] private string tagToLookFor;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag(tagToLookFor))
            return;
        outsideCameraEvent.Raise();
    }
}

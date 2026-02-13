using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SOEvent", menuName = "ScriptableObjects/Events/Event")]
public class SOEvent : ScriptableObject
{
    private readonly UnityEvent e = new();

    public void Raise()
    {
        e.Invoke();
    }

    public void Subscribe(UnityAction listener)
    {
        e.AddListener(listener);
    }

    public void Unsubscribe(UnityAction listener)
    {
        e.RemoveListener(listener);
    }

    [ContextMenu("Raise Event")]
    private void RaiseFromInspector()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning($"[{name}] Cannot raise event outside of Play Mode.");
            return;
        }
        Debug.Log($"[{name}] Event raised from inspector.");
        Raise();
    }
}
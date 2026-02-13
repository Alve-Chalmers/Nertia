using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Generic base class for ScriptableObject events that pass data.
/// Cannot be used directly - create concrete subclasses for each type.
/// </summary>
public abstract class SOEvent<T> : ScriptableObject
{
    private readonly UnityEvent<T> e = new();

#if UNITY_EDITOR
    [SerializeField] private T debugValue;
    public T DebugValue => debugValue;
#endif

    public void Raise(T value)
    {
        e.Invoke(value);
    }

    public void Subscribe(UnityAction<T> listener)
    {
        e.AddListener(listener);
    }

    public void Unsubscribe(UnityAction<T> listener)
    {
        e.RemoveListener(listener);
    }
}

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SOEvent))]
public class SOEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SOEvent soEvent = (SOEvent)target;

        EditorGUILayout.Space(10);

        GUI.enabled = Application.isPlaying;

        if (GUILayout.Button("Raise Event", GUILayout.Height(30)))
        {
            soEvent.Raise();
            Debug.Log($"[{soEvent.name}] Event raised from inspector.");
        }

        GUI.enabled = true;

        if (!Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Enter Play Mode to raise this event.", MessageType.Info);
        }
    }
}

[CustomEditor(typeof(SOEventInt))]
public class SOEventIntEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventInt, int>((SOEventInt)target);
    }
}

[CustomEditor(typeof(SOEventFloat))]
public class SOEventFloatEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventFloat, float>((SOEventFloat)target);
    }
}

[CustomEditor(typeof(SOEventString))]
public class SOEventStringEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventString, string>((SOEventString)target);
    }
}

[CustomEditor(typeof(SOEventVector3))]
public class SOEventVector3Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventVector3, Vector3>((SOEventVector3)target);
    }
}

public static class SOEventEditorHelper
{
    public static void DrawRaiseButton<TEvent, TValue>(TEvent soEvent) where TEvent : SOEvent<TValue>
    {
        EditorGUILayout.Space(10);

        GUI.enabled = Application.isPlaying;

        if (GUILayout.Button("Raise Event (with Debug Value)", GUILayout.Height(30)))
        {
            soEvent.Raise(soEvent.DebugValue);
            Debug.Log($"[{soEvent.name}] Event raised with value: {soEvent.DebugValue}");
        }

        GUI.enabled = true;

        if (!Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Enter Play Mode to raise this event. Set 'Debug Value' above to test with different values.", MessageType.Info);
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;

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

        SOEventEditorHelper.DrawReferences(target);
    }
}

[CustomEditor(typeof(SOEventInt))]
public class SOEventIntEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventInt, int>((SOEventInt)target);
        SOEventEditorHelper.DrawReferences(target);
    }
}

[CustomEditor(typeof(SOEventFloat))]
public class SOEventFloatEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventFloat, float>((SOEventFloat)target);
        SOEventEditorHelper.DrawReferences(target);
    }
}

[CustomEditor(typeof(SOEventString))]
public class SOEventStringEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventString, string>((SOEventString)target);
        SOEventEditorHelper.DrawReferences(target);
    }
}

[CustomEditor(typeof(SOEventVector3))]
public class SOEventVector3Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventVector3, Vector3>((SOEventVector3)target);
        SOEventEditorHelper.DrawReferences(target);
    }
}

[CustomEditor(typeof(SOEventPlayerAbilityType))]
public class SOEventPlayerAbilityTypeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventPlayerAbilityType, PlayerAbilityType>((SOEventPlayerAbilityType)target);
        SOEventEditorHelper.DrawReferences(target);
    }
}

[CustomEditor(typeof(SOEventDialogLine))]
public class SOEventDialogLineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventDialogLine, DialogLine>((SOEventDialogLine)target);
        SOEventEditorHelper.DrawReferences(target);
    }
}

[CustomEditor(typeof(SOEventConversation))]
public class SOEventConversationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventConversation, Conversation>((SOEventConversation)target);
        SOEventEditorHelper.DrawReferences(target);
    }
}

[CustomEditor(typeof(SOEventAudioResource))]
public class SOEventAudioResourceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventAudioResource, AudioResource>((SOEventAudioResource)target);
        SOEventEditorHelper.DrawReferences(target);
    }
}

[CustomEditor(typeof(SOEventBool))]
public class SOEventBoolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventBool, bool>((SOEventBool)target);
        SOEventEditorHelper.DrawReferences(target);
    }
}

[CustomEditor(typeof(SOEventVector2))]
public class SOEventVector2Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SOEventEditorHelper.DrawRaiseButton<SOEventVector2, Vector2>((SOEventVector2)target);
        SOEventEditorHelper.DrawReferences(target);
    }
}


public static class SOEventEditorHelper
{
    private static readonly Dictionary<int, List<Object>> referenceCache = new();
    private static readonly Dictionary<int, bool> foldoutState = new();

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

    public static void DrawReferences(Object target)
    {
        int id = target.GetInstanceID();

        EditorGUILayout.Space(10);

        if (!foldoutState.ContainsKey(id))
            foldoutState[id] = false;

        EditorGUILayout.BeginHorizontal();
        foldoutState[id] = EditorGUILayout.Foldout(foldoutState[id], "References", true, EditorStyles.foldoutHeader);

        if (GUILayout.Button("Find", GUILayout.Width(60)))
        {
            referenceCache[id] = FindReferences(target);
            foldoutState[id] = true;
        }
        EditorGUILayout.EndHorizontal();

        if (!foldoutState[id])
            return;

        if (!referenceCache.ContainsKey(id))
        {
            EditorGUILayout.HelpBox("Click 'Find' to scan for references.", MessageType.Info);
            return;
        }

        var refs = referenceCache[id];

        if (refs.Count == 0)
        {
            EditorGUILayout.HelpBox("No references found.", MessageType.Info);
            return;
        }

        EditorGUILayout.LabelField($"{refs.Count} reference(s) found:", EditorStyles.miniLabel);

        EditorGUI.indentLevel++;
        foreach (var obj in refs)
        {
            if (obj == null) continue;
            EditorGUILayout.ObjectField(obj, obj.GetType(), false);
        }
        EditorGUI.indentLevel--;
    }

    private static List<Object> FindReferences(Object target)
    {
        string targetPath = AssetDatabase.GetAssetPath(target);
        string targetGuid = AssetDatabase.AssetPathToGUID(targetPath);

        if (string.IsNullOrEmpty(targetGuid))
            return new List<Object>();

        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths()
            .Where(p => p.StartsWith("Assets/") && !p.EndsWith(".cs"))
            .ToArray();

        var results = new List<Object>();

        int total = allAssetPaths.Length;
        for (int i = 0; i < total; i++)
        {
            string path = allAssetPaths[i];
            if (path == targetPath) continue;

            if (i % 50 == 0)
                EditorUtility.DisplayProgressBar("Finding References", path, (float)i / total);

            string fullPath = Path.GetFullPath(path);
            if (!File.Exists(fullPath)) continue;

            try
            {
                string content = File.ReadAllText(fullPath);
                if (content.Contains(targetGuid))
                {
                    var asset = AssetDatabase.LoadMainAssetAtPath(path);
                    if (asset != null)
                        results.Add(asset);
                }
            }
            catch { }
        }

        EditorUtility.ClearProgressBar();
        return results;
    }
}



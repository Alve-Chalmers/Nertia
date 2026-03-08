using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScriptableObject), true)]
public class ShowInPlayModeEditor : Editor
{
    public override bool RequiresConstantRepaint() => Application.isPlaying;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!Application.isPlaying)
            return;

        var fields = target.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        bool headerDrawn = false;

        foreach (var field in fields)
        {
            if (field.GetCustomAttribute<ShowInPlayModeAttribute>() == null)
                continue;

            if (!headerDrawn)
            {
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField("Runtime Values", EditorStyles.boldLabel);
                headerDrawn = true;
            }

            GUI.enabled = false;
            object value = field.GetValue(target);
            DrawField(field.Name, field.FieldType, value);
            GUI.enabled = true;
        }
    }

    void DrawField(string label, Type type, object value)
    {
        label = ObjectNames.NicifyVariableName(label);

        // Unwrap Nullable<T>
        Type underlying = Nullable.GetUnderlyingType(type);
        if (underlying != null)
        {
            if (value == null)
            {
                EditorGUILayout.LabelField(label, "null");
                return;
            }
            type = underlying;
        }

        if (type == typeof(int))
            EditorGUILayout.IntField(label, (int)value);
        else if (type == typeof(float))
            EditorGUILayout.FloatField(label, (float)value);
        else if (type == typeof(bool))
            EditorGUILayout.Toggle(label, (bool)value);
        else if (type == typeof(string))
            EditorGUILayout.TextField(label, (string)value ?? "");
        else if (type == typeof(Vector2))
            EditorGUILayout.Vector2Field(label, (Vector2)value);
        else if (type == typeof(Vector3))
            EditorGUILayout.Vector3Field(label, (Vector3)value);
        else if (type.IsEnum)
            EditorGUILayout.EnumPopup(label, (Enum)value);
        else if (typeof(UnityEngine.Object).IsAssignableFrom(type))
            EditorGUILayout.ObjectField(label, (UnityEngine.Object)value, type, true);
        else if (value is IEnumerable enumerable)
        {
            EditorGUILayout.LabelField(label);
            EditorGUI.indentLevel++;
            int i = 0;
            foreach (var item in enumerable)
            {
                if (item is Enum e)
                    EditorGUILayout.EnumPopup(i.ToString(), e);
                else
                    EditorGUILayout.LabelField(i.ToString(), item != null ? item.ToString() : "null");
                i++;
            }
            if (i == 0)
                EditorGUILayout.LabelField("(empty)");
            EditorGUI.indentLevel--;
        }
        else
            EditorGUILayout.LabelField(label, value != null ? value.ToString() : "null");
    }
}

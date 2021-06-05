using UnityEngine;
using UnityEditor;

namespace KOS.Waves
{
    [CustomEditor(typeof(SpawnRuleset))]
    public class RulesetEditorInspector : Editor
    {
        override public void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.Space(6);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("recoveryPeriod"));
            EditorGUILayout.Slider(serializedObject.FindProperty("spawnSpeed"), 10, 200, new GUIContent("Spawns/Minute"));
            EditorGUILayout.Slider(serializedObject.FindProperty("spawnSpeedMultiplier"), 1, 2, new GUIContent("Multiplier"));
            EditorGUILayout.Space(6);
            EditorList.Show(serializedObject.FindProperty("spawnRules"), ListOptions.ItemButtons);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
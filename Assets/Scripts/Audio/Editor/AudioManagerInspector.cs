
using UnityEditor;

namespace KOS.Audio
{
    [CustomEditor(typeof(AudioManager))]
    [CanEditMultipleObjects]
    public class AudioManagerInspector : Editor
    {
        override public void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Audio Clips");
            EditorList.Show(serializedObject.FindProperty("sounds"), ListOptions.ItemButtons);
            
            serializedObject.ApplyModifiedProperties();
            
        }
    };
}







using UnityEngine;
using UnityEditor;

namespace KOS.Audio
{
    [CustomPropertyDrawer(typeof(Sound))]
    [CanEditMultipleObjects]
    public class SoundPropertyDrawer: PropertyDrawer
    {
        SerializedProperty name, clip, pitch, volume, source;

        override public void OnGUI(Rect rect, SerializedProperty property, GUIContent title)
        {
            EditorGUI.BeginProperty(rect, title, property);
            
            // Rect prefixRect = new Rect(rect.x, rect.y, rect.width, 18);
            // EditorGUI.PrefixLabel(prefixRect, title);

            // EditorGUI.indentLevel++;

            name = property.FindPropertyRelative("name");
            Rect nameRect = new Rect(rect.x, rect.y, rect.width/2-1, 18);
            EditorGUI.PropertyField(nameRect, name, GUIContent.none);

            clip = property.FindPropertyRelative("clip");
            Rect clipRect = new Rect(rect.x + rect.width/2+2, rect.y, rect.width/2-2, 18);
            EditorGUI.PropertyField(clipRect, clip, GUIContent.none);

            volume = property.FindPropertyRelative("volume");
            Rect volumeRect = new Rect(rect.x, rect.y + 20, rect.width, 18);
            volume.floatValue = EditorGUI.Slider(volumeRect, GUIContent.none, volume.floatValue, 0f, 1f);

            // EditorGUI.indentLevel--;

            
            EditorGUI.EndProperty();
            
        }

        override public float GetPropertyHeight(SerializedProperty property, GUIContent title)
        {
            return EditorGUIUtility.singleLineHeight * 2 + 2;
        }
    }
}

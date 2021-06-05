using System;
using UnityEngine;
using UnityEditor;

namespace KOS.HexMap
{
    [CustomPropertyDrawer(typeof(HexCoords))]
    public class HexCoordinatesDrawer : PropertyDrawer
    {
        override public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            HexCoords coordinates = new HexCoords(
                property.FindPropertyRelative("x").intValue,
                property.FindPropertyRelative("z").intValue);

            position = EditorGUI.PrefixLabel(position, label);
            GUI.Label(position, coordinates.ToString());
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

namespace KOS
{
    [Flags] public enum ListOptions
    {
        None = 0,
        Size = 1,
        ListLabel = 2,
        ItemLabels = 3,
        ItemButtons = 4,
        NewItem = 5,
        Buttons = ItemButtons | NewItem,
        Default = Size | ListLabel | ItemLabels,
        All = Default | Buttons,
    };

    public static class EditorList
    {
        public static Color UIColor = new Color32(88, 88, 88, 255);

        public static void DrawUILine(int thickness = 2, int padding = 4)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding+thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 18;
            r.width += 32;
            EditorGUI.DrawRect(r, UIColor);
        }

        private static GUIContent moveUp = new GUIContent("\u2191", "Move Item Up");
        private static GUIContent moveDown = new GUIContent("\u2193", "Move Item Down");
        private static GUIContent duplicate = new GUIContent("\u21BB", "Duplicate Item");
        private static GUIContent delete = new GUIContent("\u2715", "Delete Item");
        private static GUIContent addItem = new GUIContent("+", "Add New Item");

        public static void Show(SerializedProperty list, ListOptions options = ListOptions.Default)
        {
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + "is not valid here loser!", MessageType.Error);
                return;
            }

            bool showSize           = (options & ListOptions.Size)        != 0;
            bool showListLabel      = (options & ListOptions.ListLabel)   != 0;
            bool showItemLabels     = (options & ListOptions.ItemLabels)  != 0;
            bool showItemButtons    = (options & ListOptions.ItemButtons) != 0;
            bool showNewItemButton  = (options & ListOptions.NewItem)     != 0;

            if (showListLabel)
            {
                EditorGUILayout.PropertyField(list);
                EditorGUI.indentLevel++;
            }

            if (!showListLabel || list.isExpanded)
            {
                if (showSize) EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
                DrawUILine();
                for (int i = 0; i < list.arraySize; i++)
                {
                    if (showItemLabels) EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), true);
                    else EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none, true);
                    EditorGUILayout.BeginHorizontal();
                    if (showItemButtons)
                    {
                        if (GUILayout.Button(moveUp))
                        {
                            list.MoveArrayElement(i, i-1);
                        }
                        if (GUILayout.Button(moveDown))
                        {
                            list.MoveArrayElement(i, i+1);
                        }
                        if (GUILayout.Button(duplicate))
                        {
                            list.InsertArrayElementAtIndex(i);
                        }
                        if (GUILayout.Button(delete))
                        {
                            int oldSize = list.arraySize;
                            list.DeleteArrayElementAtIndex(i);
                            if (list.arraySize == oldSize) list.DeleteArrayElementAtIndex(i);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    DrawUILine();
                }
                if (GUILayout.Button(addItem)) list.arraySize++;
            }

            if (showListLabel) EditorGUI.indentLevel--;
        }
    }
}


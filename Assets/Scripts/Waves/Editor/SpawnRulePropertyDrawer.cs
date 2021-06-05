using UnityEngine;
using UnityEditor;


namespace KOS.Waves
{
    [CustomPropertyDrawer(typeof(SpawnRule))]
    public class SpawnConditionPropertyDrawer: PropertyDrawer
    {
        SerializedProperty enemyType, firstWave, frequency, baseAmount, increasePerWave, sequence, cooldown;
        private float height = 18;
        private float line = 20;

        override public void OnGUI(Rect rect, SerializedProperty property, GUIContent title)
        {
            enemyType = property.FindPropertyRelative("enemyType");
            sequence = property.FindPropertyRelative("sequence");
            firstWave = property.FindPropertyRelative("firstWave");
            baseAmount = property.FindPropertyRelative("baseAmount");
            frequency = property.FindPropertyRelative("frequency");
            increasePerWave = property.FindPropertyRelative("increasePerWave");
            cooldown = property.FindPropertyRelative("cooldown");

            EditorGUI.BeginProperty(rect, title, property);

            Rect prefixRect = EditorGUI.PrefixLabel(rect, new GUIContent("Enemy Type"));

            Rect enemyTypeRect = new Rect(prefixRect.x, rect.y, prefixRect.width*9/10, 18);
            EditorGUI.PropertyField(enemyTypeRect, enemyType, GUIContent.none);
            Rect foldoutRect = new Rect(prefixRect.x + prefixRect.width-4, rect.y, prefixRect.width*1/10, 18);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, GUIContent.none);

            if (property.isExpanded)
            {
                Rect sequenceRect = new Rect(rect.x, rect.y + line, rect.width, height);
                sequence.intValue = EditorGUI.IntSlider(sequenceRect, new GUIContent("Sequence"), sequence.intValue, 1, 4);

                Rect firstWaveRect = new Rect(rect.x, rect.y + 2*line, rect.width, height);
                firstWave.intValue = EditorGUI.IntSlider(firstWaveRect, new GUIContent("First Wave"), firstWave.intValue, 1, 25);
                
                Rect baseAmountRect = new Rect(rect.x, rect.y + 3*line, rect.width, height);
                baseAmount.intValue = EditorGUI.IntSlider(baseAmountRect, new GUIContent("Base Amount"), baseAmount.intValue, 1, 20);

                Rect frequencyRect = new Rect(rect.x, rect.y + 4*line, rect.width, height);
                frequency.intValue = EditorGUI.IntSlider(frequencyRect, new GUIContent("Frequency"), frequency.intValue, 1, 25);
                
                Rect increasePerWaveRect = new Rect(rect.x, rect.y + 5*line, rect.width, height);
                increasePerWave.intValue = EditorGUI.IntSlider(increasePerWaveRect, new GUIContent("Increase Per Wave"), increasePerWave.intValue, 0, 10);
                
                Rect cooldownRect = new Rect(rect.x, rect.y + 6*line, rect.width, height);
                cooldown.floatValue = EditorGUI.Slider(cooldownRect, new GUIContent("Cooldown"), cooldown.floatValue, 0f, 0.5f);
            }

            EditorGUI.EndProperty();





        }

        override public float GetPropertyHeight(SerializedProperty property, GUIContent title)
        {
            if (property.isExpanded) return EditorGUIUtility.singleLineHeight * 7 + 18;
            else return EditorGUIUtility.singleLineHeight + 2;
            // return EditorGUIUtility.singleLineHeight * 5 + 14;
        }
    }
}
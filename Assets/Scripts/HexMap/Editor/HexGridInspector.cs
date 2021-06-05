using UnityEngine;
using UnityEditor;

namespace KOS.HexMap
{
    [CustomEditor(typeof(HexGrid))]
    public class HexGridInspector : Editor
    {
        override public void OnInspectorGUI()
        {
            HexGrid hexGrid = (HexGrid)target;

            base.OnInspectorGUI();

            if (GUILayout.Button("Build Map"))
            {
                hexGrid.CreateMap(hexGrid.cellCountX, hexGrid.cellCountZ);
            }
            
        }
    }
}
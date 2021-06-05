using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;

namespace KOS.HexMap
{
    public class HexGridEditor : MonoBehaviour
    {
        public HexGrid hexGrid;
        private Color activeColor;
        private int activeElevation, activeTerrainType, brushSize;
        [SerializeField]
        private bool editingElevation = true, continuous = true, editingBuildable = false, buildable = true;
        
        //> CHECK FOR INPUT
        private void Update()
        {
            if (continuous)
            {
                if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()) HandleInput();
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) HandleInput();
            }

        }

        //> HANDLE THE INPUT
        private void HandleInput()
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(inputRay, out RaycastHit hit)) EditCells(hexGrid.GetCell(hit.point));
        }

        //> EDIT A CELL WITH CURRENT SETTINGS
        private void EditCell(HexCell cell)
        {
            if (cell)
            {
                if (activeTerrainType != -1 && !editingBuildable)
                {
                    cell.TerrainType = activeTerrainType;
                }

                if (editingElevation && !editingElevation)
                {
                    cell.Elevation = activeElevation;
                }

                if (editingBuildable)
                {
                    cell.Buildable = buildable;
                }
            }
        }

        private void EditCells(HexCell center)
        {
            int centerX = center.coordinates.X;
            int centerZ = center.coordinates.Z;

            for (int r = 0, z = centerZ - brushSize; z <= centerZ; z++, r++) {
                for (int x = centerX - r; x <= centerX + brushSize; x++)
                {
                    EditCell(hexGrid.GetCell(new HexCoords(x, z)));
                }

            }
            for (int r = 0, z = centerZ + brushSize; z > centerZ; z--, r++) {
                for (int x = centerX - brushSize; x <= centerX + r; x++)
                {
                    EditCell(hexGrid.GetCell(new HexCoords(x, z)));
                }
            }

        }

        //> SET TERRAIN TYPE
        public void SetTerrainType(int index) => activeTerrainType = index;
        //> SET ELEVATION LEVEL
        public void SetElevation(float elevation) => activeElevation = (int)elevation;

        public void EditingElevation(bool toggle) => editingElevation = toggle;

        public void SetBrushSize(float size) => brushSize = (int)size;

        public void SetContinuous(bool toggle) => continuous = toggle;

        public void SetBuildable(bool toggle) => buildable = toggle;

        public void EditingBuildable(bool toggle) 
        {
            editingBuildable = toggle;
            hexGrid.EditingBuildable(toggle);
            hexGrid.Refresh();
        }
    }
}

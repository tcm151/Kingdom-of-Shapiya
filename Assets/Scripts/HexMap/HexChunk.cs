using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

namespace KOS.HexMap
{
    public class HexChunk : MonoBehaviour
    {
        private HexMesh hexMesh;

        [SerializeField]
        private HexCell[] cells;
        
        [SerializeField]
        public bool asset = false;

        //> INITIALIZE THE CHUNK
        private void Awake()
        {
            if (!asset)
            {
                hexMesh = GetComponentInChildren<HexMesh>();
                cells = new HexCell[HM.chunkSizeX * HM.chunkSizeZ];
            }
        }

        //> ADD A CELL TO THIS CHUNK
        public void AddCell(int index, HexCell newCell)
        {
            // link & assure proper hierarchy
            cells[index] = newCell;
            newCell.chunk = this;
            newCell.transform.SetParent(hexMesh.transform, false);
        }

        //> UPDATE THIS CHUNK*
        public void Refresh()
        {
            enabled = true;
        }

        //> UPDATE THIS CHUNK FOR REAL
        public void LateUpdate()
        {
            hexMesh.Triangulate(cells);
            enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace KOS.HexMap
{
    public class HexCell : MonoBehaviour
    {
        public HexCoords coordinates;
        public HexChunk chunk;

        [SerializeField] private HexCell[] neighbors;

        //> MODIFY THE ELEVATION PROPERTY
        private int elevation = -1;
        public int Elevation
        {
            get => elevation;

            set
            {
                if (elevation == value) return; // if unchanged; cancel

                elevation = value; // update elevation

                RefreshPosition(); // update position       
                Refresh(); // update mesh
            }
        }

        //> MODIFY THE COLOR PROPERTY
        public Color Color
        {
            get
            {
                if (editingBuildable)
                {
                    return !buildable ? HM.colors[7] : HM.colors[terrainType];
                }
                else return HM.colors[terrainType];
            }

        }

        //> MODIFY THE TERRAIN TYPE
        private int terrainType;
        public int TerrainType
        {
            get => terrainType;

            set
            {
                if (terrainType == value) return; // if unchanged; cancel

                terrainType = value;
                Refresh(); // update the mesh
            }
        }

        [SerializeField]
        public bool buildable = true, editingBuildable = false, occupied = false;
        public bool Buildable
        {
            get => buildable;

            set
            {
                if (buildable == value) return;

                buildable = value;
                Refresh();
            }
        }

        //> GET THE NEIGHBOR IN THE PROVIDED HEX DIRECTION
        public HexCell GetNeighbor(HexDirection direction) => neighbors[(int)direction];

        //> SET THE NEIGHBOR IN THE PROVIDED HEX DIRECTION
        public void SetNeighbor(HexDirection direction, HexCell cell)
        {
            neighbors[(int)direction] = cell;
            cell.neighbors[(int)direction.Opposite()] = this;
        }

        //> UPDATE THE ELEVATION OF THE HEX TILE
        private void RefreshPosition()
        {
            // update the hex tile
            Vector3 position = transform.localPosition;
            position.y = elevation * HM.elevationStep;
            transform.localPosition = position;
        }

        //> UPDATE THE MESH
        private void Refresh()
        {
            if (!chunk) return; // if no parent chunk; cancel

            chunk.Refresh(); // update the parent chunk

            // check if every neighbor exists, and update the neighbor chunk
            foreach (HexCell neighbor in neighbors)
            {
                if (neighbor != null && neighbor.chunk != chunk) neighbor.chunk.Refresh();
            }
        }

        //> SAVE THIS HEX CELL
        public void Save(BinaryWriter writer)
        {
            writer.Write(terrainType);
            writer.Write(elevation);
            writer.Write(buildable);
        }

        //> LOAD THIS HEX CELL
        public void Load(BinaryReader reader)
        {
            terrainType = reader.ReadInt32();
            elevation = reader.ReadInt32();
            buildable = reader.ReadBoolean();
            RefreshPosition(); // required or death
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

using UnityEditor;

namespace KOS.HexMap
{
    public class HexGrid : MonoBehaviour
    {
        public HexChunk chunkPrefab;
        public HexCell cellPrefab;

        public int chunkCountX, chunkCountZ;
        public int cellCountX = 10, cellCountZ = 10;

        public Color[] colors;

        [SerializeField]
        public HexChunk[] chunks;
        
        [SerializeField]
        public HexCell[] cells;
        
        [SerializeField]
        private bool asset = false;
        public bool IsAsset
        {
            get => asset;
            set => asset = value;
        }

        //> INITIALIZE THE GRID
        private void Awake()
        {
            if (!asset)
            {
                HM.colors = colors; // set the colors for the hex metrics
                CreateMap(cellCountX, cellCountZ); // create a new map with dimensions
            }

        }

        //> CREATE A NEW MAP
        public bool CreateMap(int x, int z)
        {
            // check if the map is of valid size
            if (x <= 0 || x % HM.chunkSizeX != 0 || z <= 0 || z % HM.chunkSizeZ != 0)
            {
                Debug.LogError("Unsupported map size. :(");
                return false; // exit when not supported
            }

            // clear the map data if one already exists
            if (chunks != null)
            {
                for (int i = 0; i < chunks.Length; i++) Destroy(chunks[i].gameObject);
            }

            // setup the map dimensions
            cellCountX = x;
            cellCountZ = z;
            chunkCountX = cellCountX / HM.chunkSizeX;
            chunkCountZ = cellCountZ / HM.chunkSizeZ;

            CreateChunks(); // build all the chunks
            CreateCells(); // the build all the cells

            return true;
        }

        //> CREATE THE PROPER AMOUNT OF CHUNKS
        private void CreateChunks()
        {
            // initialize chunk array with proper dimensions
            chunks = new HexChunk[chunkCountX * chunkCountZ];

            // loop over 2 dimensions (x,z)
            for (int z = 0, i = 0; z < chunkCountZ; z++) {
                for (int x = 0; x < chunkCountX; x++)
                {
                    HexChunk chunk = chunks[i++] = Instantiate(chunkPrefab); // instantiate a new chunk 
                    chunk.transform.SetParent(transform); // assure a clean hierarchy
                    chunk.name = "Chunk " + i;
                }
            }
        }

        //> CREATE EVERY CELL ACCORDING TO THE DIMENSIONS
        private void CreateCells()
        {
            // initialize the cells array with proper dimensions
            cells = new HexCell[cellCountZ * cellCountX];

            // loop over two dimensions (x,z)
            for (int z = 0, i = 0; z < cellCountZ; z++) {
                for (int x = 0; x < cellCountX; x++)
                {
                    CreateCell(x, z, i++); // handle creation separately
                }
            }
        }

        //> CREATE A CELL
        private void CreateCell(int x, int z, int i)
        {
            // calculate the hexagonal position
            Vector3 position;
            position.x = (x + z * 0.5f - z / 2) * (HM.innerRadius * 2f);
            position.y = 0f;
            position.z = z * (HM.outerRadius * 1.5f);

            // instantiate a cell prefab, set it's position and provide it with hex coords
            HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
            cell.transform.localPosition = position;
            cell.coordinates = HexCoords.FromOffset(x, z);
            cell.name = cell.coordinates.ToString();

            // complicated neighbor association, don't need to understand
            if (x > 0) cell.SetNeighbor(HexDirection.W, cells[i - 1]);
            if (z > 0)
            {
                if ((z & 1) == 0)
                {
                    cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX]);
                    if (x > 0) cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX - 1]);
                }
                else
                {
                    cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX]);
                    if (x < cellCountX - 1) cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX + 1]);
                }
            }

            cell.Elevation = 0; // default elevation

            AddCellToChunk(x, z, cell); // associate the cell to it's proper chunk
        }

        //> ADD CELL TO IT'S RESPECTIVE CHUNK
        private void AddCellToChunk(int x, int z, HexCell cell)
        {
            // calculate the proper chunk 
            int chunkX = x / HM.chunkSizeX;
            int chunkZ = z / HM.chunkSizeZ;
            HexChunk chunk = chunks[chunkX + chunkZ * chunkCountX];

            // add the cell to that chunk
            int localX = x - chunkX * HM.chunkSizeX;
            int localZ = z - chunkZ * HM.chunkSizeZ;
            chunk.AddCell(localX + localZ * HM.chunkSizeX, cell);
        }

        //> GET A CELL FROM POSITION
        public HexCell GetCell(Vector3 position)
        {
            // return the cell which was click upon
            position = transform.InverseTransformPoint(position);
            HexCoords coordinates = HexCoords.FromPosition(position);
            int index = coordinates.X + coordinates.Z * cellCountX + coordinates.Z / 2;
            return cells[index];
        }

        public HexCell GetCell(HexCoords coords)
        {
            int z = coords.Z;
            if (z < 0 || z >= cellCountZ) return null; // ignore if out of bounds
            int x = coords.X + z / 2;
            if (x < 0 || x >= cellCountX) return null; // ignore if out of bounds

            return cells[x + z * cellCountX];
        }

        public void EditingBuildable(bool toggle)
        {
            foreach (var cell in cells) cell.editingBuildable = toggle;
        }

        public void Refresh()
        {
            foreach (var chunk in chunks) chunk.Refresh();
        }

        //> SAVE THE MAP
        public void Save(BinaryWriter writer)
        {
            writer.Write(cellCountX);
            writer.Write(cellCountZ);

            for (int i = 0; i < cells.Length; i++) cells[i].Save(writer);
        }

        //> LOAD IN A SAVED MAP
        public void Load(BinaryReader reader, int header)
        {
            int x = 20, z = 15;

            if (header >= 1)
            {
                x = reader.ReadInt32();
                z = reader.ReadInt32();
            }

            if (x != cellCountX || z != cellCountZ)
            {
                if (!CreateMap(x, z)) return;
            }

            for (int i = 0; i < cells.Length; i++) cells[i].Load(reader);
            for (int i = 0; i < chunks.Length; i++) chunks[i].Refresh();
        }
    }
}


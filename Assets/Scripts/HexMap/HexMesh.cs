using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


namespace KOS.HexMap
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        private Mesh hexMesh;
        private MeshCollider meshCollider;
        static private readonly List<Vector3> Vertices = new List<Vector3>();
        static private readonly List<int> Triangles = new List<int>();
        static private readonly List<Color> Colors = new List<Color>();

        [SerializeField]
        public bool asset = false;

        //> INITIALIZE THE MESH
        private void Awake()
        {
            if (!asset)
            {
                GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
                meshCollider = gameObject.AddComponent<MeshCollider>();
                hexMesh.name = "Hex Mesh";
            }
        }

        //> CALCULATE THE HEXAGON GRID
        public void Triangulate(HexCell[] cells)
        {
            // clear all of the data before calculating
            hexMesh.Clear();
            Vertices.Clear();
            Triangles.Clear();
            Colors.Clear();

            // calculate each individual hexagon
            foreach (HexCell cell in cells) Triangulate(cell);

            // assign the calculated values to the mesh
            hexMesh.vertices = Vertices.ToArray();
            hexMesh.triangles = Triangles.ToArray();
            hexMesh.colors = Colors.ToArray();
            hexMesh.RecalculateNormals();
            meshCollider.sharedMesh = hexMesh;
        }

        //> CALCULATE AN INDIVIDUAL HEXAGON
        private void Triangulate(HexCell cell)
        {
            // only do certain directions for uniformity
            for (HexDirection direction = HexDirection.NE; direction <= HexDirection.NW; direction++)
                Triangulate(cell, direction);
        }

        //> CALCULATE AN INDIVIDUAL HEXAGON FOR A GIVEN DIRECTION
        private void Triangulate(HexCell cell, HexDirection direction)
        {
            // create a single triangle for this hexagon and color it
            Vector3 center = cell.transform.localPosition;
            Vector3 corner1 = center + HM.GetSolidCorner1(direction);
            Vector3 corner2 = center + HM.GetSolidCorner2(direction);
            AddTriangle(center, corner1, corner2);
            AddTriangleColor(cell.Color);

            // if proper direction, calculate the connection between neighboring hexes
            if (direction <= HexDirection.SE) TriangulateConnection(direction, cell, corner1, corner2);
        }

        //> CALCULATE THE CONNECTION BETWEEN NEIGHBORING HEXES
        private void TriangulateConnection(HexDirection direction, HexCell cell, Vector3 corner1, Vector3 corner2)
        {
            HexCell neighbor = cell.GetNeighbor(direction); // get the corresponding neighbor
            if (neighbor is null) return; // if no neighbor exists; cancel

            // adjust for neighbor height and connect the gap
            Vector3 bridge = HM.GetBridge(direction);
            Vector3 neighborCorner1 = corner1 + bridge;
            Vector3 neighborCorner2 = corner2 + bridge;
            neighborCorner1.y = neighborCorner2.y = neighbor.Elevation * HM.elevationStep;
            AddQuad(corner1, corner2, neighborCorner1, neighborCorner2);
            AddQuadColor(cell.Color, neighbor.Color);

            HexCell nextNeighbor = cell.GetNeighbor(direction.Next()); // get the next clockwise neighbor

            // if neighbor exists and is a proper direction then fill in the remaining gap
            if (direction <= HexDirection.E && nextNeighbor != null)
            {
                Vector3 neighborCorner3 = corner2 + HM.GetBridge(direction.Next());
                neighborCorner3.y = nextNeighbor.Elevation * HM.elevationStep;
                AddTriangle(corner2, neighborCorner2, neighborCorner3);
                AddTriangleColor(cell.Color, neighbor.Color, nextNeighbor.Color);
            }
        }

        //> APPLY THE COLOR TO THE TRIANGLE
        private void AddTriangleColor(Color color)
        {
            Colors.Add(color);
            Colors.Add(color);
            Colors.Add(color);
        }

        //> INTERPOLATE TRIANGLES COLOR
        private void AddTriangleColor(Color c1, Color c2, Color c3)
        {
            Colors.Add(c1);
            Colors.Add(c2);
            Colors.Add(c3);
        }

        //> CREATE A SIMPLE TRIANGLE
        private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = Vertices.Count;
            Vertices.Add(v1);
            Vertices.Add(v2);
            Vertices.Add(v3);
            Triangles.Add(vertexIndex + 0);
            Triangles.Add(vertexIndex + 1);
            Triangles.Add(vertexIndex + 2);
        }

        //> CREATE A SIMPLE QUAD
        private void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            int vertexIndex = Vertices.Count;
            Vertices.Add(v1);
            Vertices.Add(v2);
            Vertices.Add(v3);
            Vertices.Add(v4);
            Triangles.Add(vertexIndex + 0);
            Triangles.Add(vertexIndex + 2);
            Triangles.Add(vertexIndex + 1);
            Triangles.Add(vertexIndex + 1);
            Triangles.Add(vertexIndex + 2);
            Triangles.Add(vertexIndex + 3);
        }

        //> INTERPOLATE QUADS COLOR FANCY
        private void AddQuadColor(Color c1, Color c2, Color c3, Color c4)
        {
            Colors.Add(c1);
            Colors.Add(c2);
            Colors.Add(c3);
            Colors.Add(c4);
        }

        //> INTERPOLATE QUADS COLOR SIMPLE
        private void AddQuadColor(Color c1, Color c2)
        {
            Colors.Add(c1);
            Colors.Add(c1);
            Colors.Add(c2);
            Colors.Add(c2);
        }
    }
}

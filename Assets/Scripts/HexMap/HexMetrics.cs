using UnityEngine;

namespace KOS.HexMap
{
    static public class HM
    {
        // essential hex grid measurements
        public const float outerRadius = 1.5f;
        public const float innerRadius = outerRadius * 0.866025404f;
        public const float solidFactor = 0.75f;
        public const float blendFactor = 1f - solidFactor;
        public const float elevationStep = 0.5f;
        public const int   chunkSizeX = 10, chunkSizeZ = 10;

        static public Color[] colors;

        // array to hold position of each corner of hexagon
        static public Vector3[] corners = 
        {
            new Vector3(          0f,  0f,  outerRadius        ),
            new Vector3( innerRadius,  0f,  outerRadius *  0.5f),
            new Vector3( innerRadius,  0f,  outerRadius * -0.5f),
            new Vector3(          0f,  0f, -outerRadius        ),
            new Vector3(-innerRadius,  0f,  outerRadius * -0.5f),
            new Vector3(-innerRadius,  0f,  outerRadius *  0.5f),
            new Vector3(          0f,  0f,  outerRadius        ),
        };

        static public Vector3 GetCorner1(HexDirection direction) => corners[(int)direction + 0];
        static public Vector3 GetCorner2(HexDirection direction) => corners[(int)direction + 1];

        static public Vector3 GetSolidCorner1(HexDirection direction) => corners[(int)direction + 0] * solidFactor;
        static public Vector3 GetSolidCorner2(HexDirection direction) => corners[(int)direction + 1] * solidFactor;

        static public Vector3 GetBridge(HexDirection direction) => (corners[(int)direction] + corners[(int)direction + 1]) * blendFactor;
    }

    public enum HexDirection
    {
        NE, E, SE, SW, W, NW
    }

    static public class HexDirectionExtensions
    {
        static public HexDirection Opposite(this HexDirection direction) => (int)direction < 3 ? (direction + 3) : (direction - 3);
        static public HexDirection Prev(this HexDirection direction) => direction == HexDirection.NE ? HexDirection.NW : (direction - 1);
        static public HexDirection Next(this HexDirection direction) => direction == HexDirection.NW ? HexDirection.NE : (direction + 1);
    }
}

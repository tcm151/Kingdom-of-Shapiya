using UnityEngine;

namespace KOS.HexMap
{
    [System.Serializable]
    public struct HexCoords
    {
        [SerializeField]
        private int x, z; // cannot modify

        public int X {get => x;}
        public int Z {get => z;}

        //> CONSTRUCTOR
        public HexCoords(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        //> CALCUATE THE Y COORD ON DEMAND
        public int Y {get => (-x - z);}

        //> GET HEX COORDS FROM REGULAR GRID
        static public HexCoords FromOffset(int x, int z) => new HexCoords(x - z / 2, z);

        //> GET A COORDINATE STRING
        override public string ToString() => "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";

        //> GET SEPARATED COORDINATE STRINGS
        public string ToStringOnSeparateLines() => X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();

        //> CALCULATE THE HEX COORDINATE FROM THE WORLD POSITION
        static public HexCoords FromPosition(Vector3 position)
        {
            // get raw position
            float xf = position.x / (HM.innerRadius * 2f);
            float yf = -xf;

            // account for offset grid nature
            float offset = position.z / (HM.outerRadius * 3f);
            xf -= offset;
            yf -= offset;

            // round to the nearest int
            int xi = Mathf.RoundToInt(xf);
            int yi = Mathf.RoundToInt(yf);
            int zi = Mathf.RoundToInt(-xf-yf);

            // fix for rounding errors
            if (xi + yi + zi != 0)
            {
                float xr = Mathf.Abs(xf - xi);
                float yr = Mathf.Abs(yf - yi);
                float zr = Mathf.Abs(-xf -yf - zi);

                if (xr > yr && xr > zr) xi = -yi - zi;

                else if (zr > yr) zi = -xi - yi;
            }
            return new HexCoords(xi, zi);
        }
    }
}

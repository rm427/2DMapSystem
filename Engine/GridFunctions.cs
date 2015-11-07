using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class GridFunctions
    {
        public Location[,] GenerateTileArray(int XDimension, int YDimension)
        {

            /// Declare a Location class array of length Dimension*Dimension
            Location[,] LocArray = new Location[XDimension, YDimension];

            /// Generate each tile
            for (int x = 0; x < XDimension; x = x + 1)
            {
                for (int y = 0; y < YDimension; y = y + 1)
                {
                    int z = x * XDimension + y;   /// ID calculator
                    Location location = new Location(z, x, y, "grass", "");
                    LocArray[x, y] = location;
                    string String = "ID: " + z + " Coord: " + location.XRef + "," + location.YRef;
                }
            }
            LocArray[1, 4].ObjectType = "house7";
            LocArray[2, 4].ObjectType = "house8";
            LocArray[3, 4].ObjectType = "house9";
            LocArray[1, 3].ObjectType = "house4";
            LocArray[2, 3].ObjectType = "house5";
            LocArray[3, 3].ObjectType = "house6";
            LocArray[1, 2].ObjectType = "house1";
            LocArray[2, 2].ObjectType = "house2";
            LocArray[3, 2].ObjectType = "house3";

            return LocArray;
        }        
    }
}

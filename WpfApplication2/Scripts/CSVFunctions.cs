using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.Scripts
{
    class CSVFunctions
    {
        public static void SaveCSV(Location[,] array, string filename, int XDimension, int YDimension)
        {
            /// Will save to a .csv file. 
            /// 1st column) ID, 2) XRef, 3) YRef, 4) TileType, 5) ObjectType.
            /// Comma acts as the seperator.
            int z;
            string line;
            using (StreamWriter outfile = new StreamWriter(@filename))
            {
                for (int x = 0; x < XDimension; x++)
                {
                    for (int y = 0; y < YDimension; y++)
                    {
                        z = (x * XDimension) + y;
                        line = array[x, y].ID + "," + array[x, y].XRef + "," + array[x, y].YRef + "," + array[x, y].TileType + "," + array[x, y].ObjectType;
                        outfile.WriteLine(line);
                    }
                }
            }

        }

        public static Location[,] LoadCSV(string filename)
        {
            Location[,] LocationArray = new Location[0, 0];
            /// 1st column) ID, 2) XRef, 3) YRef, 4) TileType, 5) ObjectType.
            string contents = File.ReadAllText(@filename);
            string[] ContentsArray = contents.Split('\n');
            int b = ContentsArray.Length;
            int Dimension = Convert.ToInt32(Math.Round(Math.Sqrt(b))); /// NOTE: Will fail if the grid is not perfect square.
            LocationArray = new Location[Dimension, Dimension];
            string[,] TwoDContents = new string[5, b];
            string[] TempString;
            string AnotherTempString = "";

            for (int a = 0; a < b - 1; a++)
            {
                AnotherTempString = ContentsArray[a];
                TempString = ContentsArray[a].Split(',');
                for (int c = 0; c < 5; c++)
                {
                    TwoDContents[c, a] = TempString[c];
                }
                int id = Int32.Parse(TempString[0]);
                int xref = Int32.Parse(TempString[1]);
                int yref = Int32.Parse(TempString[2]);
                TempString[4] = TempString[4].Replace("\r", "");
                /// \r will appear on the last string.


                Location location = new Location(id, xref, yref, TempString[3], TempString[4]);
                LocationArray[xref, yref] = location;
            }

            return LocationArray;

        }
    }
}

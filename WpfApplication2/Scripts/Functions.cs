using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.Scripts
{
    public class Functions
    {
        /// Instead of void below (meaning return void), it returns Location.
        /// This means we have Location in the header.
        /// This used to find the observation of a 1D array from x,y coords
        /// Now that the array is 2D, this is no longer needed.
        //public static Location find(Location[] locarray, int dimension, int x, int y)
        //{
        //    Location[] LocArray = locarray;
        //    int Dimension = dimension;
        //    int X = x;
        //    int Y = y;

        //    Location loc = LocArray[0];

        //    for (int i = 0; i < Dimension; i = i + 1) /// i replaces x
        //    {
        //        for (int j = 0; j < Dimension; j = j + 1) /// j replaces y
        //        {
        //            int z = i * Dimension + j;   /// ID calculator
        //            if (LocArray[z].XRef == X)
        //            {
        //                loc = LocArray[z];
        //            }
        //        }
        //    }
        //    /// This function should take about 0.012 seconds with 1000*1000 dimensions

        //    return loc;
        //}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Location
    {
        public int ID { get; set; }
        public int XRef { get; set; }
        public int YRef { get; set; }
        public string TileType { get; set; }
        public string ObjectType { get; set; }

        public Location(int id, int xref, int yref, string tiletype, string objecttype)
        {
            ID = id;
            XRef = xref;
            YRef = yref;
            TileType = tiletype;
            ObjectType = objecttype;
        }
    }
}
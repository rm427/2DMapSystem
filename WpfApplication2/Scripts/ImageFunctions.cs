using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.Scripts
{
    public class ImageFunctions
    {
        public string UpdateImage(string objecttype)
        {
            string ObjectType = objecttype;
            string FilePath = "";

            if (ObjectType == "house1")
            {
                FilePath = "pack://application:,,,/Resources/House/1.png";
            }
            if (ObjectType == "house2")
            {
                FilePath = "pack://application:,,,/Resources/House/2.png";
            }
            if (ObjectType == "house3")
            {
                FilePath = "pack://application:,,,/Resources/House/3.png";
            }
            if (ObjectType == "house4")
            {
                FilePath = "pack://application:,,,/Resources/House/4.png";
            }
            if (ObjectType == "house5")
            {
                FilePath = "pack://application:,,,/Resources/House/5.png";
            }
            if (ObjectType == "house6")
            {
                FilePath = "pack://application:,,,/Resources/House/6.png";
            }
            if (ObjectType == "house7")
            {
                FilePath = "pack://application:,,,/Resources/House/7.png";
            }
            if (ObjectType == "house8")
            {
                FilePath = "pack://application:,,,/Resources/House/8.png";
            }
            if (ObjectType == "house9")
            {
                FilePath = "pack://application:,,,/Resources/House/9.png";
            }

            return FilePath;
        }

        public string UpdateDisplayedTile(string tiletype)
        {
            string TileType = tiletype;
            string FilePath = "";

            if (TileType == "grass")
            {
                FilePath = "pack://application:,,,/Resources/Tile/Grass.png";
            }
            if (TileType == "empty")
            {
                FilePath = "pack://application:,,,/Resources/Tile/Empty.png";
            }

            return FilePath;
        }
    }
}

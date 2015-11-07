using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace WpfApplication2.Scripts
{
    public class Player
    {
        public int MaxHitPoints { get; set; }
        public int HitPoints { get; set; }
        public int Speed { get; set; }
        public string Sprite { get; set; }
        public int XRef { get; set; }
        public int YRef { get; set; }
 

        public Player(int maxhitpoints, int hitpoints, int speed, string sprite, int xref, int yref)
        {
            MaxHitPoints = maxhitpoints;
            HitPoints = hitpoints;
            Speed = speed;
            Sprite = sprite;
            XRef = xref;
            YRef = yref;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puertos
{
    class Direction
    {
        public const string Stay = "Stay";
        public const string North = "North";
        public const string East = "East";
        public const string South = "South";
        public const string West = "West";
    }
    class posItems
    {
       Mine mine{ get; set; }



    }
    class Mine
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    public class Item
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int PointValue { get; set; }
         
        public override string ToString() {
            return $"{Name}: ({PointValue} gold) {Description}";
        }
    }
}

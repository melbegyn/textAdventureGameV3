using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    public class Player
    {
        public List<Item> inventory;
        public int score;
         
        public List<Item> Inventory { get => inventory; set => inventory = value; }
        public int Score { get => score; set => score = value; }

        public Player() {
            /* init the list of items */
            inventory = new List<Item>();
        }
    }
}

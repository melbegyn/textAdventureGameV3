using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    public class Room
    {

        public string RoomName { get; set; }
        public string RoomDescription { get; set; }
        public List<Item> Items { get; set; }
        public Dictionary<string, Room> Transitions { get; set; }

        public bool FinalRoom { get; set; }

        public void PrintRoom() {
            Console.WriteLine(this.RoomDescription);
            if (Items.Count > 0) {
                foreach (Item item in Items) {
                    Console.WriteLine(item);
                }
            }
        }
    }
}

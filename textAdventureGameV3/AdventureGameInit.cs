using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    public class AdventureGameInit : AdventureGame {
         
        public AdventureGameInit() {
            currentRoom = entranceHall;
            enemyRoom = paintingRoom;

            setupRooms();
            initRoomsList();
        }

        private void initRoomsList() {
            roomslist.Add(outside);
            roomslist.Add(entranceHall);
            roomslist.Add(livingRoom);
            roomslist.Add(paintingRoom);
            roomslist.Add(kitchen);
            roomslist.Add(fancyBedroom);
            roomslist.Add(basement);
            roomslist.Add(library);
            roomslist.Add(laboratory);
            roomslist.Add(skeletonRoom);
        }

        /*
         ************** 
         *    ROOMS
         ************** 
         */

        // outside the house
        Room outside = new Room
        {
            RoomName = "Outside",
            RoomDescription = "You decide to take what you've already found and leave. Something about this place unnerves you, and you never return.",
            Items = new List<Item>(),
            FinalRoom = true
        };

        // entrance hall
        Room entranceHall = new Room
        {
            RoomName = "Entrance hall",
            RoomDescription = "You are in the entrance hall. There is a door leading further into the house to the north, and an exit to the south.",
            Items = new List<Item>()
        };

        // living room
        Room livingRoom = new Room
        {
            RoomName = "Living room",
            RoomDescription = "You are in the living room. There are doors to the north, south, east, and west. There is a staircase going down.",
            Items = new List<Item> {
                new Item   { Name = "trophy case", Description = "A trophy case containing a massive golden cup", PointValue = 150 },
                new Weapon { Name = "elven sword", Description = "A leaf-bladed longsword, elven crafted.", PointValue = 150 },
                new Item   { Name = "fancy rug", Description = "A large, oriental-style rug with exceptional craftsmanship.", PointValue = 100 }
            }
        };

        // painting room
        Room paintingRoom = new Room
        {
            RoomName = "Painting room",
            RoomDescription = "You are in the painting room. There is a Harpsichord. A painting depicts a skeleton holding open a gateway to an underground passage. A male elf is entering the passage. A female elf is holding a strange orb. A human man stands to the side observing.",
            Items = new List<Item> {
                new Enemy { Name = AdventureGameConstants.ENEMY_HARPSICHORD, Description = "An incredibly heavy harpsichord.", PointValue = 300, TypeOfAttack = "destroy", LostBattleMessage = "A destroyed harpsichord, worth 0" },
                new Item  { Name = "oil painting", Description = "The painting depicts a skeleton holding open a gateway to an underground passage. A male elf is entering the passage. A female elf is holding a strange orb. A human man stands to the side observing.", PointValue = 150 }
            }
        };


        // kitchen
        Room kitchen = new Room
        {
            RoomName = "Kitchen",
            RoomDescription = "You are in the kitchen. A table seems to have been used recently for the preparation of food. A passage leads to the west.",
            Items = new List<Item> {
                new Item { Name = "sack of peppers", Description = "A brown sack containing spicy green peppers.", PointValue = 1 },
                new Item { Name = "glass of water", Description = "A refreshing glass of cold water.", PointValue = 1 }
            }
        };

        // fancy bedroom
        Room fancyBedroom = new Room
        {
            RoomName = "Fancy bedroom",
            RoomDescription = "You are in the fancy bedroom. There is a four-poster bed with red sheets. There is a closed chest at the foot of the bed.",
            Items = new List<Item> {
                new Item { Name = "boots", Description = "Tough boots with spikes for climbing.", PointValue = 10 },
                new Item { Name = "sheets", Description = "Fancy silk sheets", PointValue = 50 },
                new Item { Name = "gold", Description = "Shiny gold coins.", PointValue = 100 }
            }
        };

        // cellar / basement
        Room basement = new Room
        {
            RoomName = "Basement",
            RoomDescription = "You are in a tidy cellar. There are barrels of wine here. A door leads to the north, and a staircase goes up.",
            Items = new List<Item>{
                new Item { Name = "wine", Description = "A bottle of fine wine.", PointValue = 50 }
            }
        };

        // library
        Room library = new Room
        {
            RoomName = "Library",
            RoomDescription = "You are in a large library. There are many books about anatomy, history, and alchemy. Some of the books are written in Elven. There is a door to the south.",
            Items = new List<Item> {
                new Item { Name = "necklace", Description = "A ruby necklace.", PointValue = 125 },
                new Item { Name = "elven book", Description = "A tome written in the indecipherable elven dialect.", PointValue = 100 }
            }
        };

        // laboratory
        Room laboratory = new Room
        {
            RoomName = "Laboratory",
            RoomDescription = "You find yourself in a strange laboratory. A lamp with a red filter lights the room. There is a secret passage to the south. There is a door with a skull to the north.",
            Items = new List<Item> {
                new Item  { Name = "orb", Description = "The Orb of Yendor, an ancient artifact that has been missing for many years.", PointValue = 500 },
                new Item  { Name = "flask", Description = "A flask encrusted with gems.", PointValue = 200 },
                new Item  { Name = "lamp", Description = "A lamp with a ruby-tinted filter.", PointValue = 30 },
                new Enemy { Name = AdventureGameConstants.ENEMY_TROMP, Description = "It can disinfect you in one minute.", PointValue = 540, TypeOfAttack = "strike", LostBattleMessage = "Tromp lies destroyed here." }
            }
        };

        // skeleton room
        Room skeletonRoom = new Room
        {
            RoomName = "Skeleton room",
            RoomDescription = "The strange door opens into darkness. You peer in, and a pair of skeletal hands reach out and drags you in! The last thing you see is a strange underground passage before the last of the light disappears.",
            Items = new List<Item>(),
            FinalRoom = true
        };
         

        /*
         ************** 
         * SETUP ROOMS 
         ************** 
         */
        public void setupRooms() {

            // entrance hall
            entranceHall.Transitions = new Dictionary<string, Room> {
                { AdventureGameConstants.SOUTH, outside},
                { AdventureGameConstants.NORTH, livingRoom}
            };

            // living room
            livingRoom.Transitions = new Dictionary<string, Room> {
                { AdventureGameConstants.SOUTH, entranceHall},
                { AdventureGameConstants.NORTH, fancyBedroom},
                { AdventureGameConstants.EAST, kitchen},
                { AdventureGameConstants.WEST, paintingRoom},
                { AdventureGameConstants.DOWN, basement}
            };

            // fancy bedroom
            fancyBedroom.Transitions = new Dictionary<string, Room> {
                { AdventureGameConstants.SOUTH, livingRoom}
            };

            // kitchen
            kitchen.Transitions = new Dictionary<string, Room> {
                { AdventureGameConstants.WEST, livingRoom}
            };

            // painting room
            paintingRoom.Transitions = new Dictionary<string, Room> {
                { AdventureGameConstants.EAST, livingRoom}
            };

            // basement
            basement.Transitions = new Dictionary<string, Room> {
                { AdventureGameConstants.UP, livingRoom},
                { AdventureGameConstants.NORTH, library}
            };

            // library
            library.Transitions = new Dictionary<string, Room> {
                { AdventureGameConstants.ACCESS_SECRET_DOOR, laboratory},
                { AdventureGameConstants.SOUTH, basement}
            };

            // laboratory
            laboratory.Transitions = new Dictionary<string, Room> {
                { AdventureGameConstants.NORTH, skeletonRoom},
                { AdventureGameConstants.SOUTH, library}
            };
        }   
    } 
}

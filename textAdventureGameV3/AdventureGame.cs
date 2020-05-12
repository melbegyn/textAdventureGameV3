using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace textAdventureGameV3
{

    public static class LinqHelper
    {
        public static void RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey)
        {
            TValue value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }
    }

    public class AdventureGame
    {

        public Room currentRoom;
        public List<Room> roomslist = new List<Room>();
        public int moves = 0;

        public void StartGame(Player player)
        {
            Console.WriteLine(AdventureGameConstants.INTRO);
            InputLoop(player);
        }

        public void InputLoop(Player player)
        {
            while (true) {

                // display room information
                currentRoom.PrintRoom();

                string input = Console.ReadLine();

                if (input == AdventureGameConstants.ACTION_QUIT) {
                    Console.WriteLine(AdventureGameConstants.MESSAGE_QUIT_GAME);
                    PrintScore(player);
                    break;
                }

                /* ********************* */
                /* ACTIONS OF THE PLAYER */
                /* ********************* */

                /* ACTION 1 - DIRECTION: the player want to move to the next room  */
                
                if (currentRoom.Transitions.TryGetValue(input, out Room nextRoom)) {

                    /** if the secret door is opened **/
                    bool secretDoorOpened = openSecretDoor(input);
                    if (secretDoorOpened) {
                        Item elvenBook = getItemFromRoom(input);
                        addItemToInventory(player, elvenBook);
                        LinqHelper.RenameKey(currentRoom.Transitions, AdventureGameConstants.ACCESS_SECRET_DOOR, AdventureGameConstants.NORTH);
                    }

                    /** update the current room **/
                    currentRoom = nextRoom;

                    /** if the number of moves is even, the enemy is moved **/
                    moves++;
                    if (moves % 2 == 0) {
                        moveEnemy();
                    }
                }

                /* ACTION 2 - PICK UP: add an item to their inventory by typing the command */
                else if (input.StartsWith(AdventureGameConstants.ACTION_PICK_UP)) {

                    Item itemPicked = getItemFromRoom(input);
                    if (itemPicked != null){
                        addItemToInventory(player, itemPicked);
                    }
                    else {
                        Console.WriteLine(AdventureGameConstants.MESSAGE_ITEM_NOT_IN_ROOM);
                    }
                }

                /* ACTION 3 - DROP: remove an item from his inventory and leaves it in the current room */
                else if (input.StartsWith(AdventureGameConstants.ACTION_DROP)) {

                    Item itemDropped = getItemFromInventory(input, player);
                    if (itemDropped != null) {
                        removeItemFromInventory(player, itemDropped);
                    }
                    else {
                        Console.WriteLine(AdventureGameConstants.MESSAGE_ITEM_NOT_IN_INVENTORY);
                    }
                }

                /* ACTION 4 - DESCRIBE: describe an item in inventory or in the same room */
                else if (input.StartsWith(AdventureGameConstants.ACTION_DESCRIBE)) {

                    Item itemDescribed = describeItem(input, player);

                    if (itemDescribed != null) {
                        Console.WriteLine(itemDescribed.Description);
                    }
                    else {
                        Console.WriteLine(AdventureGameConstants.MESSAGE_ITEM_NOT_INVENT_AND_ROOM);
                    }
                }

                /* ACTION 5 - SHOW INVENTORY: the player can ask to see the items stores in his inventory */
                else if (input.StartsWith(AdventureGameConstants.ACTION_SHOW_INVENTORY)) {
                    showInventory(player);
                }

                /* ACTION 6 - ATTACK ENEMY WITH WEAPON: the player can attack the enemy in the room with a weapon */
                else if (input.StartsWith(AdventureGameConstants.ACTION_ATTACK_ENEMY) && input.Contains("with")) {
                    
                    // lui cest tromp
                    Console.WriteLine("Troll is struck with elven sword!");
                    // lui cest harpsichord
                    Console.WriteLine("harpsichord was destroyed by elven sword!");
                }

                // If the action entered by the player isn't correct / doesn't exist
                else {
                    Console.WriteLine(AdventureGameConstants.MESSAGE_INVALID_ACTION);
                }

                // the room is the final room to the end = finish the game
                if (currentRoom.FinalRoom) {

                    currentRoom.PrintRoom();
                    PrintScore(player);
                    break;
                }
            }
        }

        /**
         * openSecretDoor(string input)
         * Check if the player is in the library and has picked up the elven book 
         */
        private bool openSecretDoor(string input) {
            bool hasPickedBook = false;
            if (input == AdventureGameConstants.ACCESS_SECRET_DOOR && currentRoom.RoomName == "Library"){
                hasPickedBook = true;
            }

            return hasPickedBook;
        }

        /*
         * PrintScore(Player player)
         * Print the final score of the player
         */
        public void PrintScore(Player player) {
            Console.WriteLine($"Your score was: {player.Score}");
        }

        /*
         * getItemFromRoom(String input)
         * Get the item from the list of items in the room
         */
        private Item getItemFromRoom(String input) {

            // check if the room has items
            if (currentRoom.Items.Count != 0) {
                foreach (Item item in currentRoom.Items.ToList()) {
                    if (input.Contains(item.Name)) {
                        return item;
                    }
                }
            }
             
            return null;
        }


        /*
         * getItemFromInventory(String input, Player player)
         * Get the item from the list of items in the inventory
         */
        private Item getItemFromInventory(string input, Player player) { 

            // check if the inventory of the player is not empty
            if (player.Inventory.Count != 0)
            {
                foreach (Item item in player.Inventory.ToList())
                {
                    if (input.Contains(item.Name))
                    {
                        return item;
                    } 
                }
            }
            return null;

        }

        /*
         * addItemToInventory(Player player, Item itemPicked)
         * Add an item to the inventory of the player
         */
        private void addItemToInventory(Player player, Item itemPicked) {

            // add the item to the inventory
            player.Inventory.Add(itemPicked);
            // remove the item to the current room
            currentRoom.Items.Remove(itemPicked);
            // add points value to the player score
            player.score += itemPicked.PointValue;
        }

        /*
         * removeItemFromInventory(Player player, Item itemDropped)
         * Remove an item from the inventory of the player 
         * Add it to the item list of the current room
         */
        private void removeItemFromInventory(Player player, Item itemDropped)  {

            // remove the item from the inventory of the player
            player.Inventory.Remove(itemDropped);
            // add the item to the current room
            currentRoom.Items.Add(itemDropped);
            // remove points value to the player score
            player.score -= itemDropped.PointValue;
        }

        /*
         * describeItem(string input, Player player)
         * Describe the item present in the inventory / or in the current room
         */
        private Item describeItem(string input, Player player) { 

            if (player.Inventory.Count != 0 || currentRoom.Items.Count != 0) {
                foreach (Item itemInvent in player.Inventory.ToList()) {
                    if (input.Contains(itemInvent.Name)) {
                        return itemInvent;
                    }
                }
                foreach (Item itemRoom in currentRoom.Items.ToList())
                {
                    if (input.Contains(itemRoom.Name))
                    {
                        return itemRoom;
                    } 
                }
            }
            return null;
        }


        /*
         * showInventory(Player player)
         * BONUS : show the inventory of the player and see the current items he picked
         */
        private void showInventory(Player player) {

            if (player.Inventory.Count != 0) {
                foreach (Item item in player.Inventory) {
                    Console.WriteLine(item);
                }
            }
            else {
                Console.WriteLine(AdventureGameConstants.MESSAGE_EMPTY_INVENTORY);
            }
        }

        private void moveEnemy() {

            // 1- init random
            Random random = new Random(); 
            int nb = random.Next(roomslist.Count);

            // 2- remove enemy from the room
            Item tromp = new Enemy();
            foreach (Room enemyRoom in roomslist) {
                foreach(Item enemy in enemyRoom.Items.ToList()) {
                    if(enemy.Name == AdventureGameConstants.ENEMY_TROMP) {
                        tromp = enemy;
                        enemyRoom.Items.Remove(enemy);
                    }
                }
            }

            // 3- move the enemy to another random room
            Room randomRoom = roomslist[nb];
            randomRoom.Items.Add(tromp);

            // 4- display message
            Console.WriteLine(AdventureGameConstants.MESSAGE_ROOM_ENEMY + randomRoom.RoomName);
        }

    }
}

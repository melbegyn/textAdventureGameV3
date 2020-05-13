using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace textAdventureGameV3
{

    public static class LinqHelper {
        public static void RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey) {
            TValue value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }
    }

    public class AdventureGame {

        public Room currentRoom;
        public List<Enemy> enemiesCurrentRoom;
        public Room trompRoom; 

        public List<Room> roomslist = new List<Room>();
        public int moves = 0; 

        public void StartGame(Player player) {
            Console.WriteLine(AdventureGameConstants.INTRO);
            InputLoop(player);
        }

        public void InputLoop(Player player) {

            while (true) {

                // display room information
                currentRoom.PrintRoom();
                 
                // display message if Tromp is in current room
                if(trompRoom == currentRoom) {
                     Console.WriteLine(AdventureGameConstants.MESSAGE_ENEMY_SAME_ROOM);
                } 
                 
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

                    // CHECK 1: if the secret door is opened 
                    bool secretDoorOpened = openSecretDoor(input);
                    if (secretDoorOpened) {
                        Item elvenBook = getItemFromRoom(input);
                        addItemToInventory(player, elvenBook);
                        LinqHelper.RenameKey(currentRoom.Transitions, AdventureGameConstants.ACCESS_SECRET_DOOR, AdventureGameConstants.NORTH);
                    }

                    // CHECK 2: if the player change the room without attacking the enemy
                    bool isEndGame = isPlayerFledEnemy(player);
                    if (isEndGame) {
                        break;
                    }

                    // remove the destroyed enemy from items list of the current room
                    foreach (Enemy enemy in currentRoom.Items.ToList().OfType<Enemy>()) {
                        if (enemy.IsDestroyed) {
                            currentRoom.Items.Remove(enemy);
                        } 
                    }

                    // ********************* UPDATE THE ROOM *********************
                    currentRoom = nextRoom;
                    // ***********************************************************

                    // CHECK 3: if the Tromp is alive and the number of moves is even, the enemy is moved 
                    moveEnemy();

                    // Increment number of moves 
                    moves++;
                }

                /* ACTION 2 - PICK UP: add an item to their inventory by typing the command */
                else if (input.StartsWith(AdventureGameConstants.ACTION_PICK_UP))  {

                    Item itemToPick = getItemFromRoom(input);
                    if (itemToPick != null) {
                        addItemToInventory(player, itemToPick);
                    }
                    else {
                        Console.WriteLine(AdventureGameConstants.MESSAGE_ITEM_NOT_IN_ROOM);
                    }
                }

                /* ACTION 3 - DROP: remove an item from his inventory and leaves it in the current room */
                else if (input.StartsWith(AdventureGameConstants.ACTION_DROP)) {

                    Item itemToDrop = getItemFromInventory(input, player);
                    if (itemToDrop != null) {
                        removeItemFromInventory(player, itemToDrop);
                    }
                    else {
                        Console.WriteLine(AdventureGameConstants.MESSAGE_ITEM_NOT_IN_INVENTORY);
                    }
                }

                /* ACTION 4 - DESCRIBE: describe an item in inventory or in the same room */
                else if (input.StartsWith(AdventureGameConstants.ACTION_DESCRIBE))  {

                    Item itemToDescribe = describeItem(input, player);

                    if (itemToDescribe != null) {
                        Console.WriteLine(itemToDescribe.Description);
                    }
                    else {
                        Console.WriteLine(AdventureGameConstants.MESSAGE_ITEM_NOT_INVENT_AND_ROOM);
                    }
                }

                /* ACTION 5 - SHOW INVENTORY: the player can ask to see the items stores in his inventory */
                else if (input.StartsWith(AdventureGameConstants.ACTION_SHOW_INVENTORY))  {
                    showInventory(player);
                }

                /* ACTION 6 - ATTACK ENEMY WITH WEAPON: the player can attack the enemy in the room with a weapon */
                else if (input.StartsWith(AdventureGameConstants.ACTION_ATTACK_ENEMY) && input.Contains("with")) {

                    // Check if the enemy is in the current room and if the player has the weapon in his inventory
                    Weapon weapon = getWeaponFromInventory(input, player);
                    if (weapon != null) {
                        attackEnemy(player, input, weapon);
                    }
                    else {
                        Console.WriteLine(AdventureGameConstants.MESSAGE_INVALID_ATTACK);
                    }

                }

                // ELSE : If the action entered by the player isn't correct / doesn't exist
                else {
                    Console.WriteLine(AdventureGameConstants.MESSAGE_INVALID_ACTION);
                }

                // The room is the final room to the end = finish the game
                if (currentRoom.FinalRoom) {

                    /***************/
                    /** GAME OVER **/
                    /***************/
                    currentRoom.PrintRoom();
                    PrintScore(player);
                    break;
                } 
            }
        }

        /*
         * isPlayerFledEnemy(Player player)
         * check if the player left the room without attacking the enemy
         * if TRUE = end of the game.
         */
        private bool isPlayerFledEnemy(Player player) {

            if (currentRoom.Items.ToList().OfType<Enemy>().Any()) {
                foreach (Enemy enemy in currentRoom.Items.ToList().OfType<Enemy>()) { 
                    if (!enemy.IsDestroyed) {

                        Console.WriteLine("The " + enemy.Name + " attacks and slays you!");

                        /***************/
                        /** GAME OVER **/
                        /***************/
                        player.score = 0;
                        PrintScore(player);
                        return true;
                    }
                }
            }
            return false;
        }

        /*
         * attackEnemy(Player player, string input, Weapon weapon)
         * attack the enemy with the weapon, 
         */
        private void attackEnemy(Player player, string input, Weapon weapon)  {
            foreach (Enemy enemy in currentRoom.Items.ToList().OfType<Enemy>()) {
                if (input.Contains(enemy.Name)) {

                    weapon.attackEnemy(enemy);
                     
                    if (currentRoom == trompRoom) {
                        player.score += enemy.PointValue;
                        trompRoom = null;
                    }
                    else {
                        createNewItem(currentRoom);
                    }
                }
            }
        }

        /** 
         * createNewItem(Room currentRoom)
         * Create a new item and add it to the room where the Enemy is killed
         */
        private void createNewItem(Room currentRoom) {
            Item gold = new Item();
            gold.Name = "gold";
            gold.Description = "Shiny gold coins.";
            gold.PointValue = 100;

            currentRoom.Items.Add(gold);
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
            if (player.Inventory.Count != 0) {
                foreach (Item item in player.Inventory.ToList()) {
                    if (input.Contains(item.Name)) {
                        return item;
                    } 
                }
            }
            return null;
        }

        /*
         * getWeaponFromInventory(String input, Player player)
         * Get the weapon from the list of items in the inventory
         */
        private Weapon getWeaponFromInventory(string input, Player player) { 
            // check if the inventory of the player is not empty
            if (player.Inventory.Count != 0) {
                foreach (Item weapon in player.Inventory.ToList()) {
                    if (input.Contains(weapon.Name)) {
                        return (Weapon) weapon;
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
                foreach (Item itemRoom in currentRoom.Items.ToList()) {
                    if (input.Contains(itemRoom.Name)) {
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

        /*
         * moveEnemy()
         * Choose a random room to move the Tromp enemy 
         */
        private void moveEnemy() {

            if (moves % 2 == 0 && trompRoom != null) {

                // 1- init random
                Random random = new Random();
                int nb = random.Next(roomslist.Count);

                // 2- remove enemy from the room
                Item tromp = getTrompFromRoom();

                // 3- move the enemy to another random room
                trompRoom = updateTrompRoom(tromp, nb);

                // 4- display message
                Console.WriteLine(AdventureGameConstants.MESSAGE_ENEMY_ROOM + trompRoom.RoomName);
            }
        }

        private Room updateTrompRoom(Item tromp, int nb) {

            Room enemyRoom = new Room();

            if (tromp != null) { 
                trompRoom = roomslist[nb];
                trompRoom.Items.Add(tromp);
                enemyRoom = trompRoom;
            }
            return enemyRoom;
        }

        private Item getTrompFromRoom() {
            Item tromp = new Enemy();
            foreach (Room enemyInRoom in roomslist) {
                foreach (Item enemy in enemyInRoom.Items.ToList()) {
                    if (enemy.Name == AdventureGameConstants.ENEMY_TROMP) {
                        tromp = enemy;
                        trompRoom.Items.Remove(enemy);
                    }
                }
            } 
            return tromp;
        }
    }
}

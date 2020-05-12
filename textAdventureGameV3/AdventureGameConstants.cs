using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    class AdventureGameConstants
    {
        public const string INTRO = "Weeks ago, you received a mysterious letter claiming that your late grandfather (who you don't know anything about) left you his house and land in the mountains. Having no property yourself, this is a substantial inheritance. After a few days of hiking into the countryside, you come upon the house, opulent and imperial, standing proudly against the hills leading into the mountain behind it.";

        /** directions **/
        public const string NORTH = "north";
        public const string SOUTH = "south";
        public const string EAST  = "east";
        public const string WEST  = "west";
        public const string UP    = "up";
        public const string DOWN  = "down"; 

        public const string ACCESS_SECRET_DOOR = "pick up elven book";

        /** actions **/
        public const string ACTION_QUIT = "quit";
        public const string ACTION_PICK_UP = "pick up";
        public const string ACTION_DROP = "drop";
        public const string ACTION_DESCRIBE = "describe";
        public const string ACTION_SHOW_INVENTORY = "show inventory"; 
        public const string ACTION_ATTACK_ENEMY = "attack";  

        /** enemies **/
        public const string ENEMY_HARPSICHORD = "harpsichord";
        public const string ENEMY_TROMP = "Tromp";

        /** attacks **/
        public static readonly Dictionary<string, string> ATTACK_MESSAGES = new Dictionary<string, string> {
            {"strike", "is struck with"}, 
            {"destroy", "was destroyed by"}
        }; 

        /** messages **/
        public const string MESSAGE_ITEM_NOT_IN_INVENTORY = "Sorry, you don't have that item in your inventory.";
        public const string MESSAGE_ITEM_NOT_IN_ROOM = "Sorry, this room doesn't have that item, or doesn't exist.";
        public const string MESSAGE_ITEM_NOT_INVENT_AND_ROOM = "Sorry, this item isn't in this room, in your inventory neither.";
        
        public const string MESSAGE_EMPTY_INVENTORY = "Your inventory is empty.";

        public const string MESSAGE_ENEMY_ROOM = ENEMY_TROMP + " is now in ";
        public const string MESSAGE_ENEMY_SAME_ROOM = ENEMY_TROMP + " is in the room.";

        public const string MESSAGE_INVALID_ATTACK = "Sorry, you cannot attack this enemy or use this weapon.";
        public const string MESSAGE_INVALID_ACTION = "Invalid Action.";
        public const string MESSAGE_QUIT_GAME = "Thank you playing with game with us.";
         

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    class Program
    {

        static void Main(string[] args) { 
            AdventureGame game = new AdventureGameInit();

            Player player = new Player();
            game.StartGame(player);
 
        }
    }
}

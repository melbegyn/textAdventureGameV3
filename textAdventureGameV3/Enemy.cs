using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    class Enemy : Item, IAttackable
    {
        // Enemies: Tromp and Hapsichord 
        public bool _isDestroyed;

        public bool IsDestroyed  // read-write instance property
        {
            get => _isDestroyed;
            set => _isDestroyed = value;
        }

        public Enemy()
        {
            IsDestroyed = false;
        }
 
        public void printEnemy() {
            if(IsDestroyed) {
                // for a Tromp
                //"Troll is struck with elven sword!"
                Console.WriteLine("Tromp lies destroyed here.");
                // for a harpsichord
                //"harpsichord was destroyed by elven sword!"
                Console.WriteLine("A destroyed harpsichord, worth 0.");
            }
            // if finally the player hasn't destroyed the enemy and left 
            else {
                // for a Tromp
                Console.WriteLine("The Troll attacks and slays you!");
                // for a harpsichord
                Console.WriteLine("The Harpsichord attacks and slays you!");
            }
        }
    }
}

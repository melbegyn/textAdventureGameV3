using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    class Weapon : Item, IWeapon
    {
        // Elven Sword weapon
        public void printWeapon(Enemy enemy) {

            // lui cest tromp
            Console.WriteLine(enemy.Name + " is struck with elven sword!");
             
            // lui cest harpsichord
            //Console.WriteLine("harpsichord was destroyed by elven sword!");
        }
    }
}

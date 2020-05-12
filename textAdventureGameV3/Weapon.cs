using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    class Weapon : Item, IWeapon
    {
        // Weapon: Elven Sword

        public void attackEnemy(Enemy enemy) {

            enemy.IsDestroyed = true; 
            Console.WriteLine("The " + enemy.Name 
                              + " " + AdventureGameConstants.ATTACK_MESSAGES[enemy.TypeOfAttack]
                              + " " + Name + "!"); 
        } 
    }
}

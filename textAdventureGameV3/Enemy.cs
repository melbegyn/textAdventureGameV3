using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    class Enemy : Item, IAttackable
    {
        // Enemies: Tromp and Hapsichord 

        public bool IsDestroyed { get; set; }
        public string TypeOfAttack { get; set; } 
        public string LostBattleMessage { get; set; }

        public Enemy() {
            IsDestroyed = false;
        }


        public void printEnemy() {
            // if the player has killed the enemy
            if(IsDestroyed) {
                Console.WriteLine(LostBattleMessage);
            }
            // if finally the player hasn't attacked the enemy and left 
            else {
                Console.WriteLine("The " + Name + " attacks and slays you!");
            }
        }

        public override string ToString() {
            return $"{Name}: ({PointValue} gold) {Description}";
        }

    }
}

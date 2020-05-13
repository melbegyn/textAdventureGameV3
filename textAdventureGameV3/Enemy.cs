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

        public override string ToString() {
            return $"{Name}: ({PointValue} gold) {Description}";
        }

    }
}

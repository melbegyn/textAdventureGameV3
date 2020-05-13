using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    public class Enemy : Item, IAttackable
    {
        // Enemies: Tromp and Hapsichord 

        public bool IsDestroyed { get; set; }
        public string TypeOfAttack { get; set; } 
        public string LostBattleMessage { get; set; }

        public Enemy() {
            IsDestroyed = false;
        }

        public override string ToString() {
            if(IsDestroyed) {
                return LostBattleMessage;
            }
            else {
                return $"{Name}: ({PointValue} gold) {Description}";
            }
        }
    }
}

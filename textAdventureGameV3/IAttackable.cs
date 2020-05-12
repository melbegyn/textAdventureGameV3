using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    interface IAttackable
    {
         bool IsDestroyed { get; set; }
         string TypeOfAttack { get; set; } 
         string LostBattleMessage { get; set; }
         
         void printEnemy();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace textAdventureGameV3
{
    class Enemy : Item, IAttackable
    {
        // Tromp and Hapsichord enemies

        bool IAttackable.IsDestroyed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

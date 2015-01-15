using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTackToe
{


    abstract class Player : IEquatable<Player>
    {
        abstract public Enums.PlaySymbol symbol {get;}



        public bool Equals(Player other)
        {
            return this.symbol.Equals(other.symbol);
        }
    }

    class TickPlayer : Player
    {
        public override Enums.PlaySymbol symbol
        {
            get { return Enums.PlaySymbol.TICK; }
        }
    }

    class CrossPlayer : Player
    {
        public override Enums.PlaySymbol symbol 
        {   
            get{ return Enums.PlaySymbol.CROSS;}   
        }
    }
}

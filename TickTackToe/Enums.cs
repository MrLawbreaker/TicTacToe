using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTackToe
{
    class Enums
    {
        public enum PlaySymbol { EMPTY, CROSS, TICK };

        public static String PlaySymbolToString(PlaySymbol p)
        {
            switch (p)
            {
                case PlaySymbol.CROSS:
                    return "X";
                case PlaySymbol.TICK:
                    return "O";
                default:
                    return " ";
            }
        }

        public enum GameState { DRAW, WIN, IN_PROGRESS };
    }
}

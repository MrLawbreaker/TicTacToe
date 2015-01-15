using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTackToe
{
    class PlayField
    {
        private Enums.PlaySymbol[,] field;
        private const int fieldSize = 3;
        private int movesDone = 0;
        private Enums.GameState gState = Enums.GameState.IN_PROGRESS;
        public Enums.GameState gameState { get { return gState; } }

        private int[] lastSetField = {-1,-1};

        public PlayField()
        {
            this.field = new Enums.PlaySymbol[3, 3];
            ResetField();
        }

        /// <summary>
        /// Tries to set the given field will return false when the move was illegal
        /// </summary>
        /// <param name="x">X coordinate of the move</param>
        /// <param name="y">Y coordinate of the move</param>
        /// <param name="value">PlaySymbol that is going to be set into the given field</param>
        /// <returns></returns>
        public Boolean SetField(byte x, byte y, Enums.PlaySymbol value)
        {
            Boolean isEmpty = this.field[x, y].Equals(Enums.PlaySymbol.EMPTY);

            if (!isEmpty)
                return false;
            else
            {
                this.field[x, y] = value;
                this.movesDone++;
                this.lastSetField[0] = x;
                this.lastSetField[1] = y;
                this.CheckGameOver();
                return true;
            }
        }

        /// <summary>
        /// Returns the playfield
        /// </summary>
        /// <returns></returns>
        public Enums.PlaySymbol[,] GetField()
        {
            return field;
        }

        /// <summary>
        /// Resets the playfield 
        /// All fields will have the EMPTY value
        /// </summary>
        public void ResetField()
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    this.field[i, j] = Enums.PlaySymbol.EMPTY;
                }
            }
            gState = Enums.GameState.IN_PROGRESS;
            this.movesDone = 0;
            lastSetField[0] = -1;
            lastSetField[1] = -1;
        }

        /// <summary>
        /// Checks if the last made move brought the playfield into a winning or ending state
        /// </summary>
        private void CheckGameOver()
        {
            //Minimum number of moves for a win needs to be reached
            if (this.movesDone < (2 * fieldSize) - 1)
                return;


            Enums.PlaySymbol lastSymbol = field[lastSetField[0],lastSetField[1]];
            //Check the row for a possible win
            for (int i = 0; i < fieldSize; i++)
            {
                if (!field[i, lastSetField[1]].Equals(lastSymbol))
                {
                    break;
                }
                else if (i == fieldSize - 1)
                {
                    gState = Enums.GameState.WIN;
                    return;
                }
            }

            //Check the column for a possible win
            for (int i = 0; i < fieldSize; i++)
            {
                if (!field[lastSetField[0], i].Equals(lastSymbol))
                {
                    Console.WriteLine("no win column");
                    break;
                }
                else if (i == fieldSize - 1)
                {
                    gState = Enums.GameState.WIN;
                    return;
                }
            }

            //Check diagonal for a possible win
            //First diagonal does not need to be checked when the last move
            //was not on the main diagonal (Top left to bottom right)
            if (lastSetField[0] == lastSetField[1])
            {
                for (int i = 0; i < fieldSize; i++)
                {
                    if (!field[i, i].Equals(lastSymbol))
                        break;
                    else if (i == fieldSize - 1)
                    {
                        gState = Enums.GameState.WIN;
                        return;
                    }

                }
            }

            //Check from top right to bottom left
            int j = fieldSize-1;
            for (int i = 0; i < fieldSize; i++)
            {
                if (!field[j--, i].Equals(lastSymbol))
                    break;
                else if (i == fieldSize -1)
                {
                    gState = Enums.GameState.WIN;
                    return;
                }
            }
                            

            //All possible moves are done and the game is a draw
            if (this.movesDone >= fieldSize * fieldSize)
                this.gState = Enums.GameState.DRAW;

        }

        /// <summary>
        /// Represents the playfield as a String
        /// </summary>
        /// <returns>Returns a String with fieldSize number of lines</returns>
        public override String ToString()
        {
            String result = "";

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    result += Enums.PlaySymbolToString(field[j,i]);
                }
                result += "\n";
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace othello.ia.MinMaxAlphaBeta
{
    public class State
    {
        public int[,] Board;
        public int[,] UndoBoard;

        public State(int[,] b)
        {
            Board = b;
            UndoBoard = (int[,])b.Clone();
        }
    }
}
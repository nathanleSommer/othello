using System;
using System.Collections.Generic;
using System.Text;

namespace othello.ia.MinMaxAlphaBeta
{
    public class MinMax
    {
        int[,] board;

        public MinMax(int[,] b)
        {
            board = b;
        }

        public KeyValuePair<int,int> PlayMove(int depth)
        {
            int max = -10000;
            int tmp, maxi = 0, maxj = 0;

            List<KeyValuePair<int, int>> validPos = SelectPossibleMoves(board);
            foreach (KeyValuePair<int,int> move in validPos)
            {
                board[move.Key, move.Value] = 2;
                tmp = Min(board, depth - 1);

                if(tmp > max)
                {
                    max = tmp;
                    maxi = move.Key;
                    maxj = move.Value;
                }
                board[move.Key, move.Value] = 0;
            }
            
            return new KeyValuePair<int,int>(maxi,maxj);
        }

        private int Min(int[,] board, int depth)
        {
            if (depth == 0 || SelectPossibleMoves(board).Count == 0)
            {
                return Eval(board);
            }

            int min = 10000;
            int tmp = 0;

            List<KeyValuePair<int, int>> validPos = SelectPossibleMoves(board);
            foreach (KeyValuePair<int, int> move in validPos)
            {
                board[move.Key, move.Value] = 1;
                tmp = Max(board, depth - 1);

                if(tmp < min)
                {
                    min = tmp;
                }
                board[move.Key, move.Value] = 0;
            }
            return min;
        }

        private int Max(int[,] board, int depth)
        {
            if (depth == 0 || SelectPossibleMoves(board).Count == 0)
            {
                return Eval(board);
            }

            int max = -10000;
            int tmp = 0;

            List<KeyValuePair<int, int>> validPos = SelectPossibleMoves(board);
            foreach (KeyValuePair<int, int> move in validPos)
            {
                board[move.Key, move.Value] = 2;
                tmp = Max(board, depth - 1);

                if (tmp > max)
                {
                    max = tmp;
                }
                board[move.Key, move.Value] = 0;
            }
            return max;
        }

        private int Eval(int[,] board)
        {
            return 0;
        }


        private int[,] SimulateMove(KeyValuePair<int,int> move, int[,] oldBoard, int player)
        {
            int[,] newBoard = (int[,])oldBoard.Clone();
            newBoard[move.Key, move.Value] = player;

            return newBoard;
        }

        private List<KeyValuePair<int,int>> SelectPossibleMoves(int[,] board)
        {
            List<KeyValuePair<int, int>> validPos = new List<KeyValuePair<int, int>>();
            int size = (int)Math.Sqrt(board.Length);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (IsValidPosition(board, i, j, size))
                    {
                        validPos.Add(new KeyValuePair<int, int>(i, j));
                    }
                }
            }
            return validPos;
        }

        private bool IsValidPosition(int[,] tiles, int x, int y, int size)
        {
            if (tiles[x, y] != 0) return false;
            if (x != 0 && tiles[x - 1, y] != 0) return true;
            if (x != size - 1 && tiles[x + 1, y] != 0) return true;
            if (y != 0 && tiles[x, y - 1] != 0) return true;
            if (y != size - 1 && tiles[x, y + 1] != 0) return true;
            return false;
        }
    }
}

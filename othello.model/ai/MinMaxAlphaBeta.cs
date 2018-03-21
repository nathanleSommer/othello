using othello.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace othello.ia.MinMaxAlphaBeta
{
    public class MinMaxAlphaBeta
    {
        int size;
        Board Game;
        int[,] board;
        int alpha;
        int beta;

        public MinMaxAlphaBeta(Board g, int al, int be)
        {
            alpha = al;
            beta = be;

            size = (int)Math.Sqrt(g.Tiles.Length);
            
            board = new int[size,size];

            for(int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = g.Tiles[i, j];
                }
            }

            Game = new Board(size);
            Game.Tiles = board;
        }

        public KeyValuePair<int, int> PlayMove(int depth)
        {
            int min = 10000;
            int tmp;
            int maxi = 0, maxj = 0;

            List<KeyValuePair<int, int>> validPos = SelectPossibleMoves(Game.Tiles);
            foreach (KeyValuePair<int,int> move in validPos)
            {
                int[,] old = new int[size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        old[i,j] = Game.Tiles[i, j];
                    }
                }

                //simulate move AI
                Game.Tiles[move.Key, move.Value] = 2;
                Game.returnTiles(2, move.Key, move.Value);
                Console.WriteLine(Game.ToString());
                tmp = Min(depth - 1);
                Console.WriteLine(tmp);


                if (tmp < min)
                {
                    min = tmp;
                    maxi = move.Key;
                    maxj = move.Value;
                }
                Game.Tiles = old;
            }
            
            return new KeyValuePair<int,int>(maxi,maxj);
        }

        private int Min(int depth)
        {
            if (depth == 0 || SelectPossibleMoves(Game.Tiles).Count == 0)
            {
                return Eval(Game.Tiles, 1);
            }

            int min = 10000;
            int tmp = 0;

            List<KeyValuePair<int, int>> validPos = SelectPossibleMoves(Game.Tiles);
            foreach (KeyValuePair<int, int> move in validPos)
            {
                int[,] old = new int[size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        old[i, j] = Game.Tiles[i, j];
                    }
                }

                //simulate move human
                Game.Tiles[move.Key, move.Value] = 1;
                Game.returnTiles(1, move.Key, move.Value);

                tmp = Max(depth - 1);

                if(tmp < min)
                {
                    min = tmp;
                }
                Game.Tiles = old;
            }
            return min;
        }

        private int Max(int depth)
        {
            if (depth == 0 || SelectPossibleMoves(Game.Tiles).Count == 0)
            {
                return Eval(Game.Tiles, 2);
            }

            int max = -10000;
            int tmp = 0;

            List<KeyValuePair<int, int>> validPos = SelectPossibleMoves(Game.Tiles);
            foreach (KeyValuePair<int, int> move in validPos)
            {
                int[,] old = new int[size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        old[i, j] = Game.Tiles[i, j];
                    }
                }



                //simulate move human
                Game.Tiles[move.Key, move.Value] = 2;
                Game.returnTiles(2, move.Key, move.Value);

                tmp = Min(depth - 1);

                if (tmp > max)
                {
                    max = tmp;
                }
                Game.Tiles = old;
            }
            return max;
        }

        private int Eval(int[,] _board, int player)
        {
            int score = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (_board[i, j] == player) score++;
                    else if (_board[i, j] != 0) score--;
                }
            }
            return score;
        }

        private List<KeyValuePair<int,int>> SelectPossibleMoves(int[,] board)
        {
            List<KeyValuePair<int, int>> validPos = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (Game.IsValidPosition(2, i, j))
                    {
                        validPos.Add(new KeyValuePair<int, int>(i, j));
                    }
                }
            }
            return validPos;
        }
    }
}

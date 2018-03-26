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

        public MinMaxAlphaBeta(Board g)
        {
            size = (int)Math.Sqrt(g.Tiles.Length);
            
            board = new int[size,size];

            for(int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = g.Tiles[i, j];
                }
            }

            Game = new Board(size)
            {
                Tiles = board
            };
        }

        public KeyValuePair<int, int> PlayMove(int depth)
        {
            int min = -10000;
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
                tmp = Min(depth - 1, -10000, 10000);
                Console.WriteLine(Game.ToString());
                Console.WriteLine(tmp);

                if (tmp > min)
                {
                    min = tmp;
                    maxi = move.Key;
                    maxj = move.Value;
                }
                Game.Tiles = old;
            }
            
            return new KeyValuePair<int,int>(maxi,maxj);
        }

        private int Min(int depth, int a, int b)
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

                tmp = Max(depth - 1, a, b);
                if(tmp <= a)
                {
                    return tmp;
                }

                if(tmp < min)
                {
                    min = tmp;
                    b = tmp;
                }
                Game.Tiles = old;
            }
            return min;
        }

        private int Max(int depth, int a, int b)
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

                //simulate move AI
                Game.Tiles[move.Key, move.Value] = 2;
                Game.returnTiles(2, move.Key, move.Value);

                tmp = Min(depth - 1, a, b);

                if(tmp >= b)
                {
                    return tmp;
                }

                if (tmp > max)
                {
                    max = tmp;
                    a = tmp;
                }
                Game.Tiles = old;
            }
            return max;
        }

        private int Eval(int[,] _board, int player)
        {
            int score = 0;
            //CORNER = GOOOOOOD
            if (_board[0, 0] == 2) score += 10; else if (_board[0, 0] == 1) score -= 10;
            if (_board[0, size - 1] == 2) score += 10; else if (_board[0, size - 1] == 1) score -= 10;
            if (_board[size - 1, 0] == 2) score += 10; else if (_board[size - 1, 0] == 1) score -= 10;
            if (_board[size - 1, size - 1] == 2) score += 100; else if (_board[size - 1, size - 1] == 1) score -= 10;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (_board[i, j] == 2) score++;
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

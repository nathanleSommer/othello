﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using othello.ia;
using othello.ia.MinMaxAlphaBeta;

namespace othello.model
{
    public class Board
    {
        public int BoardSize;
        public int[,] Tiles { get; set; }    
        public Board(int size)
        {
            BoardSize = size;
            Tiles = new int[BoardSize, BoardSize];
            for(int i = 0; i < BoardSize; i++)
            {
                for(int j = 0; j < BoardSize; j++)
                {
                    Tiles[i, j] = 0;
                }
            }
        }

        public void PlayMovePlayer(int x, int y)
        {
            Tiles[x, y] = 1;

            /* RETOURNER LES DISCS du joueur 1 (human) */
            returnTiles(1, x, y);
        }

        public KeyValuePair<int,int> PlayMoveAI()
        {
            MinMaxAlphaBeta ai = new MinMaxAlphaBeta(this);

            KeyValuePair<int, int> pos = ai.PlayMove(5);

            int x = pos.Key;
            int y = pos.Value;
            Tiles[x, y] = 2;

            /* RETOURNER LES DISCS du joueur 2 (ai) */
            returnTiles(2, x, y);
            return pos;
        }

        #region ValidPosition
        public bool IsValidPosition(int player, int x, int y)
        {
            if (Tiles[x, y] != 0) return false;
            List<KeyValuePair<int, int>> neighbors = GetNeighbors(player, x, y);
            if (neighbors.Count == 0) return false;
            return true;
        }

        private List<KeyValuePair<int,int>> GetNeighbors(int player, int x, int y)
        {
            List<KeyValuePair<int, int>> neighbors = new List<KeyValuePair<int, int>>();
            if (y > 0)
            {
                if (Tiles[x, y - 1] != 0 && Tiles[x, y - 1] != player)
                {
                    for(int i = y - 2; i >= 0; i--)
                    {
                        if(Tiles[x,i] == player)
                        {
                            neighbors.Add(new KeyValuePair<int, int>(x, y - 1));
                            break;
                        }
                    }
                }        
            }
            if(y < BoardSize - 1)
            {
                if (Tiles[x, y + 1] != 0 && Tiles[x, y + 1] != player)
                {
                    for(int i = y + 2; i < BoardSize; i++)
                    {
                        if(Tiles[x,i] == player)
                        {
                            neighbors.Add(new KeyValuePair<int, int>(x, y + 1));
                            break;
                        }
                    }
                }
            }
            if(x > 0)
            {
                if(y > 0)
                {
                    if (Tiles[x - 1, y - 1] != 0 && Tiles[x - 1, y - 1] != player)
                    {
                        int i = x - 2, j = y - 2;
                        while (i >= 0 && j >= 0)
                        {
                            if (Tiles[i, j] == player)
                            {
                                neighbors.Add(new KeyValuePair<int, int>(x - 1, y - 1));
                                break;
                            }
                            i--;
                            j--;
                        }
                    }    
                } 
                if( y < BoardSize - 1)
                {
                    if (Tiles[x - 1, y + 1] != 0 && Tiles[x - 1, y + 1] != player)
                    {
                        int i = x - 2, j = y + 2;
                        while (i >= 0 && j < BoardSize)
                        {
                            if (Tiles[i, j] == player)
                            {
                                neighbors.Add(new KeyValuePair<int, int>(x - 1, y + 1));
                                break;
                            }
                            i--;
                            j++;
                        }
                    }     
                }

                if (Tiles[x - 1, y] != 0 && Tiles[x - 1, y] != player)
                {
                    for (int i = x - 2; i >= 0; i--)
                    {
                        if (Tiles[i, y] == player)
                        {
                            neighbors.Add(new KeyValuePair<int, int>(x - 1, y));
                            break;
                        }
                    }
                }
                
            }
            if (x < BoardSize - 1)
            {
                if (y > 0)
                {
                    if (Tiles[x + 1, y - 1] != 0 && Tiles[x + 1, y - 1] != player)
                    {
                        int i = x + 2, j = y - 2;
                        while (i < BoardSize && j >= 0)
                        {
                            if (Tiles[i, j] == player)
                            {
                                neighbors.Add(new KeyValuePair<int, int>(x + 1, y - 1));
                                break;
                            }
                            i++;
                            j--;
                        }
                    }
                }
                if (y < BoardSize - 1)
                {
                    if (Tiles[x + 1, y + 1] != 0 && Tiles[x + 1, y + 1] != player)
                    {
                        int i = x + 2, j = y + 2;
                        while (i < BoardSize && j < BoardSize)
                        {
                            if (Tiles[i, j] == player)
                            {
                                neighbors.Add(new KeyValuePair<int, int>(x + 1, y + 1));
                                break;
                            }
                            i++;
                            j++;
                        }
                    }
                }

                if (Tiles[x + 1, y] != 0 && Tiles[x + 1, y] != player)
                {
                    for (int i = x + 2; i < BoardSize; i++)
                    {
                        if (Tiles[i, y] == player)
                        {
                            neighbors.Add(new KeyValuePair<int, int>(x + 1, y));
                            break;
                        }
                    }
                }
            }

            return neighbors;
        }
        #endregion

        #region ReturnTiles
        public void returnTiles(int player, int x, int y)
        {
            top(player, x, y);
            right(player, x, y);
            down(player, x, y);
            left(player, x, y);
            diagTopLeft(player, x, y);
            diagDownLeft(player, x, y);
            diagDownRight(player, x, y);
            diagTopRight(player, x, y);
        }

        private void diagTopLeft(int player, int x, int y)
        {
            if (y <= 1 || x <= 1 || Tiles[x - 1, y - 1] == 0 || Tiles[x - 1, y - 1] == player) return;

            //on trouve une piece adverse à la diagonale haut-gauche de la piece qu'on vient de placer
            int i = x - 1;
            int j = y - 1;
            while (Tiles[i, j] != player && Tiles[i, j] != 0 && i != 0 && j != 0)
            {
                i--;
                j--;
            }

            if (Tiles[i, j] == player)
            {
                while (i != x && j != y)
                {
                    Tiles[i, j] = player;
                    i++;
                    j++;
                }
            }
        }
        private void diagTopRight(int player, int x, int y)
        {
            if (x <= 1 || y >= BoardSize - 2 || Tiles[x - 1, y + 1] == player || Tiles[x - 1, y + 1] == 0) return;

            int i = x - 1;
            int j = y + 1;
            while (Tiles[i, j] != player && Tiles[i, j] != 0 && i != 0 && j != BoardSize - 1)
            {
                i--;
                j++;
            }

            if (Tiles[i, j] == player)
            {
                while (i != x)
                {
                    i++;
                    j--;
                    Tiles[i, j] = player;
                }
            }
        }
        private void diagDownRight(int player, int x, int y)
        {
            if (y >= BoardSize - 2 || x >= BoardSize - 2 || Tiles[x + 1, y + 1] == 0 || Tiles[x + 1, y + 1] == player) return;

            //on trouve une piece adverse à la diagonale bas-droite de la piece qu'on vient de placer
            int i = x + 1;
            int j = y + 1;
            while (Tiles[i, j] != player && Tiles[i, j] != 0 && i != BoardSize - 1 && j != BoardSize - 1)
            {
                i++;
                j++;
            }

            if (Tiles[i, j] == player)
            {
                while (i != x && j != y)
                {
                    Tiles[i, j] = player;
                    i--;
                    j--;
                }
            }
        }
        private void diagDownLeft(int player, int x, int y)
        {
            if (x >= BoardSize - 2 || y <= 1 || Tiles[x + 1, y - 1] == player || Tiles[x + 1, y - 1] == 0) return;

            int i = x + 1;
            int j = y - 1;
            while (Tiles[i, j] != player && Tiles[i, j] != 0 && i != BoardSize - 1 && j != 0)
            {
                i++;
                j--;
            }

            if (Tiles[i, j] == player)
            {
                while (i != x)
                {
                    i--;
                    j++;
                    Tiles[i, j] = player;
                }
            }
        }
        private void left(int player, int x, int y)
        {
            if (y == 0 || Tiles[x, y - 1] == 0 || Tiles[x, y - 1] == player) return;

            //on trouve une piece adverse à la gauche de la piece qu'on vient de placer
            int i = y - 1;
            while (Tiles[x, i] != player && Tiles[x, i] != 0 && i != 0)
            {
                i--;
            }

            if (Tiles[x, i] == player)
            {
                for(int j = y-1; j > i; j--)
                {
                    Tiles[x, j] = player;
                }
            }          
        }
        private void top(int player, int x, int y)
        {
            if (x == 0 || Tiles[x - 1, y] == 0 || Tiles[x - 1, y] == player) return;

            //on trouve une piece adverse au dessus de la piece qu'on vient de placer
            int i = x - 1;
            while (Tiles[i, y] != player && Tiles[i, y] != 0 && i != 0)
            {
                //on compte le nombre de pieces adverses à retourner dans le cas où l'on trouve une piece à nous en bout de ligne
                i--;
            }

            if (Tiles[i, y] == player)
            {
                for (int j = x - 1; j > i; j--)
                {
                    Tiles[j, y] = player;
                }
            }
        }
        private void right(int player, int x, int y)
        {
            if (y == BoardSize - 1 || Tiles[x, y + 1] == 0 || Tiles[x, y + 1] == player) return;

            //on trouve une piece adverse à la droite de la piece qu'on vient de placer
            int i = y + 1;
            while (Tiles[x, i] != player && Tiles[x, i] != 0 && i != BoardSize - 1)
            {
                //on compte le nombre de pieces adverses à retourner dans le cas où l'on trouve une piece à nous en bout de ligne
                i++;
            }

            if (Tiles[x, i] == player)
            {
                for (int j = y + 1; j < i; j++)
                {
                    Tiles[x, j] = player;
                }
            }
        }
        private void down(int player, int x, int y)
        {
            if (x == BoardSize - 1 || Tiles[x + 1, y] == 0 || Tiles[x + 1, y] == player) return;

            //on trouve une piece adverse au dessus de la piece qu'on vient de placer
            int i = x + 1;
            while (Tiles[i, y] != player && Tiles[i, y] != 0 && i != BoardSize - 1)
            {
                //on compte le nombre de pieces adverses à retourner dans le cas où l'on trouve une piece à nous en bout de ligne
                i++;
            }

            if (Tiles[i, y] == player)
            {
                for (int j = x + 1; j < i; j++)
                {
                    Tiles[j, y] = player;
                }
            }
        }
        #endregion

        override
        public string ToString()
        {
            int i = 0;
            int j = 0;
            string board = "";
            while (i < BoardSize)
            {
                j = 0;
                while (j < BoardSize)
                {
                    board += Tiles[i, j] + " | ";
                    j++;
                }
                board += Environment.NewLine;
                i++;
            }

            return board;
        }
    }
}
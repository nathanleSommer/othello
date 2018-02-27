using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using othello.ia;

namespace othello.model
{
    public class Board
    {
        public int[,] Tiles { get; set; }    
        public Board()
        {
            Tiles = new int[8,8];
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Tiles[i, j] = 0;
                }
            }
        }

        public void PlayMovePlayer(int x, int y)
        {
            Tiles[x, y] = 1;

            /* RETOURNER LES DISCS */

            //vertical haut
            for (int i = 0; i < x; i++)
            {
                if(Tiles[i, y] == 1)
                {
                    for(int j = i; j < x; j++)
                    {
                        Tiles[j, y] = 1;
                    }
                    break;
                }
            }

            //vertical bas
            for (int i = 7; i > x; i--)
            {
                if (Tiles[i, y] == 1)
                {
                    for (int j = i; j > x; j--)
                    {
                        Tiles[j, y] = 1;
                    }
                    break;
                }
            }

            //horizontal gauche
            for (int i = 0; i < y; i++)
            {
                if (Tiles[x, i] == 1)
                {
                    for (int j = i; j < x; j++)
                    {
                        Tiles[x, j] = 1;
                    }
                    break;
                }
            }

            //horizontal droit
            for (int i = 7; i > y; i--)
            {
                if (Tiles[x, i] == 1)
                {
                    for (int j = i; j > x; j--)
                    {
                        Tiles[x, j] = 1;
                    }
                    break;
                }
            }

        }

        public KeyValuePair<int,int> PlayMoveIA()
        {
            KeyValuePair<int,int> pos = IANoob.SelectPosition(Tiles);
            int x = pos.Key;
            int y = pos.Value;
            Tiles[x,y] = 2;

            /* RETOURNER LES DISCS */

            //vertical haut
            for (int i = 0; i < x; i++)
            {
                if (Tiles[i, y] == 2)
                {
                    for (int j = i; j < x; j++)
                    {
                        Tiles[j, y] = 2;
                    }
                    break;
                }
            }

            //vertical bas
            for (int i = 7; i > x; i--)
            {
                if (Tiles[i, y] == 2)
                {
                    for (int j = i; j > x; j--)
                    {
                        Tiles[j, y] = 2;
                    }
                    break;
                }
            }

            //horizontal gauche
            for (int i = 0; i < y; i++)
            {
                if (Tiles[x, i] == 2)
                {
                    for (int j = i; j < x; j++)
                    {
                        Tiles[x, j] = 2;
                    }
                    break;
                }
            }

            //horizontal droit
            for (int i = 7; i > y; i--)
            {
                if (Tiles[x, i] == 2)
                {
                    for (int j = i; j > x; j--)
                    {
                        Tiles[x, j] = 2;
                    }
                    break;
                }
            }

            return pos;
        }

        public bool IsValidPosition(int x, int y)
        {
            if (Tiles[x, y] != 0) return false;
            if (x != 0 && Tiles[x - 1, y] != 0) return true;
            if (x != 7 && Tiles[x + 1, y] != 0) return true;
            if (y != 0 && Tiles[x, y - 1] != 0) return true;
            if (y != 7 && Tiles[x, y + 1] != 0) return true;
            return false;
        }
    }
}
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

            /* RETOURNER LES DISCS du joueur 1 (humain) */
            returnTiles(1, x, y);
        }

        public KeyValuePair<int,int> PlayMoveIA()
        {
            KeyValuePair<int,int> pos = IANoob.SelectPosition(Tiles);
            int x = pos.Key;
            int y = pos.Value;
            Tiles[x,y] = 2;

            /* RETOURNER LES DISCS du joueur 2 (ia) */
            returnTiles(2, x, y);
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

        private void returnTiles(int player, int x, int y)
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

        #region ReturnTiles
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
            if (x <= 1 || y >= 6 || Tiles[x - 1, y + 1] == player || Tiles[x - 1, y + 1] == 0) return;

            int i = x - 1;
            int j = y + 1;
            while (Tiles[i, j] != player && Tiles[i, j] != 0 && i != 0 && j != 7)
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
            if (y >= 6 || x >= 6 || Tiles[x + 1, y + 1] == 0 || Tiles[x + 1, y + 1] == player) return;

            //on trouve une piece adverse à la diagonale haut-gauche de la piece qu'on vient de placer
            int i = x + 1;
            int j = y + 1;
            while (Tiles[i, j] != player && Tiles[i, j] != 0 && i != 7 && j != 7)
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
            if (x >= 6 || y <= 1 || Tiles[x + 1, y - 1] == player || Tiles[x + 1, y - 1] == 0) return;

            int i = x + 1;
            int j = y - 1;
            while (Tiles[i, j] != player && Tiles[i, j] != 0 && i != 7 && j != 0)
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
            if (y == 7 || Tiles[x, y + 1] == 0 || Tiles[x, y + 1] == player) return;

            //on trouve une piece adverse à la droite de la piece qu'on vient de placer
            int i = y + 1;
            while (Tiles[x, i] != player && Tiles[x, i] != 0 && i != 7)
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
            if (x == 7 || Tiles[x + 1, y] == 0 || Tiles[x + 1, y] == player) return;

            //on trouve une piece adverse au dessus de la piece qu'on vient de placer
            int i = x + 1;
            while (Tiles[i, y] != player && Tiles[i, y] != 0 && i != 7)
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
    }
}
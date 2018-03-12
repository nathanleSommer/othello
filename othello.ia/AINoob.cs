using System;
using System.Collections.Generic;

namespace othello.ia
{
    public class AINoob
    {
        public static KeyValuePair<int,int> SelectPosition(int[,] tiles, int size)
        {
            List<KeyValuePair<int, int>> validPos = new List<KeyValuePair<int, int>>();
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if(IsValidPosition(tiles, i,j, size))
                    {
                        validPos.Add(new KeyValuePair<int, int>(i,j));
                    }
                }
            }
            if(validPos.Count != 0)
            {
                Random rnd = new Random();
                int r = rnd.Next(validPos.Count);
                return validPos[r];
            }
            return default(KeyValuePair<int,int>);
        }

        public static bool IsValidPosition(int[,] tiles, int x, int y, int size)
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
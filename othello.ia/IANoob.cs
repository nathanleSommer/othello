using System;
using System.Collections.Generic;
using System.Text;

namespace othello.ia
{
    public class IANoob
    {
        public static KeyValuePair<int,int> SelectPosition(int[,] tiles)
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(tiles[i,j] == 0)
                    {
                        tiles[i, j] = 2;
                        return new KeyValuePair<int, int>(i,j);
                    }
                }
            }
            return default(KeyValuePair<int,int>);
        }
    }
}

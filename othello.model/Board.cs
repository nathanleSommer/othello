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

        public KeyValuePair<int,int> PlayMoveIA()
        {
            KeyValuePair<int,int> pos = IANoob.SelectPosition(Tiles);
            Tiles[pos.Key, pos.Value] = 2;
            return pos;
        }
    }
}
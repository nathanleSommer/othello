using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace othello.model
{
    public class Board
    {
        //public Disc[,] Discs { get; set; }
        public int[,] Tiles { get; set; }    
        public Board()
        {
            Tiles = new int[8,8];
            //Discs = new Disc[8,8];
        }
    }
}
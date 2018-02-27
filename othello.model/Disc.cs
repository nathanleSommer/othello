using System;
using System.Collections.Generic;
using System.Text;

namespace othello.model
{
    public class Disc
    {
        public string Color { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public Disc(string c, int x, int y)
        {
            PosX = x;
            PosY = y;
            Color = c;
        }
    }
}
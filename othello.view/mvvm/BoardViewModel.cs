using othello.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace othello.view.mvvm
{
    public class BoardViewModel
    {
        public Board Board { get; set; }

        public int[,] Tiles
        {
            get
            {
                return Board.Tiles;
            }
            set
            {
                Board.Tiles = value;
            }
        }

        public BoardViewModel()
        {
            Board = new Board();
        }
    }
}

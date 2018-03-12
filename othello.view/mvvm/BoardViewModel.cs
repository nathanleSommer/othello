using othello.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace othello.view.mvvm
{
    public class BoardViewModel : ViewModelBase
    {
        public int BoardSize;
        public Board Board { get; set; }

        public ObservableCollection<TileViewModel> Tiles { get; set; }

        public BoardViewModel(int size)
        {
            BoardSize = size;
            Board = new Board(size);
            Tiles = new ObservableCollection<TileViewModel>();

            for(int i = 0; i < BoardSize; i++)
            {
                for(int j = 0; j < BoardSize; j++)
                {
                    TileViewModel tile = new TileViewModel(i,j);
                    if (i == BoardSize/2 - 1 && j == BoardSize / 2 - 1)
                    {
                        tile.setDisc(new DiscViewModel(i, j, new SolidColorBrush(Colors.White)));
                        Board.Tiles[i, j] = 1;
                    }
                    if (i == BoardSize / 2 && j == BoardSize / 2 - 1)
                    {
                        tile.setDisc(new DiscViewModel(i, j, new SolidColorBrush(Colors.Black)));
                        Board.Tiles[i, j] = 2;
                    }
                    if(i == BoardSize / 2 - 1 && j == BoardSize / 2)
                    {
                        tile.setDisc(new DiscViewModel(i, j, new SolidColorBrush(Colors.Black)));
                        Board.Tiles[i, j] = 2;
                    }
                    if (i == BoardSize / 2 && j == BoardSize / 2)
                    {
                        tile.setDisc(new DiscViewModel(i, j, new SolidColorBrush(Colors.White)));
                        Board.Tiles[i, j] = 1;
                    }
                    Tiles.Add(tile);
                }

            }          
        }

        public void PlayMovePlayer(int x, int y)
        {
            Board.PlayMovePlayer(x, y);
            ReverseDiscs();
        }

        public void PlayMoveIA()
        {
            KeyValuePair<int,int> pos = Board.PlayMoveAI();
            foreach(TileViewModel t in Tiles)
            {
                if(t.PosX == pos.Key && t.PosY == pos.Value)
                {
                    t.setDisc(new DiscViewModel(t.PosX, t.PosY, new SolidColorBrush(Colors.Black)));
                    ReverseDiscs();
                    return;
                }
            }
        }

        private void ReverseDiscs()
        {
            foreach (TileViewModel tile in Tiles)
            {
                if (tile.HasDisc)
                {
                    if (Board.Tiles[tile.PosX, tile.PosY] == 1)
                    {
                        tile.Disc.Color = new SolidColorBrush(Colors.White);
                    }
                    else if (Board.Tiles[tile.PosX, tile.PosY] == 2)
                    {
                        tile.Disc.Color = new SolidColorBrush(Colors.Black);
                    }
                }
            }    
        }

        public bool IsValidPosition(TileViewModel tile)
        {
            return Board.IsValidPosition(tile.PosX, tile.PosY);
        }
    }
}
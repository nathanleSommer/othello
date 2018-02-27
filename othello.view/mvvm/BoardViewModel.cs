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
        public Board Board { get; set; }

        public ObservableCollection<TileViewModel> Tiles { get; set; }

        public BoardViewModel()
        {
            Board = new Board();
            Tiles = new ObservableCollection<TileViewModel>();

            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    TileViewModel tile = new TileViewModel(i,j);
                    if (i == 3 && j == 3)
                    {
                        tile.setDisc(new DiscViewModel(i, j, new SolidColorBrush(Colors.White)));
                        Board.Tiles[i, j] = 1;
                    }
                    if (i == 4 && j == 3)
                    {
                        tile.setDisc(new DiscViewModel(i, j, new SolidColorBrush(Colors.Black)));
                        Board.Tiles[i, j] = 2;
                    }
                    if(i == 3 && j == 4)
                    {
                        tile.setDisc(new DiscViewModel(i, j, new SolidColorBrush(Colors.Black)));
                        Board.Tiles[i, j] = 2;
                    }
                    if (i == 4 && j == 4)
                    {
                        tile.setDisc(new DiscViewModel(i, j, new SolidColorBrush(Colors.White)));
                        Board.Tiles[i, j] = 1;
                    }
                    Tiles.Add(tile);
                }

            }          
        }

        public void PlayMoveIA()
        {
            KeyValuePair<int,int> pos = Board.PlayMoveIA();
            foreach(TileViewModel t in Tiles)
            {
                if(t.PosX == pos.Key && t.PosY == pos.Value)
                {
                    t.setDisc(new DiscViewModel(t.PosX, t.PosY, new SolidColorBrush(Colors.Black)));
                }
            }

        }
    }
}
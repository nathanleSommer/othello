using othello.ia;
using othello.view.mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace othello.view
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _canPlay;

        BoardViewModel Board { get; set; }

        public ObservableCollection<TileViewModel> Tiles
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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Board = new BoardViewModel();
            _canPlay = true;
        }

        private void Tile_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle r = (Rectangle)sender;
            TileViewModel tile = (TileViewModel)r.DataContext;
            int x = tile.PosX;
            int y = tile.PosY;
            
            if (Board.IsValidPosition(tile) && _canPlay)
            {
                tile.setDisc(new DiscViewModel(x, y, new SolidColorBrush(Colors.White)));
                Board.PlayMovePlayer(x, y);
                Board.PlayMoveIA();
            } else
            {
                MessageBox.Show("" + Board.Board.Tiles[x, y]);
            }
        } 
    }
}
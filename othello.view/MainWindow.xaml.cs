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
        private TileViewModel _tile;

        public BoardViewModel Board { get; set; }
        public int BoardSize
        {
            get
            {
                return 6;
            }
            private set { }
        }

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
            Board = new BoardViewModel(BoardSize);
            _canPlay = true;
        }

        private void Tile_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_canPlay)
            {
                Rectangle r = (Rectangle)sender;
                _tile = (TileViewModel)r.DataContext;
                int x = _tile.PosX;
                int y = _tile.PosY;

                if (Board.IsValidPosition(1, _tile) && _canPlay)
                {
                    _tile.setDisc(new DiscViewModel(x, y, new SolidColorBrush(Colors.Black)));
                    Board.PlayMovePlayer(x, y);
                    _canPlay = false;
                }
                else
                {
                    MessageBox.Show("(" + x + ";" + y + ") : " + Board.Board.Tiles[x, y]);
                }
            } 
        }

        private void Button_EndTurn(object sender, RoutedEventArgs e)
        {
            if (!Board.PlayMoveIA())
            {
                MessageBox.Show("Impossible de placer une pièce : à vous de jouer.");
            }
            _canPlay = true;
        }
    }
}
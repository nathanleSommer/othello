using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace othello.view.mvvm
{
    public class DiscViewModel : ViewModelBase
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public Brush Color { get; set; }

        public DiscViewModel(int x, int y, Brush color)
        {
            PosX = x;
            PosY = y;
            Color = color;
        }
    }
}

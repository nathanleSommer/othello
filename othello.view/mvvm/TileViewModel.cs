using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace othello.view.mvvm
{
    public class TileViewModel : ViewModelBase
    {
        private bool _HasDisc;
        public bool HasDisc
        {
            get
            {
                return _HasDisc;
            }
            set
            {
                _HasDisc = value;
                RaisePropertyChanged("HasDisc");
            }
        }

        private DiscViewModel _Disc;
        public DiscViewModel Disc
        {
            get
            {
                return _Disc;
            }
            set
            {
                _Disc = value;
                RaisePropertyChanged("Disc");
            }
        }

        public int PosX { get; set; }
        public int PosY { get; set; }

        public TileViewModel(int x, int y)
        {
            PosX = x;
            PosY = y;
            HasDisc = false;
            Disc = null;
        }

        public void setDisc(DiscViewModel d)
        {
            Disc = d;
            HasDisc = true;
        }
    }
}
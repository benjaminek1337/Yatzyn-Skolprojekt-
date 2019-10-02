using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Yatzy.Models
{
    public class Dice : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public int DiceID { get; set; }

        private int _diceValue;
        public int DiceValue
        {
            get { return _diceValue; }
            set
            {
                if (_diceValue != value)
                {
                    _diceValue = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("DiceValue"));
                }                
            }
        }      

        private BitmapImage _diceImage;
        public BitmapImage DiceImage
        {
            get { return _diceImage; }
            set
            {
                if (_diceImage != value)
                {
                    _diceImage = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("DiceImage"));
                }
            }
        }

        private bool _isDiceEnabled;
        public bool IsDiceEnabled
        {
            get { return _isDiceEnabled; }
            set
            {
                if(_isDiceEnabled != value)
                {
                    _isDiceEnabled = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsDiceEnabled"));
                }
                
            }
        }

        private bool _diceHasValue;
        public bool DiceHasValue
        {
            get { return _diceHasValue; }
            set { _diceHasValue = value; PropertyChanged(this, new PropertyChangedEventArgs("DiceHasValue")); }
        }



    }
}

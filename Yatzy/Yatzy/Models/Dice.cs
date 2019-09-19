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

        private int diceValue;

        public int DiceValue
        {
            get { return diceValue; }
            set
            {
                if (diceValue != value)
                {
                    diceValue = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("DiceValue"));
                }                
            }
        }      

        public int DiceID { get; set; }

        private BitmapImage diceImage;
        public BitmapImage DiceImage
        {
            get { return diceImage; }
            set
            {
                if (diceImage != value)
                {
                    diceImage = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("DiceValue"));
                }
            }
        }



        //public int DiceValue { get; set; }
        //public bool IsDiceEnabled { get; set; }

        private bool isDiceEnabled;

        public bool IsDiceEnabled
        {
            get { return isDiceEnabled; }
            set
            {
                if(isDiceEnabled != value)
                {
                    isDiceEnabled = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsDiceEnabled"));
                }
                
            }
        }



    }
}

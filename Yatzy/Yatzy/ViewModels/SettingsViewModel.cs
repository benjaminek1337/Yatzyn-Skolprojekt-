using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using System.Windows.Media;

namespace Yatzy.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        #region Properties

        public MediaPlayer mediaPlayer { get; set; }



        #endregion

        #region Methods
        public void ToggleSound(object paramter)
        {
            bool sound = true;



        }

        public void ToggleImage(object paramater)
        {

        }

        #endregion

        #region Event Handlers
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

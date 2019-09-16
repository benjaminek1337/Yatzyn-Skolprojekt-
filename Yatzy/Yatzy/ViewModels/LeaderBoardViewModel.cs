using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yatzy.Models;

namespace Yatzy.ViewModels
{
    class LeaderBoardViewModel : INotifyPropertyChanged
    {

        #region Commands

        public ICommand UpdateLeaderBoardCommand { get; set; }


        #endregion

        #region Contructor

        public LeaderBoardViewModel()
        {

        }

        ObservableCollection<Player> leaderBoard { get; set; }

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

        #region Methods

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void UpdateLeaderBoard(object parameter)
        {



        }

        #endregion
    }
}

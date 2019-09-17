using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yatzy.Models;
using Yatzy.DBOps;

namespace Yatzy.ViewModels
{
    class LeaderBoardViewModel : INotifyPropertyChanged
    {

        #region Fields

        DbOperations dbOps = new DbOperations();
        
        ObservableCollection<Player> leaderBoard { get; set; }


        #endregion

        #region Contructor

        public LeaderBoardViewModel()
        {
            LeaderBoardClassic();
            LeaderBoardForced();
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

        #region Methods

        public void LeaderBoardClassic()
        {
            leaderBoard = dbOps.GetHighScorePlayers(4);            
        }

        public void LeaderBoardForced()
        {
            leaderBoard = dbOps.GetHighScorePlayers(5);
        }
        #endregion
    }
}

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
using Yatzy.Commands;

namespace Yatzy.ViewModels
{
    class LeaderBoardViewModel : INotifyPropertyChanged
    {

        #region Fields

        DbOperations dbOps = new DbOperations();
        public ICommand BackCommand { get; set; }

        private ObservableCollection<Player> _leaderboardsevenDays;
        public ObservableCollection<Player> LeaderboardsevenDays {
            get { return _leaderboardsevenDays; }
            set { _leaderboardsevenDays = value; OnPropertyChanged("LeaderboardsevenDays");}
        }

        private ObservableCollection<Player> _mostGames;
        public ObservableCollection<Player> MostGames
        {
            get { return _mostGames; }
            set { _mostGames = value; OnPropertyChanged("LeaderboardsevenDays"); }
        }

        private ObservableCollection<Player> _mostvictoriesinaRow;
        public ObservableCollection<Player> MostVictoriesInaRow
        {
            get { return _mostvictoriesinaRow; }
            set { _mostvictoriesinaRow = value; OnPropertyChanged("LeaderboardsevenDays"); }
        }

        #endregion


        #region Contructor

        public LeaderBoardViewModel()
        {
            dbOps = new DbOperations();            
            LeaderboardsevenDays = new ObservableCollection<Player>();
            LeaderBoard7Days();
            LeaderBoardMostGames();
            LeaderBoardMostVicoriesInARow();
            BackCommand = new RelayCommand(Backcommand, CanExecuteMethod);           
            
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

        private void LeaderBoard7Days()
        {
            LeaderboardsevenDays = dbOps.GetHighScorePlayers(4);            
        }
        
        private void LeaderBoardMostGames()
        {
            MostGames = dbOps.GetHighestGames(4); 
        }

        private void LeaderBoardMostVicoriesInARow()
        {
            MostVictoriesInaRow = dbOps.GetHighestWinStreak(4);
        }

        private void CountLeaderBoardPosition()
        {
            
        }
        #endregion


        #region Methods for going back
        private object selectedViewModel;
        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }
        public void Backcommand(object parameter)
        {
            SelectedViewModel = new MainMenuViewModel();
        }
        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }
        #endregion
    }
}

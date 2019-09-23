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

        #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand ShowGamesLboard { get; set; }
        public ICommand ShowWinStreakLboard { get; set; }
        public ICommand ShowLBoard { get; set; }

        #endregion


        #region Fields

        DbOperations dbOps = new DbOperations();

        private ObservableCollection<Player> _leaderboardsevenDays;
        public ObservableCollection<Player> LeaderboardsevenDays {
            get { return _leaderboardsevenDays; }
            set { _leaderboardsevenDays = value; OnPropertyChanged("LeaderboardsevenDays");}
        }

        private ObservableCollection<Player> _mostGames;
        public ObservableCollection<Player> MostGames
        {
            get { return _mostGames; }
            set { _mostGames = value; OnPropertyChanged("MostGames"); }
        }

        private ObservableCollection<Player> _mostvictoriesinaRow;
        public ObservableCollection<Player> MostVictoriesInaRow
        {
            get { return _mostvictoriesinaRow; }
            set { _mostvictoriesinaRow = value; OnPropertyChanged("MostVictoriesInaRow"); }
        }

        #endregion


        #region Fields for Showing LeaderBoards
        private bool _showmostgame;
        public bool ShowMostGames
        {
            get { return _showmostgame; }
            set { _showmostgame = value;OnPropertyChanged("ShowMostGames"); }
        }

        private bool _showleaderboard;
        public bool ShowLeaderBoard
        {
            get { return _showleaderboard; }
            set { _showleaderboard = value; OnPropertyChanged("ShowLeaderBoard"); }
        }

        private bool _showwinstreak;
        public bool ShowWinStreak
        {
            get { return _showwinstreak; }
            set { _showwinstreak = value; OnPropertyChanged("ShowWinStreak"); }
        }
        #endregion


        #region Contructor

        public LeaderBoardViewModel()
        {
            dbOps = new DbOperations();            
            LeaderboardsevenDays = new ObservableCollection<Player>();
            MostGames = new ObservableCollection<Player>();

            LeaderBoard7Days();
            LeaderBoardMostGames();
            LeaderBoardMostVicoriesInARow();

            ShowLBoard = new RelayCommand(ShowLeaderBoardFromMostGamesMethod, CanExecuteMethod);
            ShowWinStreakLboard = new RelayCommand(ShowWinStreakMethod ,CanExecuteMethod);
            ShowGamesLboard = new RelayCommand(ShowMostGamesMethod, CanExecuteMethod);
            BackCommand = new RelayCommand(Backcommand, CanExecuteMethod);          

            ShowMostGames = false;
            ShowLeaderBoard = true;
            ShowWinStreak = false;
            
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


        #region Methods for showing leaderboards

        private void ShowWinStreakMethod(object paramater)
        {
            ShowLeaderBoard = false;
            ShowWinStreak = true;
        }

        private void ShowMostGamesMethod(object paramater)
        {          
            ShowLeaderBoard = false;
            ShowMostGames = true;
        }

        private void ShowLeaderBoardFromMostGamesMethod(object paramater)
        {
            ShowWinStreak = false;
            ShowLeaderBoard = true;
            ShowMostGames = false;
        }

        private void ShowLeaderBoardFromWinStreakMethod(object paramater)
        {
            ShowLeaderBoard = true;
            ShowWinStreak = false;
        }
        #endregion


        #region Methods for populatiing leaderboards


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
            //MostVictoriesInaRow = dbOps.GetHighestWinStreak(4);
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

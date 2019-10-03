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
        public ICommand ShowForcedCommand { get; set; }
        public ICommand ShowForcedGamesCommand { get; set; }
        public ICommand ShowForcedWinStreak { get; set; }

        #endregion


        #region Fields

        DbOperations dbOps = new DbOperations();

        private ObservableCollection<Player> _leaderboard7Days;
        public ObservableCollection<Player> Leaderboard7Days {
            get { return _leaderboard7Days; }
            set { _leaderboard7Days = value; OnPropertyChanged("Leaderboard7Days");}
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

        private ObservableCollection<Player> _leaderboardsevenDaysforced;
        public ObservableCollection<Player> LeaderboardsevenDaysForced
        {
            get { return _leaderboardsevenDaysforced; }
            set { _leaderboardsevenDaysforced = value; OnPropertyChanged("LeaderboardsevenDaysForced"); }
        }

        private ObservableCollection<Player> _mostGamesforced;
        public ObservableCollection<Player> MostGamesForced
        {
            get { return _mostGamesforced; }
            set { _mostGamesforced = value; OnPropertyChanged("MostGamesForced"); }
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

        private bool _showForcedgame;
        public bool ShowForcedMostGames
        {
            get { return _showForcedgame; }
            set { _showForcedgame = value; OnPropertyChanged("ShowForcedMostGames"); }
        }

        private bool _showForcedleaderboard;
        public bool ShowForcedLeaderBoard
        {
            get { return _showForcedleaderboard; }
            set { _showForcedleaderboard = value; OnPropertyChanged("ShowForcedLeaderBoard"); }
        }

        private bool _showwinstreakforced;
        public bool ShowWinStreakForced
        {
            get { return _showwinstreakforced; }
            set { _showwinstreakforced = value; OnPropertyChanged("ShowWinStreakForced"); }
        }

        #endregion


        #region Contructor

        public LeaderBoardViewModel()
        {
            dbOps = new DbOperations();            
            Leaderboard7Days = new ObservableCollection<Player>();
            MostGames = new ObservableCollection<Player>();
            MostVictoriesInaRow = new ObservableCollection<Player>();

            PopulateLeaderBoard7Days();
            PopulateLeaderBoardMostGames();
            PopulateLeaderBoardMostVicoriesInARow();
            PopulateLeaderBoardMostGamesForced();
            PopulateLeaderBoard7DaysForced();

            BackCommand = new RelayCommand(Backcommand, CanExecuteMethod);
            ShowLBoard = new RelayCommand(ShowLeaderBoardFromMostGamesMethod, CanExecuteMethod);
            ShowWinStreakLboard = new RelayCommand(ShowWinStreakMethod ,CanExecuteMethod);
            ShowGamesLboard = new RelayCommand(ShowMostGamesMethod, CanExecuteMethod);

            ShowForcedCommand = new RelayCommand(ShowForcedYatzyMethod , CanExecuteMethod);
            ShowForcedGamesCommand = new RelayCommand(ShowForcedMostGamesMethod, CanExecuteMethod);
            ShowForcedWinStreak = new RelayCommand(ShowForcedWinstreak, CanExecuteMethod);

            ShowLeaderBoard = true;
            ShowMostGames = false;
            ShowWinStreakForced = false;
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
            ShowWinStreak = true;
            ShowLeaderBoard = false;
            ShowForcedLeaderBoard = false;
            ShowMostGames = false;
            ShowForcedMostGames = false;
            ShowWinStreakForced = false;
        }

        private void ShowMostGamesMethod(object paramater)
        {          
            ShowMostGames = true;
            ShowLeaderBoard = false;
            ShowWinStreak = false;
            ShowForcedLeaderBoard = false;
            ShowForcedMostGames = false;
            ShowWinStreakForced = false;
        }

        private void ShowLeaderBoardFromMostGamesMethod(object paramater)
        {
            ShowLeaderBoard = true;
            ShowWinStreak = false;
            ShowForcedLeaderBoard = false;
            ShowMostGames = false;
            ShowForcedMostGames = false;
            ShowWinStreakForced = false;
        }

        private void ShowForcedYatzyMethod(object parameter)
        {
            ShowForcedLeaderBoard = true;
            ShowForcedMostGames = false;
            ShowWinStreak = false;
            ShowLeaderBoard = false;
            ShowMostGames = false;
            ShowWinStreakForced = false;

        }

        private void ShowForcedMostGamesMethod(object parameter)
        {
            ShowForcedMostGames = true;
            ShowForcedLeaderBoard = false;
            ShowWinStreak = false;
            ShowLeaderBoard = false;
            ShowMostGames = false;
            ShowWinStreakForced = false;
        }

        private void ShowForcedWinstreak(object parameter)
        {
            ShowWinStreakForced = true;
            ShowForcedMostGames = false;
            ShowForcedLeaderBoard = false;
            ShowWinStreak = false;
            ShowLeaderBoard = false;
            ShowMostGames = false;
        }

        #endregion


        #region Methods for populating leaderboards


        private void PopulateLeaderBoard7Days()
        {
            Leaderboard7Days = dbOps.GetHighScorePlayers(1);            
        }
        
        private void PopulateLeaderBoardMostGames()
        {
            MostGames = dbOps.GetHighestGames(1); 
        }

        private void PopulateLeaderBoardMostVicoriesInARow()
        {
            MostVictoriesInaRow = new ObservableCollection<Player>(dbOps.GetWinStreak());          
        }

        private void PopulateLeaderBoard7DaysForced()
        {
            LeaderboardsevenDaysForced = dbOps.GetHighScorePlayers(2);
        }

        private void PopulateLeaderBoardMostGamesForced()
        {
            MostGamesForced = dbOps.GetHighestGames(2);
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

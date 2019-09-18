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
        
        ObservableCollection<Player> leaderBoard { get; set; }
        public ICommand BackCommand { get; set; }


        private ObservableCollection<Player> sortedBy;
        public ObservableCollection<Player> SortedBy
        {
            get { return sortedBy; }
            set
            {
                sortedBy = value;
                OnPropertyChanged("SortedBy");
            }

        }
        #endregion

        #region Hårdkodad lista för testning

        private ObservableCollection<Player> _hardcodedPlayers;
        public ObservableCollection<Player> HardcodedPlayers
        {
            get { return _hardcodedPlayers; }
            set { _hardcodedPlayers = value; OnPropertyChanged("HardcodedPlayers"); }
        }

        private void SetHardcodedPlayers()
        {
            Player p = new Player
            {
                Firstname = "Beendjaameeehn",
                Lastname = "Ek",
                Nickname = "Galne_Gunnar1337",
                HighScore = 2
            }; HardcodedPlayers.Add(p);
            Player p2 = new Player
            {
                Firstname = "Djååohäänis",
                Lastname = "Inte ett könsord ;)",
                Nickname = "Cheffer",
                HighScore = 1
            }; HardcodedPlayers.Add(p2);
            Player p3 = new Player
            {
                Firstname = "Määtiihuuuhs",
                Lastname = "Svensson",
                Nickname = "Bruh_momento",
                HighScore = 420
            }; HardcodedPlayers.Add(p3);
        }

        #endregion

        #region Contructor

        public LeaderBoardViewModel()
        {
            HardcodedPlayers = new ObservableCollection<Player>();
            SetHardcodedPlayers();
            CountLeaderBoardPosition();
            SortedBy = new ObservableCollection<Player>();
            //LeaderBoard7Days();
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

        public void LeaderBoard7Days()
        {
            leaderBoard = dbOps.GetHighScorePlayers(4);            
        }

        ObservableCollection<Player> Sortedby;
        public void CountLeaderBoardPosition()
        {
            Sortedby = dbOps.GetAvaliablePlayers();
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

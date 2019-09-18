using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzy.Commands;
using Yatzy.GameEngine;
using Yatzy.Models;
using Yatzy.DBOps;

namespace Yatzy.ViewModels
{
    class CreateGameViewModel : INotifyPropertyChanged
    {

        #region Properties
        public RelayCommand ClassicGameCommand { get; set; }
        public RelayCommand StyrdGameCommand { get; set; }
        public RelayCommand AddPlayerCommand { get; set; }
        public RelayCommand RemovePlayerCommand { get; set; }
        public RelayCommand StartGameCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        private ObservableCollection<Player> _players;
        public ObservableCollection<Player> AvailablePlayers
        {
            get { return _players; }
            set { _players = value; OnPropertyChanged("Players"); }
        }

        private ObservableCollection<Player> selectedPlayers;
        public ObservableCollection<Player> SelectedPlayers
        {
            get { return selectedPlayers; }
            set { selectedPlayers = value; OnPropertyChanged("SelectedPlayers"); }
        }

        private Player _selectedPlayer;
        public Player SelectedPlayer
        {
            get { return _selectedPlayer; }
            set { _selectedPlayer = value; OnPropertyChanged("SelectedPlayer"); }
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
                Nickname = "Galne_Gunnar1337"
            }; HardcodedPlayers.Add(p);
            Player p2 = new Player
            {
                Firstname = "Djååohäänis",
                Lastname = "Inte ett könsord ;)",
                Nickname = "ChildKillah"
            }; HardcodedPlayers.Add(p2);
            Player p3 = new Player
            {
                Firstname = "Määtiihuuuhs",
                Lastname = "Svensson",
                Nickname = "Skolrunken"
            }; HardcodedPlayers.Add(p3);
        }

        #endregion

        #region Objekt och lokala variabler

        DicesViewModel dicesViewModel;
        PlayerEngine playerEngine;
        DbOperations dbOps = new DbOperations();


        private int gameType = 0; //Ändra denna till 4 för klassisk eller 5 för styrd.

        #endregion

        #region Constructor

        public CreateGameViewModel()
        {
            playerEngine = new PlayerEngine();

            HardcodedPlayers = new ObservableCollection<Player>();
            SelectedPlayers = new ObservableCollection<Player>();
            SelectedPlayer = new Player();

            ClassicGameCommand = new RelayCommand(ClassicGame, CanChooseClassicYatzy);
            StyrdGameCommand = new RelayCommand(StyrdGame, CanChooseStyrdYatzy);
            AddPlayerCommand = new RelayCommand(AddPlayer, CanAddPlayer);
            RemovePlayerCommand = new RelayCommand(RemovePlayer, CanRemovePlayer);
            StartGameCommand = new RelayCommand(StartGame, CanStartGame);
            BackCommand = new RelayCommand(Backcommand ,CanExecuteMethod);
           
            //GetAvaliablePlayers();
            SetHardcodedPlayers();
        }

        #endregion

        #region PropChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        #endregion

        #region bools
        private bool CanStartGame(object parameter)
        {
            if (playerEngine.ActivePlayers.Count > 1 && playerEngine.ActivePlayers.Count < 5 && gameType < 6 && gameType > 3)
                return true;
            else
                return false;
        }

        private bool CanAddPlayer(object parameter)
        {
            if (SelectedPlayers.Count < 5 && SelectedPlayer != null)
                return true;
            else
                return false;                    
        }
        private bool CanRemovePlayer(object parameter)
        {
            if (SelectedPlayers.Count > 0 && SelectedPlayer != null)
                return true;
            else
                return false;
        }

        private bool CanChooseClassicYatzy(object parameter)
        {
            if (gameType == 4)
                return false;
            else
                return true;
        }

        private bool CanChooseStyrdYatzy(object parameter)
        {
            if (gameType == 5)
                return false;
            else
                return true;                
        }

        #endregion

        #region Metoder
        private void GetAvaliablePlayers()
        {
            AvailablePlayers = dbOps.GetAvaliablePlayers();
        }

        public void RemovePlayer(object parameter)
        {
            playerEngine.ActivePlayers.Remove(SelectedPlayer);
            SelectedPlayers.Remove(SelectedPlayer);
        }

        public void AddPlayer(object parameter)
        {
            playerEngine.ActivePlayers.Add(SelectedPlayer);
            SelectedPlayers.Add(SelectedPlayer);
            if (SelectedPlayer != null)
                AvailablePlayers.Remove(SelectedPlayer);
            else
                SelectedPlayer = null;
        }

        private void ClassicGame(object parameter)
        {
            gameType = int.Parse(parameter.ToString());
        }

        private void StyrdGame(object parameter)
        {
            gameType = int.Parse(parameter.ToString());
        }

        private void StartGame(object parameter)
        {
            dicesViewModel = new DicesViewModel(gameType);
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

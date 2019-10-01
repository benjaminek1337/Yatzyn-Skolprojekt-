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
using Yatzy.Views;
using Yatzy.ViewModels;
using System.Windows.Input;
using System.Windows;

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
        public RelayCommand AddNewPlayerCommand { get; set; }
        public ICommand BackCommand { get; set; }

        private ObservableCollection<Player> _availablePlayers;
        public ObservableCollection<Player> AvailablePlayers
        {
            get { return _availablePlayers; }
            set { _availablePlayers = value; OnPropertyChanged("AvailablePlayers"); }
        }

        private ObservableCollection<Player> selectedPlayers;
        public ObservableCollection<Player> SelectedPlayers
        {
            get { return selectedPlayers; }
            set { selectedPlayers = value; OnPropertyChanged("SelectedPlayers"); }
        }

        private Player _availablePlayer;
        public Player AvailablePlayer
        {
            get { return _availablePlayer; }
            set { _availablePlayer = value; OnPropertyChanged("AvailablePlayer"); }
        }

        private Player player;

        public Player SelectedPlayer
        {
            get { return player; }
            set { player = value; OnPropertyChanged("SelectedPlayer"); }
        }

        private string _firstname;
        public string _Firstname
        {
            get { return _firstname; }
            set { _firstname = value; OnPropertyChanged("_Firstname"); }
        }

        private string _lastname;
        public string _Lastname
        {
            get { return _lastname; }
            set { _lastname = value; OnPropertyChanged("_Lastname"); }
        }

        private string _nickname;
        public string _Nickname
        {
            get { return _nickname; }
            set { _nickname = value; OnPropertyChanged("_Nickname"); }
        }
        #endregion

        #region Objekt och lokala variabler

        PlayerEngine playerEngine;        
        DbOperations dbOps;
        NavigationViewModel nav;

        private int gameType = 0; //Denna ändras till 4 för klassisk eller 5 för styrd.

        #endregion

        #region Constructor

        public CreateGameViewModel()
        {
            dbOps = new DbOperations();
            playerEngine = new PlayerEngine();
            SelectedPlayers = new ObservableCollection<Player>();
            AvailablePlayer = new Player();
            AvailablePlayers = new ObservableCollection<Player>();
            nav = new NavigationViewModel();

            ClassicGameCommand = new RelayCommand(ClassicGame, CanChooseClassicYatzy);
            StyrdGameCommand = new RelayCommand(StyrdGame, CanChooseStyrdYatzy);
            AddPlayerCommand = new RelayCommand(AddPlayer, CanAddPlayer);
            RemovePlayerCommand = new RelayCommand(RemovePlayer, CanRemovePlayer);
            StartGameCommand = new RelayCommand(StartGame, CanStartGame);
            BackCommand = new RelayCommand(Backcommand, CanExecuteMethod);
            AddNewPlayerCommand = new RelayCommand(AddNewPlayer, CanAddNewPlayer);

            AvailablePlayer = null;
            GetAvaliablePlayers();
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
            if (gameType < 3 && gameType > 0 && SelectedPlayers.Count >= 2 && SelectedPlayers.Count <= 4)
                return true;
            else
                return false;
        }

        private bool CanAddPlayer(object parameter)
        {
            if (SelectedPlayers.Count < 4 && AvailablePlayer != null)
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

        private bool CanAddNewPlayer(object parameter)
        {
            if (!String.IsNullOrEmpty(_Firstname) && !String.IsNullOrEmpty(_Lastname) && !String.IsNullOrEmpty(_Nickname))
                return true;
            else
                return false;
        }

        private bool CanChooseClassicYatzy(object parameter)
        {
            if (gameType == 1)
                return false;
            else
                return true;
        }

        private bool CanChooseStyrdYatzy(object parameter)
        {
            if (gameType == 2)
                return false;
            else
                return true;
        }

        #endregion

        #region Metoder
        public void GetAvaliablePlayers()
        {
            AvailablePlayers = dbOps.GetAvaliablePlayers();
        }

        private void UpdateAvaliablePlayers()
        {
            AvailablePlayers.Clear();
            AvailablePlayers = dbOps.GetAvaliablePlayers();
        }

        public void RemovePlayer(object parameter)
        {
            playerEngine.ActivePlayers.Remove(SelectedPlayer);
            AvailablePlayers.Add(SelectedPlayer);
            if (SelectedPlayer != null)
            {
                SelectedPlayers.Remove(SelectedPlayer);
                SelectedPlayer = null;
            }
            SelectedPlayer = null;
        }

        public void AddPlayer(object parameter)
        {
            playerEngine.ActivePlayers.Add(AvailablePlayer);
            SelectedPlayers.Add(AvailablePlayer);

            if (AvailablePlayer != null)
            {
                AvailablePlayers.Remove(AvailablePlayer);
                AvailablePlayer = null;
            }
            AvailablePlayer = null;
        }

        private void ClassicGame(object parameter)
        {
            gameType = int.Parse(parameter.ToString());
            playerEngine.GetGameType(gameType);
        }

        private void StyrdGame(object parameter)
        {
            gameType = int.Parse(parameter.ToString());
            playerEngine.GetGameType(gameType);
        }

        private void StartGame(object parameter)
        {
            
            //nav.EndMainMusic();
            dbOps.StartGameTransaction(SelectedPlayers, gameType);
            PlayGameView dicesView = new PlayGameView();
            SelectedViewModel = new DicesViewModel(playerEngine);
            dicesView.DataContext = SelectedViewModel;
        }

        private void AddNewPlayer(object parameter)
        {
            Player player;
            player = new Player
            {
                Firstname = _Firstname,
                Lastname = _Lastname,
                Nickname = _Nickname
            };
            dbOps.RegisterPlayer(player);
            _Firstname = null;
            _Lastname = null;
            _Nickname = null;
            UpdateAvaliablePlayers();
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

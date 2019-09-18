using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Yatzy.Commands;
using Yatzy.DBOps;
using Yatzy.GameEngine;
using Yatzy.Models;
using Yatzy.Views;

namespace Yatzy.ViewModels
{
    class AddPlayerViewModel : INotifyPropertyChanged
    {
        DbOperations dbOps;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #region Properties 
        public RelayCommand AddPlayerCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; PropertyChanged(this, new PropertyChangedEventArgs("Player")); }
        }

        private string _firstname;
        public string _Firstname
        {
            get { return _firstname; }
            set { _firstname = value; PropertyChanged(this, new PropertyChangedEventArgs("_Firstname")); }
        }

        private string _lastname;
        public string _Lastname
        {
            get { return _lastname; }
            set { _lastname = value; PropertyChanged(this, new PropertyChangedEventArgs("_Lastname")); }
        }

        private string _nickname;
        public string _Nickname
        {
            get { return _nickname; }
            set { _nickname = value; PropertyChanged(this, new PropertyChangedEventArgs("_Nickname")); }
        }

        #endregion

        #region Konstruktor
        public AddPlayerViewModel()
        {
            dbOps = new DbOperations();
            AddPlayerCommand = new RelayCommand(AddPlayer, CanAddPlayer);
            CancelCommand = new RelayCommand(Cancel, CanExecute);
        }
        #endregion

        #region Metoder för att lägga till spelare

        private void AddPlayer(object parameter)
        {
            Player = new Player
            {
                Firstname = _Firstname,
                Lastname = _Lastname,
                Nickname = _Nickname
            };
            dbOps.RegisterPlayer(Player);
            MessageBox.Show("Spelare " + Player.Firstname + " '" + Player.Nickname + "' " + Player.Lastname + " är tillagd.");
        }

        private void Cancel(object parameter)
        {
            //Metod för att återgå till tidigare
            AddPlayerView addPlayerView = new AddPlayerView();
            addPlayerView.Close();
        }

        private bool CanAddPlayer(object parameter)
        {
            //if (_Firstname != null && _Lastname != null && _Nickname != null)
                return true;
            //else
            //    return false;
                    
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }

        #endregion



    }
}

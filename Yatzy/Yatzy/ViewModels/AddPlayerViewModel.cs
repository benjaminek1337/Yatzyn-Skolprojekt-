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

namespace Yatzy.ViewModels
{
    class AddPlayerViewModel : INotifyPropertyChanged
    {
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
            set { _firstname = value; PropertyChanged(this, new PropertyChangedEventArgs("_Firstname"); }
        }

        private string _lastname;
        public string _Lastname
        {
            get { return _lastname; }
            set { _lastname = value; PropertyChanged(this, new PropertyChangedEventArgs("_Lastname"); }
        }

        private string _nickname;
        public string _Nickname
        {
            get { return _nickname; }
            set { _nickname = value; PropertyChanged(this, new PropertyChangedEventArgs("_Nickname"); }
        }

        #endregion

        #region Konstruktor
        public AddPlayerViewModel()
        {
            AddPlayerCommand = new RelayCommand(AddPlayer, CanExecute);
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
        }

        private void Cancel(object parameter)
        {
            //Metod för att återgå till tidigare
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }

        #endregion



    }
}

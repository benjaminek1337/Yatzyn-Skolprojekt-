using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Models
{
    class Player : INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #region variabler till properties
        private string _firstname;
        private string _lastname;
        private string _nickname;
        private int _playerid;
        private int _highscore;

        private int? _ones;
        private int? _twos;
        private int? _threes;
        private int? _fours;
        private int? _fives;
        private int? _sixes;
        private int? _upperScore;
        private int? _upperBonus;
        private int? _pair;
        private int? _twoPairs;
        private int? _threeOfaKind;
        private int? _fourOfaKind;
        private int? _smalLadder;
        private int? _largeLadder;
        private int? _fullHouse;
        private int? _chance;
        private int? _yatzy;
        private int? _totalScore;
        #endregion




        #region player properties
        public string Firstname
        {
            get { return _firstname; }
            set
            {
                if (value != _firstname)
                {
                    _firstname = value;
                    OnPropertyChanged("Firstname");
                }
            }
        }
        public string Lastname
        {
            get { return _lastname; }
            set
            {
                if (value != _lastname)
                {
                    _lastname = value;
                    OnPropertyChanged("Lastname");
                }
            }
        }
        public string Nickname
        {
            get { return _nickname; }
            set
            {
                if (value != _nickname)
                {
                    _nickname = value;
                    OnPropertyChanged("Lastname");
                }
            }
        }

        public int PlayerId
        {
            get { return _playerid; }
            set
            {
                if (value != _playerid)
                {
                    _playerid = value;
                    OnPropertyChanged("PlayerID");
                }
            }
        }

        public int HighScore
        {
            get { return _highscore; }
            set
            {
                if (value != _highscore)
                {
                    _highscore = value;
                    OnPropertyChanged("PlayerID");
                }
            }
        }
        #endregion

        #region dice result properties
        public int? Ones
        {
            get { return _ones; }
            set
            {
                if (value != _ones)
                {
                    _ones = value;
                    OnPropertyChanged("Ones");
                }
            }
        }
        public int? Twos
        {
            get { return _twos; }
            set
            {
                if (value != _twos)
                {
                    _twos = value;
                    OnPropertyChanged("Twos");
                }
            }
        }
        public int? Threes
        {
            get { return _threes; }
            set
            {
                if (value != _threes)
                {
                    _threes = value;
                    OnPropertyChanged("Threes");
                }
            }
        }
        public int? Fours
        {
            get { return _fours; }
            set
            {
                if (value != _fours)
                {
                    _fours = value;
                    OnPropertyChanged("Fours");
                }
            }
        }
        public int? Fives
        {
            get { return _fives; }
            set
            {
                if (value != _fives)
                {
                    _fives = value;
                    OnPropertyChanged("Fives");
                }
            }
        }
        public int? Sixes
        {
            get { return _sixes; }
            set
            {
                if (value != _sixes)
                {
                    _sixes = value;
                    OnPropertyChanged("Sixes");
                }
            }
        }
        public int? UpperScore
        {
            get { return _upperScore; }
            set
            {
                if (value != _upperScore)
                {
                    _upperScore = value;
                    OnPropertyChanged("UpperScore");
                }
            }
        }
        public int? UpperBonus
        {
            get { return _upperBonus; }
            set
            {
                if (value != _upperBonus)
                {
                    _upperBonus = value;
                    OnPropertyChanged("UpperBonus");
                }
            }
        }

        public int? Pair
        {
            get { return _pair; }
            set
            {
                if (value != _pair)
                {
                    _pair = value;
                    OnPropertyChanged("Pair");
                }
            }
        }
        public int? TwoPairs
        {
            get { return _twoPairs; }
            set
            {
                if (value != _twoPairs)
                {
                    _twoPairs = value;
                    OnPropertyChanged("TwoPairs");
                }
            }
        }
        public int? ThreeOfaKind
        {
            get { return _threeOfaKind; }
            set
            {
                if (value != _threeOfaKind)
                {
                    _threeOfaKind = value;
                    OnPropertyChanged("ThreeOfaKind");
                }
            }
        }

        public int? FourOfaKind
        {
            get { return _fourOfaKind; }
            set
            {
                if (value != _fourOfaKind)
                {
                    _fourOfaKind = value;
                    OnPropertyChanged("FourOfaKind");
                }
            }
        }
        public int? SmalLadder
        {
            get { return _smalLadder; }
            set
            {
                if (value != _smalLadder)
                {
                    _smalLadder = value;
                    OnPropertyChanged("SmalLadder");
                }
            }
        }

        public int? LargeLadder
        {
            get { return _largeLadder; }
            set
            {
                if (value != _largeLadder)
                {
                    _largeLadder = value;
                    OnPropertyChanged("LargeLadder");
                }
            }
        }

        public int? FullHouse
        {
            get { return _fullHouse; }
            set
            {
                if (value != _fullHouse)
                {
                    _fullHouse = value;
                    OnPropertyChanged("FullHouse");
                }
            }
        }

        public int? Chance
        {
            get { return _chance; }
            set
            {
                if (value != _chance)
                {
                    _chance = value;
                    OnPropertyChanged("Chance");
                }
            }
        }
        public int? Yatzy
        {
            get { return _yatzy; }
            set
            {
                if (value != _yatzy)
                {
                    _yatzy = value;
                    OnPropertyChanged("Yatzy");
                }
            }
        }
        
        public int? TotalScore
        {
            get { return _totalScore; }
            set
            {
                if (value != _totalScore)
                {
                    _totalScore = value;
                    OnPropertyChanged("TotalScore");
                }
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Yatzy.Commands;

namespace Yatzy.Models
{
    class DicesViewModel : INotifyPropertyChanged
    {
        #region Kommandon
        public RelayCommand SaveDiceCommand { get; set; }
        public RelayCommand RollDicesCommand { get; set; }
        #endregion

        #region Properties
        private ObservableCollection<Dice> dices;

        public ObservableCollection<Dice> Dices
        {
            get { return dices; }
            set
            {
                dices = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Dices"));
            }
        }

        private ObservableCollection<Player> scoreCalc;

        public ObservableCollection<Player> ScoreCalc
        {
            get { return scoreCalc; }
            set { scoreCalc = value; OnPropertyChanged(new PropertyChangedEventArgs("ScoreCalc")); }
        }

        private Player player;

        public Player Player
        {
            get { return player; }
            set { player = value; OnPropertyChanged(new PropertyChangedEventArgs("Player")); }
        }


        #endregion

        #region Kalla på Game Engine och kolla poängkombinationer

        GameEngine gameEngine;

        private void GetGameEngine()
        {
            gameEngine = new GameEngine(Dices);
            GetScoreCombinations();
        }

        #endregion

        #region Changed Event Handler
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged (PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
            
        #endregion

        #region Konstruktor
        //Konstruktor där 5 objekt av typen Dice skapas och läggs i en lista. Får DiceID från 1 - 5, samt IsDiceEnabled = true.
        //DiceValue är tom tills tärningarna "kastas" med en metod nedanför.

        public DicesViewModel()
        {
            SaveDiceCommand = new RelayCommand(SaveDice, CanExecuteMethod);
            RollDicesCommand = new RelayCommand(RollDices, CanExecuteMethod);

            Dice dice;
            Dices = new ObservableCollection<Dice>();
            for (int i = 0; i < 5; i++)
            {
                dice = new Dice
                {
                    DiceID = i + 1,
                    IsDiceEnabled = true
                }; Dices.Add(dice);
            }
        }

        #endregion

        #region Metoder för att kasta/spara tärningar samt en bool för att godkänna att metod används
        //Metod som skickar in ett godkännande till Command att en metod kan användas. Komplicerat stuff.

        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }
        
        //Metod för att välja en tärning att spara genom att skifta värde på IsDiceEnabled

        public void SaveDice(object parameter)
        {
            
            //Måste hitta ett sätt att ge den där variabeln ett värde beroende på vald tärning i View
            int diceButtonValue = int.Parse(parameter.ToString());
            for (int i = 0; i < Dices.Count; i++)
            {
                if (Dices[i].DiceID == diceButtonValue && Dices[i].IsDiceEnabled)
                {
                    Dices[i].IsDiceEnabled = false;
                }
                else if (Dices[i].IsDiceEnabled == false && Dices[i].DiceID == diceButtonValue)
                {
                    Dices[i].IsDiceEnabled = true;
                }
            }
        }

        //Metod för att kasta tärningen. Alla tärningar där IsDiceEnabled = true får ett nytt DiceValue

        public void RollDices(object parameter)
        {

            Random random = new Random();
            for (int i = 0; i < Dices.Count; i++)
            {
                if (Dices[i].IsDiceEnabled)
                {
                    
                    int rand = random.Next(1, 7);

                    Dices[i].DiceValue = rand;
                }

            }
            GetGameEngine();
        }

        #endregion

        #region Metod för att skicka alla tillgängliga poängkombinationer baserat på kastet

        

        public void GetScoreCombinations()
        {
            //På agendan - lägga till alla poster här nedanför in i listan ScoreCalc för att summera alla tillgängliga poäng

            Player = new Player();
            ScoreCalc = new ObservableCollection<Player>();


            Player.Ones = gameEngine.GetUpperScore(1);


            Player.Twos = gameEngine.GetUpperScore(2);


            Player.Threes = gameEngine.GetUpperScore(3);


            Player.Fours = gameEngine.GetUpperScore(4);


            Player.Fives = gameEngine.GetUpperScore(5);


            Player.Sixes = gameEngine.GetUpperScore(6);


            Player.Pair = gameEngine.GetPair();


            Player.TwoPairs = gameEngine.GetTwoPairs();


            Player.ThreeOfaKind = gameEngine.GetThreeOfAKind();


            Player.FourOfaKind = gameEngine.GetFourOfAKind();


            Player.SmalLadder = gameEngine.GetSmallLadder();


            Player.LargeLadder = gameEngine.GetLargeLadder();


            Player.FullHouse = gameEngine.GetFullHouse();


            Player.Chance = gameEngine.GetChance();


            Player.Yatzy = gameEngine.GetYatzy();


        }

    }

    #endregion
}


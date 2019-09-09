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
                OnPropertyChanged("Dices");
            }
        }

        private ObservableCollection<Player> scoreCalc;

        public ObservableCollection<Player> ScoreCalc
        {
            get { return scoreCalc; }
            set { scoreCalc = value; OnPropertyChanged("ScoreCalc"); }
        }

        #endregion

        #region Kalla på Game Engine och kolla poängkombinationer

        GameEngine gameEngine;

        private void GetScoreCombinations()
        {
            gameEngine = new GameEngine(Dices);
            
        }

        #endregion

        #region Changed Event Handler
        public event PropertyChangedEventHandler PropertyChanged;
                protected void OnPropertyChanged (string PropertyName)
                {
                    PropertyChangedEventHandler handler = PropertyChanged;
                    if (handler != null)
                        handler(this, new PropertyChangedEventArgs(PropertyName));
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
            GetScoreCombinations();
        }

        #endregion

        #region Metod för att skicka alla tillgängliga poängkombinationer baserat på kastet

        Player activePlayer;

        public void SetScore()
        {
            //På agendan - lägga till alla poster här nedanför in i listan ScoreCalc för att summera alla tillgängliga poäng

            activePlayer = new Player();
            ScoreCalc = new ObservableCollection<Player>();

            activePlayer.Ones = gameEngine.GetUpperScore(1);


            activePlayer.Twos = gameEngine.GetUpperScore(2);


            activePlayer.Threes = gameEngine.GetUpperScore(3);


            activePlayer.Fours = gameEngine.GetUpperScore(4);


            activePlayer.Fives = gameEngine.GetUpperScore(5);


            activePlayer.Sixes = gameEngine.GetUpperScore(6);


            activePlayer.Pair = gameEngine.GetPair();


            activePlayer.Sixes = gameEngine.GetTwoPairs();


            activePlayer.Sixes = gameEngine.GetThreeOfAKind();


            activePlayer.Sixes = gameEngine.GetFourOfAKind();


            activePlayer.Sixes = gameEngine.GetSmallLadder();


            activePlayer.Sixes = gameEngine.GetLargeLadder();


            activePlayer.Sixes = gameEngine.GetFullHouse();


            activePlayer.Sixes = gameEngine.GetChance();


            activePlayer.Sixes = gameEngine.GetYatzy();


        }

    }

    #endregion
}
}

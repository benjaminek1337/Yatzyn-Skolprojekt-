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
using Yatzy.GameEngine;

namespace Yatzy.Models
{
    class DicesViewModel : INotifyPropertyChanged
    {
        #region Objekt och lokala variabler
        PlayerEngine playerEngine;
        Player activePlayer;
        GameEngine gameEngine;
        private int count = 0;
        #endregion

        #region Properties 
        public RelayCommand SaveDiceCommand { get; set; }
        public RelayCommand RollDicesCommand { get; set; }
        public RelayCommand ChooseScoreCategoryCommand { get; set; }

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

        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; OnPropertyChanged(new PropertyChangedEventArgs("Player")); }
        }

        #endregion

        #region Kalla på Game Engine och kolla poängkombinationer

        

        private void GetGameEngine()
        {
            gameEngine = new GameEngine(Dices, activePlayer);
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
        //Kommandon instanseras även

        public DicesViewModel()
        {
            SaveDiceCommand = new RelayCommand(SaveDice, CanExecuteMethod);
            RollDicesCommand = new RelayCommand(RollDices, IsTriesEnabled);
            ChooseScoreCategoryCommand = new RelayCommand(ChooseScoreCategory, IsCategoryEnabled);

            playerEngine = new PlayerEngine();
            Player = new Player();
            

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

            GetGameEngine();
            GetActivePlayer();
            Player.Firstname = activePlayer.Firstname; //NDISDILFSLDIUFHSUILSUILDGHSUILDFHSILDUFHSDUILFHLSIDUHFLSIUDFHSDUILFHs
            Player.TotalScore = activePlayer.TotalScore;
        }

        #endregion

        #region TESTMETOD FÖR ATT STARTA SPEL!!! TA BORT SEN!!!!!!! HELP FACK PLS SEND WIZARD!!



        private void GetActivePlayer()
        {
            activePlayer = playerEngine.GetActivePlayer();
        }

        #endregion

        #region Metoder för att kasta/spara/rensa tärningar samt en bool för att godkänna att metod används
        //Metod som skickar bool-värdet true till kommandot

        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }
        
        //Metod för att välja en tärning att spara genom att skifta värde på IsDiceEnabled

        private void SaveDice(object parameter)
        {           
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

        //Metod för att kasta tärningen. Alla tärningar där IsDiceEnabled = true får ett nytt DiceValue, samt en bool som spärrar tärningen efter 3 slag

        private bool IsTriesEnabled(object parameter)
        {
            if (count < 3)
            {
                return true;
            }

            return false;
        }

        private void RollDices(object parameter)
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
            count++;
            GetScoreCombinations();      
        }

        private void ResetDices()
        {
            count = 0;
            for (int i = 0; i < Dices.Count; i++)
            {
                Dices[i].DiceValue = 0;
                Dices[i].IsDiceEnabled = true;
            }
            ResetPlayer();
        }

        #endregion

        #region Metod för att välja en poängkategori, samt metod för att godkänna valet och nolla tärningarna

        private void ChooseScoreCategory(object parameter)
        {            
            activePlayer.TotalScore += int.Parse(parameter.ToString());
            //Fixa mer metoder för att spara poängkategori på spelare, totalpoäng samt byta till nästa spelare

            ResetDices();
            GetGameEngine();
            playerEngine.SetActivePlayer(); // TA BORT, BARA FÖR TESTNING

            GetActivePlayer();
            Player.Firstname = activePlayer.Firstname;
            Player.TotalScore = activePlayer.TotalScore;
        }

        private bool IsCategoryEnabled(object parameter)
        {
            

            return true;
        }
      

        private void ResetPlayer()
        {
            GetScoreCombinations();
        }
        #endregion

        #region Metod för att skicka alla tillgängliga poängkombinationer baserat på kastet



        public void GetScoreCombinations()
        {
            gameEngine.DiceCount();

            //Player = new Player();
            
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

        #endregion

        public void SetTotalUpperScore()
        {
            Player.UpperScore = Player.Ones
                + Player.Twos
                + Player.Threes
                + Player.Fours
                + Player.Fives
                + Player.Sixes;
        }



        public void SetBonus() //Ska vi lägga upperBonusLevel som indataparameter istället???
        {
            int upperBonusLevel = 63;
            int totalUpperScore = Player.Ones
                + Player.Twos
                + Player.Threes
                + Player.Fours
                + Player.Fives
                + Player.Sixes;

            if (totalUpperScore >= upperBonusLevel)
            {
                Player.UpperBonus = 50;
            }
        }

        public void SetTotalScore()
        {
            Player.TotalScore = Player.UpperScore
                + Player.UpperBonus
                + Player.Pair
                + Player.TwoPairs
                + Player.ThreeOfaKind
                + Player.FourOfaKind
                + Player.SmalLadder
                + Player.LargeLadder
                + Player.FullHouse
                + Player.Chance
                + Player.Yatzy;
        }

    }

   
}


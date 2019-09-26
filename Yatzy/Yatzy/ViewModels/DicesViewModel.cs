using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using Yatzy.Commands;
using Yatzy.DBOps;
using Yatzy.GameEngine;
using Yatzy.ViewModels;
using Yatzy.Views;

namespace Yatzy.Models
{
    class DicesViewModel : INotifyPropertyChanged
    {

        #region Objekt och lokala variabler
        PlayerEngine playerEngine;
        GameEngine gameEngine;
        DbOperations dbOps;
        PlayGameView pgv;
        SoundPlayer sPlayer;
        SoundPlayer sEffects;
        
        ObservableCollection<Dice> diceImages;

        ObservableCollection<Dice> DiceImages()
        {
            BitmapImage diceImage1 = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/dice1.png", UriKind.RelativeOrAbsolute));
            BitmapImage diceImage2 = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/dice2.png", UriKind.RelativeOrAbsolute));
            BitmapImage diceImage3 = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/dice3.png", UriKind.RelativeOrAbsolute));
            BitmapImage diceImage4 = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/dice4.png", UriKind.RelativeOrAbsolute));
            BitmapImage diceImage5 = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/dice5.png", UriKind.RelativeOrAbsolute));
            BitmapImage diceImage6 = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/dice6.png", UriKind.RelativeOrAbsolute));

            


            diceImages = new ObservableCollection<Dice>();
            diceImages.Add(new Dice()
            {
                DiceImage = diceImage1
            });

            diceImages.Add(new Dice()
            {
                DiceImage = diceImage2
            });

            diceImages.Add(new Dice()
            {
                DiceImage = diceImage3
            });

            diceImages.Add(new Dice()
            {
                DiceImage = diceImage4
            });

            diceImages.Add(new Dice()
            {
                DiceImage = diceImage5
            });

            diceImages.Add(new Dice()
            {
                DiceImage = diceImage6
            });
            return diceImages;
        }

        private bool gameEnded;
        private int throwsLeft = 0;
        private int count = 0;
        private int rounds = 0;
        private int gameType = 0;
        System.Timers.Timer timer1;
        System.Timers.Timer timer2;

        #endregion

        #region Properties 
        public RelayCommand SaveDiceCommand { get; set; }
        public RelayCommand RollDicesCommand { get; set; }
        public RelayCommand ChooseScoreCategoryCommand { get; set; }
        public RelayCommand Ones { get; set; }
        public RelayCommand Twos { get; set; }
        public RelayCommand Threes { get; set; }
        public RelayCommand Fours { get; set; }
        public RelayCommand Fives { get; set; }
        public RelayCommand Sixes { get; set; }
        public RelayCommand Pair { get; set; }
        public RelayCommand TwoPair { get; set; }
        public RelayCommand Threeofakind { get; set; }
        public RelayCommand Fourofakind { get; set; }
        public RelayCommand Smallstraight { get; set; }
        public RelayCommand Largestraight { get; set; }
        public RelayCommand Fullhouse { get; set; }
        public RelayCommand Chance { get; set; }
        public RelayCommand Yatzy { get; set; }
        public RelayCommand QuitGameCommand { get; set; }


        private ObservableCollection<Dice> dices;
        public ObservableCollection<Dice> Dices
        {
            get { return dices; }
            set { dices = value; OnPropertyChanged(new PropertyChangedEventArgs("Dices")); }
        }

        private ObservableCollection<Player> activePlayers;
        public ObservableCollection<Player> ActivePlayers
        {
            get { return activePlayers; }
            set { activePlayers = value; OnPropertyChanged(new PropertyChangedEventArgs("ActivePlayers")); }
        }

        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; OnPropertyChanged(new PropertyChangedEventArgs("Player")); }
        }

        private Player _activePlayer;
        public Player ActivePlayer
        {
            get { return _activePlayer; }
            set { _activePlayer = value; OnPropertyChanged(new PropertyChangedEventArgs("ActivePlayer")); }
        }

        private string _throwsLeft;
        public string ThrowsLeft
        {
            get { return ("Du har " + _throwsLeft + " kast kvar"); }
            set { _throwsLeft = value; OnPropertyChanged(new PropertyChangedEventArgs("ThrowsLeft")); }
        }


        #endregion

        #region Instansera en ny Game Engine och skicka in tärningarna, aktiva spelaren och speltypen

        private void GetGameEngine()
        {
            gameEngine = new GameEngine(Dices, ActivePlayer, gameType);
        }

        #endregion

        #region Changed Event Handler
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion

        #region Konstruktor

        public DicesViewModel(PlayerEngine _playerEngine)
        {
            Player = new Player();
            playerEngine = _playerEngine;
            dbOps = new DbOperations();
            
            gameType = playerEngine.SetGameType();
            ActivePlayers = playerEngine.SetPlayers();
            ActivePlayer = playerEngine.SetActivePlayer();
            GenerateDices();
            GetGameEngine();
            DiceImages();
            SetWarningTimer();
            SetEndTimer();

            pgv = new PlayGameView(0);
            throwsLeft = 3;
            gameEnded = false;
            SetThrowsLeft(throwsLeft);

            SaveDiceCommand = new RelayCommand(SaveDice, CanSaveDices);
            RollDicesCommand = new RelayCommand(RollDices, IsTriesEnabled);
            Ones = new RelayCommand(ChooseScoreCategory, IsOnesEnabled);
            Twos = new RelayCommand(ChooseScoreCategory, IsTwosEnabled);
            Threes = new RelayCommand(ChooseScoreCategory, IsThreesEnabled);
            Fours = new RelayCommand(ChooseScoreCategory, IsFoursEnabled);
            Fives = new RelayCommand(ChooseScoreCategory, IsFivesEnabled);
            Sixes = new RelayCommand(ChooseScoreCategory, IsSixesEnabled);
            Pair = new RelayCommand(ChooseScoreCategory, IsPairEnabled);
            TwoPair = new RelayCommand(ChooseScoreCategory, IsTwoPairEnabled);
            Threeofakind = new RelayCommand(ChooseScoreCategory, IsThreeOfaKindEnabled);
            Fourofakind = new RelayCommand(ChooseScoreCategory, IsFourOfaKindEnabled);
            Smallstraight = new RelayCommand(ChooseScoreCategory, IsSmalLadderEnabled);
            Largestraight = new RelayCommand(ChooseScoreCategory, IsLargeLadderEnabled);
            Fullhouse = new RelayCommand(ChooseScoreCategory, IsFullHouseEnabled);
            Chance = new RelayCommand(ChooseScoreCategory, IsChanceEnabled);
            Yatzy = new RelayCommand(ChooseScoreCategory, IsYatzyEnabled);
            QuitGameCommand = new RelayCommand(QuitGame, CanExecuteMethod);
        }

        #endregion

        #region Metoder för att kasta/spara/rensa tärningar samt en bool för att godkänna att metod används

        //Metod för att generera tärningen
        private void GenerateDices()
        {
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

        private void RollDices(object parameter)
        {
            Random random = new Random();

            for (int i = 0; i < Dices.Count; i++)
            {
                if (Dices[i].IsDiceEnabled)
                {
                    int rand = random.Next(1, 7);
                    Dices[i].DiceValue = rand;
                    Dices[i].DiceImage = diceImages[rand - 1].DiceImage;
                }

            }
            throwsLeft--;
            SetThrowsLeft(throwsLeft);
            DiceSound();
            count++;
            gameEngine.SetGameEngineDices(Dices);
            GetScoreCombinations();
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

        private bool CanSaveDices(object parameter)
        {
            for (int i = 0; i < Dices.Count; i++)
            {
                if (Dices[i].DiceValue != 0)
                {
                    return true;
                }
            }
            return false;
        }

        //Metod för att tillåta kast
        private bool IsTriesEnabled(object parameter)
        {
            if (count < 3)
            {
                return true;
            }

            return false;
        }

        //Metod för att återställa tärningarnas värde till 0 inför ny runda
        private void ResetDices()
        {
            count = 0;
            for (int i = 0; i < Dices.Count; i++)
            {
                Dices[i].DiceValue = 0;
                Dices[i].IsDiceEnabled = true;
                Dices[i].DiceImage = null;
            }
            throwsLeft = 3;
            SetThrowsLeft(throwsLeft);
            pgv = new PlayGameView(1);
        }

        //Sätt värde på Propen ThrowsLeft för att visa kvarvarande kast i gränssnitt
        private void SetThrowsLeft(int _throwsLeft)
        {
            ThrowsLeft = _throwsLeft.ToString();
        }

        #endregion

        #region Metoder gällande tidtagning

        private void SetWarningTimer()
        {
            timer1 = new System.Timers.Timer(6300000);
            timer1.Elapsed += Timer1_Elapsed;
            timer1.Enabled = true;
        }

        private void SetEndTimer()
        {
            timer2 = new System.Timers.Timer(7200000);
            timer2.Elapsed += Timer2_Elapsed;
            timer2.Enabled = true;
        }

        private void Timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer2.Stop();
            dbOps.AbortGameTransaction(activePlayers[0].GameId);

            MessageBox.Show("Tiden har gått ut, spelet avslutas.");
            SelectedViewModel = new MainMenuViewModel();
            gameEngine.NullProps();
            playerEngine.NullProps();
        }

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            MessageBox.Show("Nu har ni 15 minuter på er att avsluta spelet");
            timer1.Stop();
        }
        #endregion

        #region Metoder för ljud
        private void StartGameMusic()
        {
            sPlayer = new SoundPlayer();
            sPlayer.Stream = Properties.Resources.Gamemusic; ;
            sPlayer.PlayLooping();


        }

        public void EndGameMusic()
        {
            sPlayer.Stop();
        }

        public void DiceSound()
        {
            sEffects = new SoundPlayer();
            sEffects.Stream = Properties.Resources.DiceThrow;
            sEffects.Play();
        }
        public void VictorySound()
        {
            sEffects = new SoundPlayer();
            sEffects.Stream = Properties.Resources.Flawless;
            sEffects.Play();
        }
        #endregion

        #region Metod för att välja en poängkategori och metod för att avgöra hur många rundor som är kvar.

        private void ChooseScoreCategory(object parameter)
        {
            if (int.Parse(parameter.ToString()) == 1)
                ActivePlayer.Ones = Player.Ones;
            if (int.Parse(parameter.ToString()) == 2)
                ActivePlayer.Twos = Player.Twos;
            if (int.Parse(parameter.ToString()) == 3)
                ActivePlayer.Threes = Player.Threes;
            if (int.Parse(parameter.ToString()) == 4)
                ActivePlayer.Fours = Player.Fours;
            if (int.Parse(parameter.ToString()) == 5)
                ActivePlayer.Fives = Player.Fives;
            if (int.Parse(parameter.ToString()) == 6)
                ActivePlayer.Sixes = Player.Sixes;
            if (int.Parse(parameter.ToString()) == 7)
                ActivePlayer.Pair = Player.Pair;
            if (int.Parse(parameter.ToString()) == 8)
                ActivePlayer.TwoPairs = Player.TwoPairs;
            if (int.Parse(parameter.ToString()) == 9)
                ActivePlayer.ThreeOfaKind = Player.ThreeOfaKind;
            if (int.Parse(parameter.ToString()) == 10)
                ActivePlayer.FourOfaKind = Player.FourOfaKind;
            if (int.Parse(parameter.ToString()) == 11)
                ActivePlayer.SmalLadder = Player.SmalLadder;
            if (int.Parse(parameter.ToString()) == 12)
                ActivePlayer.LargeLadder = Player.LargeLadder;
            if (int.Parse(parameter.ToString()) == 13)
                ActivePlayer.FullHouse = Player.FullHouse;
            if (int.Parse(parameter.ToString()) == 14)
                ActivePlayer.Chance = Player.Chance;
            if (int.Parse(parameter.ToString()) == 15)
                ActivePlayer.Yatzy = Player.Yatzy;

            GetGameEngine();
            gameEngine.SetUpperScore();
            gameEngine.SetTotalScore();
            ResetDices();
            RoundsLeft();
            GetScoreCombinations();
            ActivePlayer = playerEngine.SetActivePlayer();
            if (rounds == 15)
            {
                GameEnded();
            }
        }

        private void RoundsLeft()
        {
            if (ActivePlayer == ActivePlayers[ActivePlayers.Count - 1])
            {
                rounds += 1;
            }
        }

        #endregion

        #region Bools för att avgöra om en kategori kan användas eller inte beroende på speltyp

        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }

        private bool IsGameTypeStyrd()
        {
            if (gameType == 5)
            {
                return true;
            }
            else
                return false;
        }

        private bool IsOnesEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.Ones != null)
                    return false;
                else
                    return true;
            }
        }
        private bool IsTwosEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.Twos != null)
                    return false;
                else
                    return true;
            }
        }
        private bool IsThreesEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.Threes != null)
                    return false;
                else
                    return true;
            }
        }
        private bool IsFoursEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.Fours != null)
                    return false;
                else
                    return true;
            }

        }
        private bool IsFivesEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.Fives != null)
                    return false;
                else
                    return true;
            }
        }
        private bool IsSixesEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.Sixes != null)
                    return false;
                else
                    return true;
            }
        }
        private bool IsPairEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.Pair != null)
                    return false;
                else
                    return true;
            }
        }
        private bool IsTwoPairEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.TwoPairs != null)
                    return false;
                else
                    return true;
            }
        }
        private bool IsThreeOfaKindEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.ThreeOfaKind != null)
                    return false;
                else
                    return true;
            }
        }
        private bool IsFourOfaKindEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.FourOfaKind != null)
                    return false;
                else
                    return true;
            }
        }
        private bool IsSmalLadderEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.SmalLadder != null)
                    return false;
                else
                    return true;
            }

        }
        private bool IsLargeLadderEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.LargeLadder != null)
                    return false;
                else
                    return true;
            }

        }
        private bool IsFullHouseEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.FullHouse != null)
                    return false;
                else
                    return true;
            }

        }
        private bool IsChanceEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.Chance != null)
                    return false;
                else
                    return true;
            }

        }
        private bool IsYatzyEnabled(object parameter)
        {
            if (IsGameTypeStyrd())
            {
                if (int.Parse(parameter.ToString()) == rounds + 1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (ActivePlayer.Yatzy != null)
                    return false;
                else
                    return true;
            }

        }
        #endregion

        #region Metod för att visa alla tillgängliga poängkombinationer baserat på tärningarna

        private void GetScoreCombinations()
        {
            gameEngine.DiceCount();

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

        #region Metoder för när spelet avslutas

        private object selectedViewModel;
        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged(new PropertyChangedEventArgs("SelectedViewModel")); }
        }

        private void QuitGame(object parameter)
        {
            if (MessageBox.Show("Vill du avsluta spelet?", "Avsluta spel", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //rutan stängs ned här
            }
            else
            {
                if (gameEnded == false)
                    dbOps.AbortGameTransaction(ActivePlayers[0].GameId);

                gameEngine.NullProps();
                playerEngine.NullProps();

                SelectedViewModel = new MainMenuViewModel();
            }
        }

        private void GameEnded()
        {
            gameEnded = true;
            List<Player> Results = new List<Player>();
            for (int i = 0; i < ActivePlayers.Count; i++)
            {
                Results = ActivePlayers.ToList<Player>();
                //Results.OrderBy(ActivePlayer => ActivePlayer.TotalScore).ToList();
            }

            var orderByResult = from r in Results
                                orderby r.TotalScore descending
                                select r;
            VictorySound();
            MessageBox.Show(orderByResult.First().Firstname.ToString() + " vann med " + orderByResult.First().TotalScore.ToString() + " poäng");
            dbOps.SaveGameTransaction(ActivePlayers);




            if (MessageBox.Show("Spelet är slut, vill du avsluta?", "Avsluta spel", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //rutan stängs ned här
            }
            else
            {
                gameEngine.NullProps();
                playerEngine.NullProps();

                SelectedViewModel = new MainMenuViewModel();
            }

        }

        #endregion

    }
}
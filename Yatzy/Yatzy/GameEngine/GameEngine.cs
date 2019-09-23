using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Models
{
    class GameEngine
    {
        Player activePlayer;
        private ObservableCollection<Dice> Dices;      
        public int[] ArrayofCountedDices = new int[6];
        int gameType = 0;


        public GameEngine(ObservableCollection<Dice> _dices, Player _activePlayer, int _gameType)
        {
            gameType = _gameType;
            activePlayer = new Player();
            activePlayer = _activePlayer;
            Dices = new ObservableCollection<Dice>();
            Dices = _dices;

            DiceCount();
        }

        public void NullProps()
        {
            activePlayer = null;
            Dices = null;
        }

        /// <summary>
        /// Konverterar tärningarna i en lista till en array med antal tärningar i varje kategori
        /// </summary>
        public void DiceCount()
        {
            Array.Clear(ArrayofCountedDices, 0, ArrayofCountedDices.Length);

            for (int i = 0; i < 5; i++)
            {
                if (Dices[i].DiceValue == 1)
                {
                    ArrayofCountedDices[0] += 1;
                }
                if (Dices[i].DiceValue == 2)
                {
                    ArrayofCountedDices[1] += 1;
                }
                if (Dices[i].DiceValue == 3)
                {
                    ArrayofCountedDices[2] += 1;
                }
                if (Dices[i].DiceValue == 4)
                {
                    ArrayofCountedDices[3] += 1;
                }
                if (Dices[i].DiceValue == 5)
                {
                    ArrayofCountedDices[4] += 1;
                }
                if (Dices[i].DiceValue == 6)
                {
                    ArrayofCountedDices[5] += 1;
                }
            }
        }
        #region Publika metoder som hämtar poäng från varje kategori
        /// <summary>
        /// Räknar ut poängen för kategori 1-6
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public int GetUpperScore(int category)
        {
            int sum = 0;

            for (int i = 0; i < Dices.Count; i++)
            {
                if (Dices[i].DiceValue == category)
                {
                    sum += category;
                }
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för det par som har högst värde
        /// </summary>
        /// <returns></returns>
        public int GetPair()
        {
            int sum = 0;
            for (int i = 5; i >= 0; i--)
            {
                if (ArrayofCountedDices[i] >= 2)
                {
                    sum = (i + 1) * 2;
                    break;
                }
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för två par och kontrollerar att inte paren har samma värde
        /// </summary>
        /// <returns></returns>
        public int GetTwoPairs()
        {
            int sum = 0;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (ArrayofCountedDices[j] >= 2 && ArrayofCountedDices[i] >= 2 && i != j)
                    {
                        sum = (i + 1 + j + 1) * 2;
                        break;
                    }
                }
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för tretal
        /// </summary>
        /// <returns></returns>
        public int GetThreeOfAKind()
        {
            int sum = 0;
            for (int i = 0; i < 6; i++)
            {
                if (ArrayofCountedDices[i] >= 3)
                {
                    sum = (i + 1) * 3;
                    break;
                }
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för fyrtal
        /// </summary>
        /// <returns></returns>
        public int GetFourOfAKind()
        {
            int sum = 0;
            for (int i = 0; i < 6; i++)
            {
                if (ArrayofCountedDices[i] >= 4)
                {
                    sum = (i + 1) * 4;
                    break;
                }
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för liten stege (Kombinationen 1,2,3,4,5)
        /// </summary>
        /// <returns></returns>
        public int GetSmallLadder()
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
            {
                if (ArrayofCountedDices[i] == 1)
                {
                    sum += i + 1;
                }
            }
            if (sum == 15)
            {
                return sum;
            }
            return 0;
        }
        /// <summary>
        /// Räknar ut poängen för stor stege (Kombinationen 2,3,4,5,6)
        /// </summary>
        /// <returns></returns>
        public int GetLargeLadder()
        {
            int sum = 0;
            for (int i = 1; i < 6; i++)
            {
                if (ArrayofCountedDices[i] == 1)
                {
                    sum += i + 1;
                }
            }
            if (sum == 20)
            {
                return sum;
            }
            return 0;
        }
        /// <summary>
        /// Räknar ut poängen för Kåk (Ett par och ett tretal)
        /// </summary>
        /// <returns></returns>
        public int GetFullHouse()
        {
            int sum = 0;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (ArrayofCountedDices[i] == 2 && ArrayofCountedDices[j] == 3)
                    {
                        sum = (i + 1) * 2 + (j + 1) * 3;
                        break;
                    }
                }
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för Chans (Alla 5 tärningarna räknas ihop oavsett värde)
        /// </summary>
        /// <returns></returns>
        public int GetChance()
        {
            int sum = 0;

            for (int i = 0; i < Dices.Count; i++)
            {
                sum += Dices[i].DiceValue;
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för Yatzy (Alla tärningar har samma värde)
        /// </summary>
        /// <returns></returns>
        public int GetYatzy() 
        {
            int sum = 0;
            for (int i = 1; i < 5; i++)
            {
                if (Dices[i].DiceValue != Dices[0].DiceValue) //Kontrollerar om tärning 2-5 är olik tärning 1 och returnerar i så fall 0
                {
                    sum = 0;
                    break;
                }
                else if(Dices[i].DiceValue > 0)
                {
                    sum = 50;
                }

            }
            return sum;
            
        }
        #endregion

        #region Publika metoder som sätter poäng för varje kategori
        /// <summary>
        /// Tilldelar aktiv spelare poäng i varje kategori
        /// </summary>
        /// <param name="category"></param>
        public void SetScore(int category)
        {
            switch (category)
            {
                case 1:
                    activePlayer.Ones = GetUpperScore(1);
                    break;
                case 2:
                    activePlayer.Twos = GetUpperScore(2);
                    break;
                case 3:
                    activePlayer.Threes = GetUpperScore(3);
                    break;
                case 4:
                    activePlayer.Fours = GetUpperScore(4);
                    break;
                case 5:
                    activePlayer.Fives = GetUpperScore(5);
                    break;
                case 6:
                    activePlayer.Sixes = GetUpperScore(6);
                    break;
                case 7:
                    activePlayer.Pair = GetPair();
                    break;
                case 8:
                    activePlayer.TwoPairs = GetTwoPairs();
                    break;
                case 9:
                    activePlayer.ThreeOfaKind = GetThreeOfAKind();
                    break;
                case 10:
                    activePlayer.FourOfaKind = GetFourOfAKind();
                    break;
                case 11:
                    activePlayer.SmalLadder = GetSmallLadder();
                    break;
                case 12:
                    activePlayer.LargeLadder = GetLargeLadder();
                    break;
                case 13:
                    activePlayer.FullHouse = GetFullHouse();
                    break;
                case 14:
                    activePlayer.Chance = GetChance();
                    break;
                case 15:
                    activePlayer.Yatzy= GetYatzy();
                    break;

                default:
                    break;
            }
        }
        #endregion


        #region Metoder för att sätta totalpoäng, samt beräkna de "övre" kategorierna och sätta bonus

        public void SetUpperScore()
        {
            int?[] upperScoreArray = new int?[6];
            int? upperScore = 0;

            upperScoreArray[0] = activePlayer.Ones;
            upperScoreArray[1] = activePlayer.Twos;
            upperScoreArray[2] = activePlayer.Threes;
            upperScoreArray[3] = activePlayer.Fours;
            upperScoreArray[4] = activePlayer.Fives;
            upperScoreArray[5] = activePlayer.Sixes;


            for (int i = 0; i < upperScoreArray.Length; i++)
            {
                if (upperScoreArray[i] == null || upperScoreArray[1] == null || upperScoreArray[2] == null || upperScoreArray[3] == null || upperScoreArray[4] == null || upperScoreArray[5] == null)
                {
                    break;
                }
                else
                {
                    upperScore += upperScoreArray[i];
                    SetBonus(upperScore);
                }
            }
            activePlayer.UpperScore = upperScoreArray.Sum();
        }

        public void SetBonus(int? upperScore)
        {
            if(gameType == 5)
            {
                if (upperScore >= 42)
                    activePlayer.UpperBonus = 50;
                else
                    activePlayer.UpperBonus = 0;
            }
            else
            {
                if (upperScore >= 63)
                    activePlayer.UpperBonus = 50;
                else
                    activePlayer.UpperBonus = 0;
            }
        }

        public void SetTotalScore()
        {
           
            int?[] totalScoreArray = new int?[16];

            totalScoreArray[0] = activePlayer.Ones;
            totalScoreArray[1] = activePlayer.Twos;
            totalScoreArray[2] = activePlayer.Threes;
            totalScoreArray[3] = activePlayer.Fours;
            totalScoreArray[4] = activePlayer.Fives;
            totalScoreArray[5] = activePlayer.Sixes;
            totalScoreArray[6] = activePlayer.UpperBonus;
            totalScoreArray[7] = activePlayer.Pair;
            totalScoreArray[8] = activePlayer.TwoPairs;
            totalScoreArray[9] = activePlayer.ThreeOfaKind;
            totalScoreArray[10] = activePlayer.FourOfaKind;
            totalScoreArray[11] = activePlayer.SmalLadder;
            totalScoreArray[12] = activePlayer.LargeLadder;
            totalScoreArray[13] = activePlayer.FullHouse;
            totalScoreArray[14] = activePlayer.Chance;
            totalScoreArray[15] = activePlayer.Yatzy;

            activePlayer.TotalScore = totalScoreArray.Sum();
        }
        #endregion


    }
}

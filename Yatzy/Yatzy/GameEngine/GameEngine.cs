using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Models
{
    class GameEngine
    {
        #region Lokala objekt och variabler

        Player ActivePlayer;
        private ObservableCollection<Dice> Dices;      
        public int[] ArrayofCountedDices = new int[6];
        int gameType = 0;
        SoundPlayer sEffects;
        #endregion

        #region Konstruktor

        public GameEngine(ObservableCollection<Dice> _dices, Player _activePlayer, int _gameType)
        {
            gameType = _gameType;
            ActivePlayer = _activePlayer;
            Dices = _dices;
            DiceCount();
        }
        #endregion

        #region Metoder för att nolla properties vid slutet av spelet, samt en för att uppdatera listan Dices
        public void NullProps()
        {
            ActivePlayer = null;
            Dices = null;
        }

        public void SetGameEngineDices(ObservableCollection<Dice> _dices)
        {
            Dices = _dices;
        }
        #endregion

        #region Metod som sorterar tärningen till en array 
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
        #endregion

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
            Yatzysound(sum);
            return sum;
            
        }
        #endregion

        #region Metoder för ljudeffekter vid yatzy och bonus
        public void Yatzysound(int sum)
        {
            if (sum == 50)
            {
                sEffects = new SoundPlayer();
                sEffects.Stream = Properties.Resources.Yatzysound;
                sEffects.Play();
            }

        }





        #endregion 

        #region Metoder för att sätta totalpoäng, samt beräkna de "övre" kategorierna och sätta bonus

        public void SetUpperScore()
        {
            int?[] upperScoreArray = new int?[6];
            int? upperScore = 0;

            upperScoreArray[0] = ActivePlayer.Ones;
            upperScoreArray[1] = ActivePlayer.Twos;
            upperScoreArray[2] = ActivePlayer.Threes;
            upperScoreArray[3] = ActivePlayer.Fours;
            upperScoreArray[4] = ActivePlayer.Fives;
            upperScoreArray[5] = ActivePlayer.Sixes;


            for (int i = 0; i < upperScoreArray.Length; i++)
            {
                if (upperScoreArray[0] == null || upperScoreArray[1] == null || upperScoreArray[2] == null || 
                    upperScoreArray[3] == null || upperScoreArray[4] == null || upperScoreArray[5] == null)
                {
                    break;
                }
                else
                {
                    upperScore += upperScoreArray[i];
                    SetBonus(upperScore);
                }
            }
            
            ActivePlayer.UpperScore = upperScoreArray.Sum();

        }

        public void SetBonus(int? upperScore)
        {
            if(gameType == 2)
            {
                if (upperScore >= 42)
                {
                   
                    ActivePlayer.UpperBonus = 50;
                }
                else
                    ActivePlayer.UpperBonus = 0;
            }
            else
            {
                if (upperScore >= 63)
                {
                   
                    ActivePlayer.UpperBonus = 50;
                }
                else
                    ActivePlayer.UpperBonus = 0;
            }
        }

        public void SetTotalScore()
        {
           
            int?[] totalScoreArray = new int?[16];

            totalScoreArray[0] = ActivePlayer.Ones;
            totalScoreArray[1] = ActivePlayer.Twos;
            totalScoreArray[2] = ActivePlayer.Threes;
            totalScoreArray[3] = ActivePlayer.Fours;
            totalScoreArray[4] = ActivePlayer.Fives;
            totalScoreArray[5] = ActivePlayer.Sixes;
            totalScoreArray[6] = ActivePlayer.UpperBonus;
            totalScoreArray[7] = ActivePlayer.Pair;
            totalScoreArray[8] = ActivePlayer.TwoPairs;
            totalScoreArray[9] = ActivePlayer.ThreeOfaKind;
            totalScoreArray[10] = ActivePlayer.FourOfaKind;
            totalScoreArray[11] = ActivePlayer.SmalLadder;
            totalScoreArray[12] = ActivePlayer.LargeLadder;
            totalScoreArray[13] = ActivePlayer.FullHouse;
            totalScoreArray[14] = ActivePlayer.Chance;
            totalScoreArray[15] = ActivePlayer.Yatzy;

            ActivePlayer.TotalScore = totalScoreArray.Sum();
        }
        #endregion


    }
}

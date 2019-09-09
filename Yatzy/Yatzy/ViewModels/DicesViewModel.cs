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

        public RelayCommand SaveDiceCommand { get; set; }
        public RelayCommand RollDicesCommand { get; set; }

        //public ObservableCollection<Dice> Dices { get; set; }

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



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged (string PropertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }


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
            //MessageBox.Show("yes");
        }


    }
}

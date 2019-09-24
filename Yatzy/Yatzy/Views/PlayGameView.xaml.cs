using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yatzy.Models;
using Yatzy.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;
using Yatzy.GameEngine;

namespace Yatzy.Views
{
    /// <summary>
    /// Interaction logic for PlayGameView.xaml
    /// </summary>
    public partial class PlayGameView : UserControl
    {

        private bool OneEnabled = true;
        private bool TwoEnabled = true;
        private bool ThreeEnabled = true;
        private bool FourEnabled = true;
        private bool FiveEnabled = true;
       

        public PlayGameView()
        {
            InitializeComponent();
        }

        public PlayGameView(int reset)
        {
            //ResetDicePositions(reset);

            var sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            var sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            var sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            var sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            var sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            //Storyboard sbr1 = (this.FindName("RollDice1_Copy1") as Storyboard);
            //Storyboard sbr2 = (this.FindName("RollDice2") as Storyboard);
            //Storyboard sbr3 = (this.FindName("RollDice3") as Storyboard);
            //Storyboard sbr4 = (this.FindName("RollDice4") as Storyboard);
            //Storyboard sbr5 = (this.FindName("RollDice5") as Storyboard);
        }

        public void ResetDicePositions(int reset)
        {
            if (reset == 1)
            {
                OneEnabled = true;
                TwoEnabled = true;
                ThreeEnabled = true;
                FourEnabled = true;
                FiveEnabled = true;

                Grid.SetColumn(btnDice1, 1);
                Grid.SetColumn(btnDice2, 2);
                Grid.SetColumn(btnDice3, 3);
                Grid.SetColumn(btnDice4, 4);
                Grid.SetColumn(btnDice5, 5);

                Grid.SetRow(btnDice1, 5);
                Grid.SetRow(btnDice2, 5);
                Grid.SetRow(btnDice3, 5);
                Grid.SetRow(btnDice4, 5);
                Grid.SetRow(btnDice5, 5);

            }
        }
        
        private void BtnDice1_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (this.Resources["RollDice1"] as Storyboard);
            Storyboard sbr = (this.Resources["RollDice1_Copy1"] as Storyboard);
            if (OneEnabled)
            {
                sbr.Begin();
                OneEnabled = false;
            }
            else
            {
                sb.Begin();
                OneEnabled = true;
            }
        }

        private void BtnDice2_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (this.Resources["RollDice2"] as Storyboard);
            Storyboard sbr = (this.Resources["RollDice2_Copy1"] as Storyboard);
            if (TwoEnabled)
            {
                sbr.Begin();
                TwoEnabled = false;
            }
            else
            {
                sb.Begin();
                TwoEnabled = true;
            }
        }

        private void BtnDice3_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (this.Resources["RollDice3"] as Storyboard);
            Storyboard sbr = (this.Resources["RollDice3_Copy1"] as Storyboard);
            if (ThreeEnabled)
            {
                sbr.Begin();
                ThreeEnabled = false;
            }
            else
            {
                sb.Begin();
                ThreeEnabled = true;
            }
        }

        private void BtnDice4_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (this.Resources["RollDice4"] as Storyboard);
            Storyboard sbr = (this.Resources["RollDice4_Copy1"] as Storyboard);
            if (FourEnabled)
            {
                sbr.Begin();
                FourEnabled = false;
            }
            else
            {
                sb.Begin();
                FourEnabled = true;
            }
        }

        private void BtnDice5_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (this.Resources["RollDice5"] as Storyboard);
            Storyboard sbr = (this.Resources["RollDice5_Copy1"] as Storyboard);
            if (FiveEnabled)
            {
                sbr.Begin();
                FiveEnabled = false;
            }
            else
            {
                sb.Begin();
                FiveEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var sb1 = (this.Resources["RollDice1"] as Storyboard);
            var sb2 = (this.Resources["RollDice2"] as Storyboard);
            var sb3 = (this.Resources["RollDice3"] as Storyboard);
            var sb4 = (this.Resources["RollDice4"] as Storyboard);
            var sb5 = (this.Resources["RollDice5"] as Storyboard);

            if (OneEnabled)
            {
                OneEnabled = true;
                sb1.Begin();
                
            }
            if (TwoEnabled)
            {
                TwoEnabled = true;
                sb2.Begin();
                
            }
            if (ThreeEnabled)
            {
                ThreeEnabled = true;
                sb3.Begin();
                
            }
            if (FourEnabled)
            {
                FourEnabled = true;
                sb4.Begin();
               
            }
            if (FiveEnabled)
            {
                FiveEnabled = true;
                sb5.Begin();
                
            }
         }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            Storyboard sbr1 = (this.Resources["RollDice1_Copy1"] as Storyboard);
            Storyboard sbr2 = (this.Resources["RollDice2_Copy1"] as Storyboard);
            Storyboard sbr3 = (this.Resources["RollDice3_Copy1"] as Storyboard);
            Storyboard sbr4 = (this.Resources["RollDice4_Copy1"] as Storyboard);
            Storyboard sbr5 = (this.Resources["RollDice5_Copy1"] as Storyboard);

            sbr1.Begin();
            sbr2.Begin();
            sbr3.Begin();
            sbr4.Begin();
            sbr5.Begin();

            OneEnabled = true;
            TwoEnabled = true;
            ThreeEnabled = true;
            FourEnabled = true;
            FiveEnabled = true;
        }
    }
}

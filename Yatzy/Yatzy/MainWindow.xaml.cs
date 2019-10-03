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
using System.Media;
using Yatzy.ViewModels;
using Yatzy.DBOps;
using Yatzy.Models;
using Yatzy.GameEngine;

namespace Yatzy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DicesViewModel dicesViewModel;
        public MainWindow()
        {
            this.DataContext = new NavigationViewModel();
            dicesViewModel = new DicesViewModel();
            InitializeComponent();



            this.Closed += new EventHandler(MainWindow_Closed);

        }
        void MainWindow_Closed(object sender, EventArgs e)
        {
            dicesViewModel.ForcedQuitGame();
        }


    }
}

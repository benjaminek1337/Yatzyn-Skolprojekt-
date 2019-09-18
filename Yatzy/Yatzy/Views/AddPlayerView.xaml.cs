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
using System.Windows.Shapes;
using Yatzy.ViewModels;

namespace Yatzy.Views
{
    /// <summary>
    /// Interaction logic for AddPlayerView.xaml
    /// </summary>
    public partial class AddPlayerView : Window
    {
        //AddPlayerViewModel addPlayerViewModel;
        public AddPlayerView()
        {
            //addPlayerViewModel = new AddPlayerViewModel();
            //DataContext = addPlayerViewModel;
            InitializeComponent();
        }
    }
}

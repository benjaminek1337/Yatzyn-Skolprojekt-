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
using System.Media;
using System.Windows.Input;
using Yatzy.ViewModels;

namespace Yatzy.Views
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : Window //22.40 2019-09-06 detta är fyllekod nu :)))))
    {
        MainMenuViewModel menuViewModel;
        
        public MainMenuView()
        {
            menuViewModel = new MainMenuViewModel();
            this.DataContext = menuViewModel;
            InitializeComponent();
           
        }
   
    }
}

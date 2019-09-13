using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Yatzy.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Page, INotifyPropertyChanged
    {

        private ObservableCollection<int> code;
        public ObservableCollection<int> Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Code"));
            }            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public GameView()
        {
            InitializeComponent();
            //test.DataContext = this;
            code = new ObservableCollection<int>();
            code.Add(1);
            code.Add(2);
            code.Add(3);
            code.Add(4);
            code.Add(5);
            code.Add(6);
            code.Add(7);
            code.Add(8);
            code.Add(9);
            code.Add(10);
            code.Add(11);
            code.Add(12);
            code.Add(13);
            code.Add(14);
            code.Add(15);


            this.DataContext = this;
        }        
    }
}

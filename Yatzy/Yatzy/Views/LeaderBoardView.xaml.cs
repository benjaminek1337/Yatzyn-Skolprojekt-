﻿using System;
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
using Yatzy.ViewModels;

namespace Yatzy.Views
{
    /// <summary>
    /// Interaction logic for LeaderBoardView.xaml
    /// </summary>
    public partial class LeaderBoardView : UserControl
    {
        public LeaderBoardView()
        {
            this.DataContext = new LeaderBoardViewModel();
            InitializeComponent();
        }
    }
}

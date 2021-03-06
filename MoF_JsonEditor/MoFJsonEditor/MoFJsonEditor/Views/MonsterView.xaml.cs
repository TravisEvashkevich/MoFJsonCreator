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
using Microsoft.Win32;
using MoFJsonEditor.ViewModels;

namespace MoFJsonEditor.Views
{
    /// <summary>
    /// Interaction logic for Monster.xaml
    /// </summary>
    public partial class Monster : UserControl
    {
        public Monster()
        {
            InitializeComponent();
            DataContext = MainVm.Instance.MonsterVm;
        }

        private void OpenLootWindow(object sender, RoutedEventArgs e)
        {
            var lootWin = new MonsterLootSelection();
            lootWin.Show();
        }
    }
}

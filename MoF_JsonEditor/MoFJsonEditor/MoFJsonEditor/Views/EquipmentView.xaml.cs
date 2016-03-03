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
using Microsoft.Win32;
using MoFJsonEditor.Models;
using MoFJsonEditor.ViewModels;

namespace MoFJsonEditor.Views
{
    /// <summary>
    /// Interaction logic for EquipmentView.xaml
    /// </summary>
    public partial class EquipmentView : UserControl
    {
        private string _defaultPath = @"E:\BitBucket\OrderOfFitness\OrderOfFitness\Assets\Resources\";
        public EquipmentView()
        {
            InitializeComponent();
            DataContext = MainVm.Instance.EquipmentVm;
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.InitialDirectory = _defaultPath;
            var success = dlg.ShowDialog();
            if (success == true)
            {
                //Unity DOES NOT like \ so we just replace them with / instead.
                var editedPath = dlg.FileName.Replace("\\", "/");
                var lastSlash = editedPath.LastIndexOf("/");
                var path = editedPath.Substring(_defaultPath.Length, lastSlash - _defaultPath.Length);

                var index = editedPath.LastIndexOf("/") + 1;
                var periodIndex = editedPath.LastIndexOf(".");
                var name = editedPath.Substring(index, editedPath.Length - index - (editedPath.Length - periodIndex));
                MainVm.Instance.EquipmentVm.SetImagePathAndSpriteName(path, name);
                //set the text, it will update the 
                var tb = sender as TextBox;
                tb.Text = path;
            }
        }

        private void TextBox_CheckForNewText(object sender, MouseButtonEventArgs e)
        {
            //we're being lazy and hacky since we can't raise PropertyChanged we'll make a work around
            var tb = sender as TextBox;
            var name = MainVm.Instance.EquipmentVm.GetSpriteName();
            tb.Text = name;
        }
    }
}

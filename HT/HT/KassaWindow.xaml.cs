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
using HT.ViewModels;
using System.IO;
using MahApps.Metro.Controls;
namespace HT
{
    /// <summary>
    /// Interaction logic for KassaWindow.xaml
    /// </summary>
    public partial class KassaWindow : MetroWindow
    {
        KassaViewModel ViewModel;

        public KassaWindow(MyyntiViewModel myynti)
        {
            InitializeComponent();
            ViewModel = new KassaViewModel(myynti);
            DataContext = ViewModel;
        }

        private void Hyvaksy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

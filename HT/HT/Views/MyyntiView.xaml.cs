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
using HT.ViewModels;
using HT.Models;

namespace HT.Views
{
    /// <summary>
    /// Interaction logic for MyyntiView.xaml
    /// </summary>
    public partial class MyyntiView : UserControl
    {
        MyyntiViewModel ViewModel;

        public MyyntiView()
        {
            InitializeComponent();
            DataContextChanged += new DependencyPropertyChangedEventHandler(View_DataContextChanged);
        }

        void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel = DataContext as MyyntiViewModel;
        }

        private void Kassalle_Click(object sender, RoutedEventArgs e)
        {
            //Avaa kassa ikkuna ja lähetä se ostoskorin tämänhetkiseen myynti tilaan
            KassaWindow kassaWindow = new KassaWindow(ViewModel);
            kassaWindow.ShowDialog();
        }
    }
}

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
    /// Interaction logic for HistoriaView.xaml
    /// </summary>
    public partial class HistoriaView : UserControl
    {
        HistoriaViewModel ViewModel;

        public HistoriaView()
        {
            InitializeComponent();
            DataContextChanged += new DependencyPropertyChangedEventHandler(View_DataContextChanged);
        }
        void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel = DataContext as HistoriaViewModel;
        }

        private void Liiketoimi_Selected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var listview = sender as ListView;
                var tuote = listview.SelectedItems[0] as Liiketoimi;
                ViewModel.SelectLiiketoimi(tuote);
            }
            catch (Exception)
            {
            }
        }
    }
}

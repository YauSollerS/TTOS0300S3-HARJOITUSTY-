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

namespace HT.Views
{
    /// <summary>
    /// Interaction logic for TuotteetView.xaml
    /// </summary>
    public partial class TuotteetView : UserControl
    {
        TuotteetViewModel ViewModel;

        public TuotteetView()
        {
            InitializeComponent();
            DataContextChanged += new DependencyPropertyChangedEventHandler(View_DataContextChanged);
        }

        //Aseta 'ViewModel'
        void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Aseta viewmodel tieto kontekstiin
            ViewModel = DataContext as TuotteetViewModel;
            //Oletuksena, Valitse ensimmäinen tuote
            tuoteListview.SelectedIndex = 0;
        }
        //Muokkaa tuotetta listalla
        private void Muokkaa_Click(object sender, RoutedEventArgs e)
        {
            PainaTuoteButtons();
        }
        //Peruuta tuotteen päivitys
        private void Lopeta_Click(object sender, RoutedEventArgs e)
        {
            PainaTuoteButtons();
        }
        //Päivitä tuotteen muokkaus
        private void Paivittaa_Click(object sender, RoutedEventArgs e)
        {
            PainaTuoteButtons();
        }
        // Napauta panel displayta lisätäksesi/muokataksesi tuotetta
        private void PainaTuoteButtons()
        {
            if (lisaaPanel.Visibility == Visibility.Visible)
                lisaaPanel.Visibility = Visibility.Collapsed;
            else
                lisaaPanel.Visibility = Visibility.Visible;

            if (muokkaaPanel.Visibility == Visibility.Visible)
                muokkaaPanel.Visibility = Visibility.Collapsed;
            else
                muokkaaPanel.Visibility = Visibility.Visible;
        }

        
    }
}

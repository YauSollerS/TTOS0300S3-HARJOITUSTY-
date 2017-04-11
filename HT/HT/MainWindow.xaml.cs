using HT.ViewModels;
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
using System.Windows.Threading;
using System.IO;
using MahApps.Metro.Controls;

namespace HT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private DispatcherTimer _clockTimer;

        public MainWindow()
        {
            InitializeComponent();
            //Varmista että kaikki tarvittavat tiedot on olemassa tallessa
            Onkotiedostot();

            _clockTimer = new DispatcherTimer();
            _clockTimer.Interval = new TimeSpan(1000);
            _clockTimer.Tick += _clockTimer_Tick;
            _clockTimer.Start();

            this.DataContext = new TuotteetViewModel();

        }

        private void _clockTimer_Tick(object sender, EventArgs e)
        {
            clockLabel.Content = DateTime.Now.ToShortTimeString();
        }

        private void btnTuotteet_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new TuotteetViewModel();
        }

        private void btnMyynti_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new MyyntiViewModel();
        }

        private void btnHistoria_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new HistoriaViewModel();
        }

        private void btnToimittajat_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new TuottajatViewModel();
        }

        private void Onkotiedostot()
        {
            if (!File.Exists("liiketoimet.json"))
            {
                File.Create("liiketoimet.json");
            }
            if (!File.Exists("tuottajat.json"))
            {
                File.Create("tuottajat.json");
            }
            if (!File.Exists("tuotteet.json"))
            {
                File.Create("tuotteet.json");
            }
        }
    }
}

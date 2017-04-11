using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HT.Models;
using System.Windows.Input;

namespace HT.ViewModels
{
    public class KassaViewModel : ObservableObject
    {
       
        public MyyntiViewModel Myynti { get; private set; }
        
        public Liiketoimi Liiketoimi { get; private set; }

        //Viimeistelee kaupan
        private ICommand _hyvaksyCommand { get; set; }
        public ICommand HyvaksyCommand
        {
            get
            {
                return _hyvaksyCommand;
            }
        }

        public KassaViewModel(MyyntiViewModel myynti)
        {
            
            Myynti = myynti;
            //Luo uusi kaupankäynti tapaus
            Liiketoimi = new Liiketoimi();
            
            _hyvaksyCommand = new RelayCommand(HyvaksyLiiketoimi);
        }

        public void HyvaksyLiiketoimi()
        {
            //Nimeä uusi liiketoimi ID
            AntaaId(Liiketoimi);

            //Asetta kaiken kaupankäynnistä saadun tiedon myynti-informaation perusteella
            Liiketoimi.AsiakasNimi = Liiketoimi.AsiakasEnimi + " " + Liiketoimi.AsiakasSnimi;
            Liiketoimi.Aika = DateTime.Now;
            Liiketoimi.ShortDate = DateTime.Now.ToShortDateString();
            Liiketoimi.OstoHinta = Myynti.OstoHinta;
            Liiketoimi.Toimitusmaksu = Myynti.Toimitus;
            Liiketoimi.Verot = Myynti.Verot;
            Liiketoimi.Yhteensa = Myynti.Yhteensa;
            Liiketoimi.Toimitus = Myynti.SelectedToimitus;

            //Tallenna uusi kaupankäynti historiaan ja tyhjennä
            Tallennukset.SaveUusiLiiketoimi(Liiketoimi);
            Myynti.ClearLiiketoimi();
        }

        private void AntaaId(Liiketoimi liiketoimi)
        {
            //Tuo kaikki tallennetut kaupankäynnit
            var histori = Tallennukset.LoadLiiketoimi();

            //Oletus ID, jos se on ensimmäinen ja ainoa
            liiketoimi.Id = 100;

            //Jos on olemassa olevia kaupankäyntejä, aseta ID
            if (histori.Count > 0)
            {
                //Toiston välttämiseksi etsi suurin numero ja kasvata sitä
                liiketoimi.Id = histori.Max(i => i.Id);
                liiketoimi.Id++;
            }
        }
    }
}

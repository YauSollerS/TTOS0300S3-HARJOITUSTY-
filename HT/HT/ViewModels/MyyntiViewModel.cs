using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HT.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HT.ViewModels
{
    public class MyyntiViewModel : ObservableObject
    {
        //Kokoelma valittavista tuotteista tiedostossa
        private ObservableCollection<Tuote> _tuoteList { get; set; }
        //Kokoelma katsottavista tuotteista
        public ObservableCollection<Tuote> Tuotteet { get; set; }
        //Kokoelma ostoskorin tuotteista
        public ObservableCollection<KoriViewModel> Kori { get; set; }
        //Valittu tilauksen toimitustapa
        public Toimitustavat SelectedToimitus { get; set; }

        //Käyttäjään komennot ostoskoria varten
        private ICommand _lisaaKoriin { get; set; }
        private ICommand _poistaKorista { get; set; }

        //Laskettu kustannusten yhteismäärä
        private double _ostoHinta { get; set; }
        private double _toimitus { get; set; }
        private double _verot { get; set; }
        private double _yhteensa { get; set; }


        /// <summary>
        /// Seuraavat ominaisuus kentät ovat pyöristettyjä arvoja koko myynnistä.
        /// Ne näkyvät käyttäjälle.
        /// </summary>
        public double OstoHinta
        {
            get
            {
                return _ostoHinta;
            }
            set
            {
                _ostoHinta = Math.Round(value, 2);
                OnPropertyChanged("Ostohinta");
            }
        }
        public double Toimitus
        {
            get
            {
                return _toimitus;
            }
            set
            {
                _toimitus = Math.Round(value, 2);
                OnPropertyChanged("Toimitus");
            }
        }

        public double Verot
        {
            get
            {
                return _verot;
            }
            set
            {
                _verot = Math.Round(value, 2);
                OnPropertyChanged("Verot");
            }
        }
        public double Yhteensa
        {
            get
            {
                return _yhteensa;
            }
            set
            {
                _yhteensa = Math.Round(value, 2);
                OnPropertyChanged("Yhteensa");
            }
        }

        public ICommand LisaaKoriinCommand
        {
            get
            {
                return _lisaaKoriin;
            }
        }

        public ICommand PoistaKoristaCommand
        {
            get
            {
                return _poistaKorista;
            }
        }

        public MyyntiViewModel()
        {
            //Luo kokonaan uusi ja tyhjä ostoskori kokoelma
            Kori = new ObservableCollection<KoriViewModel>();
            //Lataa kaikki saatavilla olevat tuotteet tiedostolla
            _tuoteList = Tallennukset.LoadTuoteList();
            
            Tuotteet = _tuoteList;
            //Aseta käyttäjä komennot ostoskorin hallintaa varten
            _lisaaKoriin = new RelayCommand(LisaaTuoteKoriin);
            _poistaKorista = new RelayCommand(PoistaTuoteKorista);
        }

        //Lisää valittu tuote katsottavaksi ostoskoriin
        public void LisaaTuoteKoriin(object tuote)
        {
            
            var uusiTuote = tuote as Tuote;
            //Vaikka esine on jo ostoskorissa, lisää se silti
            var existingItem = Kori.FirstOrDefault(param => param.Tuote.Id == uusiTuote.Id);

            //Jos tuote on jo olemassa, päivitä sen tiedot
            if (existingItem != null)
                existingItem.LisaaMaara(1);
            else
            {
                //Lisää uusi tuote ostoskoriin
                var koriEsine = new KoriViewModel(uusiTuote);
                Kori.Add(koriEsine);
            }

            //Päivitä myynnin tämänhetkinen hinta
            UpdateSaldo();
        }

        //Poista tuote ostoskorista
        public void PoistaTuoteKorista(object tuote)
        {
            var vanhaTuote = tuote as Tuote;
            Kori.Remove(Kori.First(i => i.Tuote == vanhaTuote));
            UpdateSaldo();
        }

        //Hae kaikki ostoskorin tuotteet tarkistusta varten
        public List<Tuote> KoriTuotteet()
        {
            var tuotteet = new List<Tuote>();
            foreach (var item in Kori)
            {
                tuotteet.Add(item.Tuote);
            }

            return tuotteet;
        }

        //Kaupankäynti on valmis ja tyhjennä se seuraavaa varten
        public void LiiketoimiViimeistelty()
        {
            Kori.Clear();
        }

        public void ClearLiiketoimi()
        {
            Kori = new ObservableCollection<KoriViewModel>();
            OstoHinta = 0;
            Toimitus = 0;
            Verot = 0;
            Yhteensa = 0;
        }


        //Päivitä myynnin hinta
        private void UpdateSaldo()
        {
            double osto = 0;
            double verot = 0;

            foreach (var item in Kori)
            {
                osto += item.Tuote.Hinta * item.Maara;
                verot += (item.Tuote.Hinta * 0.24) * item.Maara;
            }

            OstoHinta = osto;
            Verot = verot;
            Toimitus = 8;
            Yhteensa = OstoHinta + Toimitus + Verot;
        }
    }
}

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
    public class TuottajatViewModel : ObservableObject
    {
        //Listaus kaikista tuottajista
        public ObservableCollection<Tuottaja> Tuottajat { get; set; }
        //Valittu tuottaja katsomista varten
        private Tuottaja _viewedTuottaja { get; set; }
        //Modeli valitusta tuottajasta katsomista varten
        private Tuottaja _tuottajaModel { get; set; }

        public Tuottaja ViewedTuottaja
        {
            get
            {
                return _viewedTuottaja;
            }
            set
            {
                _viewedTuottaja = value;
                OnPropertyChanged("ViewedTuottaja");
            }
        }

        public Tuottaja TuottajaModel
        {
            get
            {
                return _tuottajaModel;
            }
            set
            {
                _tuottajaModel = value;
                OnPropertyChanged("TuottajaModel");
            }
        }



        private ICommand _viewTuottaja { get; set; }

        public ICommand ViewTuottajaCommand
        {
            get
            {
                return _viewTuottaja;
            }
        }

        private ICommand _selectTuottaja { get; set; }

        public ICommand SelectTuottajaCommand
        {
            get
            {
                return _selectTuottaja;
            }
        }


        private ICommand _updateTuottaja { get; set; }

        public ICommand UpdateTuottajaCommand
        {
            get
            {
                return _updateTuottaja;
            }
        }


        private ICommand _clearTuottaja { get; set; }

        public ICommand ClearTuottajaCommand
        {
            get
            {
                return _clearTuottaja;
            }
        }


        private ICommand _addTuottaja { get; set; }

        public ICommand AddTuottajaCommand
        {
            get
            {
                return _addTuottaja;
            }
        }


        private ICommand _removeTuottaja { get; set; }

        public ICommand RemoveTuottajaCommand
        {
            get
            {
                return _removeTuottaja;
            }
        }

        public TuottajatViewModel()
        {
            //Lataa kaikki tuottajat kokoelmaan
            Tuottajat = Tallennukset.LoadTuottajat();
            //Luo oletuksena tyhjän modelin
            TuottajaModel = new Tuottaja();

            //Nimeä komennot funktioihin
            _viewTuottaja = new RelayCommand(ViewTuottaja);
            _selectTuottaja = new RelayCommand(SelectTuottaja);
            _updateTuottaja = new RelayCommand(UpdateTuottaja);
            _clearTuottaja = new RelayCommand(ClearTuottaja);
            _addTuottaja = new RelayCommand(AddTuottaja);
            _removeTuottaja = new RelayCommand(RemoveTuottaja);
        }
        //Valitse tuottaja katsottavaksi
        public void ViewTuottaja(object tuottaja)
        {
            var valmistaja = tuottaja as Tuottaja;
            ViewedTuottaja = valmistaja;
            
        }
        //Valitse olemassa oleva tuottaja muokkausta/päivitystä varten
        public void SelectTuottaja(object tuottaja)
        {
            var valmistaja = tuottaja as Tuottaja;
            TuottajaModel = valmistaja;
        }
        //Lisää uusi tuottaja kokoelmaan
        public void AddTuottaja()
        {
            
            AntaaId(TuottajaModel);

            
            Tuottajat.Add(TuottajaModel);
            //Lisää uusi tuottaja tiedostoon
            Tallennukset.SaveUusiTuottaja(Tuottajat);
            ClearTuottaja();
        }

        //Päivitä olemassa oleva tuottaja
        public void UpdateTuottaja()
        {
            //Haetaan tuottajaa samalla ID:llä kuin modelilla
            var valmistaja = Tuottajat.FirstOrDefault(param => param.Id == TuottajaModel.Id);
            
            valmistaja = TuottajaModel;
            //Tallenna päivitetty tuottajakokoelma tiedostoon
            Tallennukset.SaveUusiTuottaja(Tuottajat);
            ClearTuottaja();
        }
        //Poista tuottaja kokoelmasta
        public void RemoveTuottaja(object contact)
        {
            var vanhaTuottaja = contact as Tuottaja;
            Tuottajat.Remove(vanhaTuottaja);
            //Tallenna kontaktien poisto tuottajakokoelmassa tiedostoon
            Tallennukset.SaveUusiTuottaja(Tuottajat);
        }

        //Tyjennä uutta tuottaja modelia varten
        public void ClearTuottaja()
        {
            TuottajaModel = new Tuottaja();
            OnPropertyChanged("TuottajaModel");
        }

        private void AntaaId(Tuottaja valmistaja)
        {
            //Aseta ID oletukseksi jos se on yksi ja ainoa
            valmistaja.Id = 1;
            if (Tuottajat.Count > 0)
            {
                //Ennaltaehkäistäkseen toistoa, etsi suurin ID arvo ja tee siitä uusi ID
                valmistaja.Id = Tuottajat.Max(i => i.Id);
                valmistaja.Id++;
            }
        }



    }
}

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
    public class TuotteetViewModel : ObservableObject
    {
        //Kokoelma kaikista tiedoston tuotteista
        private ObservableCollection<Tuote> _tuoteList { get; set; }
        //Kokoelma näytöllä olevista tuotteista
        public ObservableCollection<Tuote> Tuotteet { get; set; }
        //Kokoelma tuottajista tiedostolla
        public ObservableCollection<Tuottaja> Valmistajat { get; set; }
        //Modeli tuotteistamme joita valmistamme, päivitämme tai poistamme
        private Tuote _tuoteModel { get; set; }
        //Modeli tuotteistamme, joita katsomme par aikaa
        private Tuote _viewedTuote { get; set; }
        
        private Tuottaja _selectedValmistaja { get; set; }

        //Ominaisuus tuote modeliin pääsyyn
        public Tuote TuoteModel
        {
            get
            {
                return _tuoteModel; 
            }
            set
            {
                _tuoteModel = value;
                OnPropertyChanged("TuoteModel");
            }
        }
        //Ominaisuus pääsyyn valittuun tuottaja modeliin tuotemodelista
        public Tuottaja SelectedValmistaja
        {
            get
            {
                return _selectedValmistaja;
            }
            set
            {
                _selectedValmistaja = value;
                
                TuoteModel.ValmistajaId = _selectedValmistaja.Id;
                OnPropertyChanged("SelectedValmistaja");
            }
        }
        //Ominaisuus katsottuun tuotteeseen pääsyyn
        public Tuote ViewedTuote
        {
            get { return _viewedTuote; }
            set
            {
                _viewedTuote = value;
                //Eittämättä hieman sekava looginen tapa asettaa tuottaja katsottuihin tuotteisiin
                if (value != null && Valmistajat.Count > 0)
                {
                    
                    var valmistaja = Valmistajat.FirstOrDefault(param => param.Id == _viewedTuote.ValmistajaId);
                    
                    if (valmistaja != null) _viewedTuote.ValmistajaNimi = valmistaja.Nimi;
                }

                OnPropertyChanged("ViewedTuote");
            }
        }

        private ICommand _editTuote { get; set; }
        public ICommand EditCommand
        {
            get
            {
                return _editTuote;
            }
        }

        private ICommand _cancleEdit { get; set; }
        public ICommand CancleCommand
        {
            get
            {
                return _cancleEdit;
            }
        }

        private ICommand _updateTuote { get; set; }
        public ICommand UpdateCommand
        {
            get
            {
                return _updateTuote;
            }
        }

        private ICommand _addTuote { get; set; }
        public ICommand AddCommand
        {
            get
            {
                return _addTuote;
            }
        }

        private ICommand _removeTuote { get; set; }
        public ICommand RemoveCommand
        {
            get
            {
                return _removeTuote;
            }
        }

        public TuotteetViewModel()
        {
            //Lataa kaikki olemassa olevat tuotteet tiedostoon
            _tuoteList = Tallennukset.LoadTuoteList();
            Tuotteet = _tuoteList;
            //Lataa kaikki tuottajien kontakteihin tiedostoon
            Valmistajat = Tallennukset.LoadTuottajat();
            //Asettaa oletuksena tyhjän tuote modelin katsottavaksi
            TuoteModel = new Tuote();
            
            SelectedValmistaja = new Tuottaja();
            //Asettaa kaikki käskyt päivitykseen, lisäykseen ja poistoon
            _editTuote = new RelayCommand(EditTuote);
            _addTuote = new RelayCommand(AddTuote);
            _removeTuote = new RelayCommand(RemoveTuote);
            _cancleEdit = new RelayCommand(Cancle);
            _updateTuote = new RelayCommand(Update);
        }

        //Luodaan tuote modeli Nykyiseen modeliin tiedot välittäen 
        public void EditTuote(object tuote)
        {
            //Aseta modeli ja tuottaja tuotteisiin
            TuoteModel = tuote as Tuote;
            SelectedValmistaja.Id = TuoteModel.ValmistajaId;
            //Aseta tuottaja kokoelman pohjalta. Jos tuottaja on poistettu, käytä ensimmäistä vaihtoehtoa listassa oletuksena
            SelectedValmistaja = Valmistajat.FirstOrDefault(c => c.Id == TuoteModel.ValmistajaId) ?? Valmistajat[0];

            OnPropertyChanged("TuoteModel");

        }
        //Lisää uusi tuote kokoelmaan
        public void AddTuote()
        {
            //Nimeä ID uuteen lisättyyn tuotteeseen 
            AntaaId(TuoteModel);
            //Aseta tuote tuotekokoelmaan
            _tuoteList.Add(TuoteModel);

            Update();
        }
        //Poista tuote kokoelmasta
        public void RemoveTuote(object tuote)
        {
            var vanhaTuote = tuote as Tuote;
            _tuoteList.Remove(vanhaTuote);
            Update();
        }
        //Tyhjentää tuote modelin ja tallentaa kokoelman tiedostoon
        public void Update()
        {
            TuoteModel = new Tuote();
            Tallennukset.SaveUusiTuoteList(_tuoteList);
        }
        //Keskeyttää valitun tuotteen muokkauksen ja luo uuden
        public void Cancle()
        {
            TuoteModel = new Tuote();
        }
        //Nimeä uusi ID tuotteelle
        private void AntaaId(Tuote tuote)
        {
            tuote.Id = 100;
            if(Tuotteet.Count>0)
            {
                tuote.Id = Tuotteet.Max(i => i.Id);
                tuote.Id++;
            }
        }
    }
}

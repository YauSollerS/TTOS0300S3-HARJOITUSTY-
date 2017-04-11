using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HT.Models;

namespace HT.ViewModels
{
    public class HistoriaViewModel : ObservableObject
    {
        public List<Liiketoimi> Liiketoimet { get; private set; }
        public Liiketoimi SelectedLiiketoimi { get; private set; }

        public HistoriaViewModel()
        {
            //Lataa kaikki kaupankäynnit tiedostosta
            Liiketoimet = Tallennukset.LoadLiiketoimi();
        }

        public void SelectLiiketoimi(Liiketoimi liiketoimi)
        {
            //Aseta valittu kaupankäynti
            SelectedLiiketoimi = liiketoimi;
            OnPropertyChanged("SelectedLiiketoimi");
        }
    }
}

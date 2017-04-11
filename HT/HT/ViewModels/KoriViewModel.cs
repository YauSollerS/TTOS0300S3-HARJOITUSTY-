using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HT.Models;

namespace HT.ViewModels
{
    public class KoriViewModel : ObservableObject
    {
        public Tuote Tuote { get; private set; }
        public double Yhteensa { get; private set; }
        public int Maara { get; private set; }

        public KoriViewModel(Tuote tuote)
        {
            Tuote = tuote;
            Maara = 1;
            Yhteensa = Tuote.Hinta * Maara;
        }

        public void LisaaMaara(int value)
        {
            Maara += value;
            Yhteensa = Tuote.Hinta * Maara;
            OnPropertyChanged("Yhteensa");
            OnPropertyChanged("Maara");
        }
    }
}

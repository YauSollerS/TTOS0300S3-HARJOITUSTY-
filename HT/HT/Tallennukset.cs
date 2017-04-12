using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HT.Models;
using System.Collections.ObjectModel;

namespace HT
{
    //Enumeja 
    public enum Tuotetyypit
    {
        Hiiret,
        Hiirimatot,
        Kuulokkeet,
        Nappaimistot
    }

    public enum Toimitustavat
    {
        Posti,
        Matkahuolto
        
    }

    public enum Maksutavat
    {
        Visa,
        MasterCard,
        PayPal
    }

    //Sisältää metodeja, jotka tallentaa tiedot tiedostoihin ja tuo/lukee tiedot tiedostoista
    public static class Tallennukset
    {
        public static void SaveUusiTuoteList(ObservableCollection<Tuote> tuotteet)
        {
            JSON.WriteToJsonFile("tuotteet.json", tuotteet);
        }

        public static ObservableCollection<Tuote> LoadTuoteList()
        {
            var listing = JSON.ReadFromJsonFile<ObservableCollection<Tuote>>("tuotteet.json");

            if (listing == null)
                listing = new ObservableCollection<Tuote>();

            return listing;
        }

        public static void SaveUusiLiiketoimi(Liiketoimi liiketoimi)
        {
            try
            {
                var historia = new List<Liiketoimi>();
                historia = LoadLiiketoimi();
                historia.Add(liiketoimi);
                JSON.WriteToJsonFile("liiketoimet.json", historia);
            }
            catch
            {
                var historia = new List<Liiketoimi>();
                historia.Add(liiketoimi);
                JSON.WriteToJsonFile("liiketoimet.json", historia);
            }
        }

        public static List<Liiketoimi> LoadLiiketoimi()
        {
            var listing = JSON.ReadFromJsonFile<List<Liiketoimi>>("liiketoimet.json");

            if (listing == null)
                listing = new List<Liiketoimi>();

            return listing;
        }

        public static void SaveUusiTuottaja(ObservableCollection<Tuottaja> tuottajat)
        {
            JSON.WriteToJsonFile("tuottajat.json", tuottajat);
        }

        public static ObservableCollection<Tuottaja> LoadTuottajat()
        {
            var listing = JSON.ReadFromJsonFile<ObservableCollection<Tuottaja>>("tuottajat.json");

            if (listing == null)
                listing = new ObservableCollection<Tuottaja>();

            return listing;
        }
    }
}

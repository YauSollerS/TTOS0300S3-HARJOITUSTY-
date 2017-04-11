using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Models
{
     public class Liiketoimi
    {
        public int Id { get; set; }
        public DateTime Aika { get; set; }
        public string Seurantanumero { get; set; }
        public string MaksuId { get; set; }
        public string AsiakasEnimi { get; set; }
        public string AsiakasSnimi { get; set; }
        public string Osoite { get; set; }
        public string ToimitusOsoite { get; set; }
        public string AsiakasPuhelin { get; set; }
        public string AsiakasSposti { get; set; }
        public Toimitustavat Toimitus { get; set; }
        public Maksutavat Maksutapa { get; set; }
        public List<Tuote> Tuotteet { get; set; }
        public double OstoHinta { get; set; }
        public double Verot { get; set; }
        public double Toimitusmaksu { get; set; }
        public double Yhteensa { get; set; }

        
        public string AsiakasNimi { get; set; }
        public string ShortDate { get; set; }

        public Liiketoimi()
        {
            ShortDate = Aika.ToShortDateString();
            AsiakasNimi = AsiakasEnimi + " " + AsiakasSnimi;
        }
    }
}

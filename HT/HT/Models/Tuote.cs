using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HT.Models
{
    public class Tuote
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Kuvaus { get; set; }
        public double Hinta { get; set; }
        public Tuotetyypit Tyyppi { get; set; }
        public int ValmistajaId { get; set; }

        [JsonIgnore]
        public string ValmistajaNimi { get; set; }

        public Tuote()
        {

        }
    }
}

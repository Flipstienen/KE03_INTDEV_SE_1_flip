using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class gebruikergegevens
    {
        public string naam { get; set; }
        public string adres { get; set; }

        public gebruikergegevens(string naam, string adres)
        {
            this.naam = naam;
            this.adres = adres;
        }
    }
}

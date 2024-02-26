using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Klasy
{
    abstract class Napoje
    {
        //
        public string nazwa { get; set; } = ""; //właściwości 
        public double cena { get; set; }
        public int typNapoju { get; set; }

        //public Napoje(int id_napoj, string nazwa, double cena)
        public Napoje(string nazwa, double cena, int typNapoju) //konstruktor parametrowy
        {

            this.nazwa = nazwa;
            this.cena = cena;
            this.typNapoju = typNapoju;
        }
    }
}

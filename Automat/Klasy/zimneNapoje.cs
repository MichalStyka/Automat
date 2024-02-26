using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Klasy
{
     class zimneNapoje:Napoje
    {
        public static int iloscZminegoNapoje;
        //public string typNapoju = "zimny";
        public static int idNapoju=0;
        public int typNapoju;
        //public zimneNapoje(int id_napoj, string nazwa, double cena) :base(id_napoj, nazwa, cena)
        public zimneNapoje(string nazwa, double cena, int typNapoju) : base(nazwa, cena,typNapoju)
        {
            //this.id_napoj = id_napoj;
            this.nazwa = nazwa;
            this.cena = cena;
            this.typNapoju = typNapoju;
            idNapoju = iloscZminegoNapoje;
            iloscZminegoNapoje++;
        }





    }
}

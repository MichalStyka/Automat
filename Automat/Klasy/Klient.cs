using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Klasy
{
     class Klient : User
    {
        public Klient(string nazwa) : base(nazwa)
        {
            this.nazwa = "Klient";

        }


    }
}

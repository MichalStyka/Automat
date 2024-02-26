using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Klasy
{
    class Admin : User
    {
        public string haslo = "";
        public Admin(string nazwa) : base(nazwa)
        {
            this.nazwa = "Administrator";

        }

        public string AdminGetHaslo() 
        {
             string[] danePliku = new string[2];
            danePliku = File.ReadAllLines("automatDane.txt");
                haslo=danePliku[0]; //pierwsza linia w pliku to hasło administratora
            return haslo;
        }


    }
}


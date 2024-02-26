using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Klasy
{
     abstract class User
    {
        public string nazwa { get; set; } = "";

        public User(string nazwa)
        {
            this.nazwa = nazwa;
        }
    }
}

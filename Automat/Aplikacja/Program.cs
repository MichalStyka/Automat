using System;
using System.Collections.Generic;//listy
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;//do textu
using Automat.Klasy;

namespace Automat.Aplikacja
{

    class Program
    {
        static void Main(string[] args)
        {
            //Tytuł okna konsoli
            Title = "Automat z napojami w67193 Styka Michal";

            //tworzenie obiektu automat
            AutomatNapoje automat = new AutomatNapoje();

            //sprawdzanie czy istnieją i ewentualne utworzenie plików .txt przy pierwszym uruchamianiu
            automat.inicjalizacjaDanych();
            automat.panelLogowania();
            
    

           
        }
    }
}








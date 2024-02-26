using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using System.Linq;
namespace Automat.Klasy
{
     class ciepleNapoje:Napoje
    {
        public static int iloscCiepleNapoje;
        public int idNapoju = 0;
        public int typNapoju;
        //public ciepleNapoje(int id_napoj, string nazwa, double cena) : base(id_napoj, nazwa, cena)
        public ciepleNapoje(string nazwa, double cena,int typNapoju) : base(nazwa, cena,typNapoju)
        {
            //this.id_napoj = id_napoj;
            this.nazwa = nazwa;
            this.cena = cena;
            this.typNapoju = typNapoju;
            this.idNapoju=iloscCiepleNapoje;
            iloscCiepleNapoje++;
        }


        //poziom cukru
//        public int cukierNapoj()
//        {
//            int poziomCukru;

//            string temp;

            
//            Clear();

//            do
//            {
//WriteLine(@"Wybierz poziom cukru : 
//0 - Brak
//1 - Mało
//2 - Średnio
//3 - Dużo
//                ");
//                temp = ReadLine();
                
//                if (temp != "0" && temp != "1" && temp != "2" && temp != "3")
//                {
//                    Clear();
//                    WriteLine("Wybierz od 0-3  !!");
//                    Clear();
//                }
//            } while (temp != "0" && temp != "1" && temp != "2" && temp != "3");
            
//            Clear();

//            poziomCukru = Int32.Parse(temp);
//            return poziomCukru;
//        }



    }
}

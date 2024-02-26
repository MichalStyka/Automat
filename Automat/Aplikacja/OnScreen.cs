using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Threading.Tasks;
using static System.Console;
using Automat.Klasy;

namespace Automat.Aplikacja
{
    static class OnScreen
    {
        
        public static int wyborUser() //menu powitalne programu
        {
            int wybor;
            string[] opcje = { "Zaloguj się jako administrator automatu", "Kup napój","Zamknij aplikacje automat" }; //pozycje wyświetlające się w menu
            string tekst = @"
Witaj! Dziękujemy za korzystanie z naszego automatu z napojami.
Jaka dalszą operacje chcesz dokonać ? :
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)
";

            wybor=Menu.wysOpcje(tekst, opcje);
            return wybor;
        }



        public static int adminLogIn() //menu logowania administratora
        {
            int wybor;
            string[] opcje = { "Zaloguj się","Powrót" };
            string tekst = @"
Czy chcesz się zalogować jako admin?
Jaka dalszą operacje chcesz dokonać ? :
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)
";
            wybor = Menu.wysOpcje(tekst, opcje);
            return wybor;
        }


//logowanie admin
        public static int Logowanie()
        {
            int statusHasla;
            string hasloPoprawne = Dodatki.daneAutomatu(0);//0 to haslo a 1 to status automatu
            SecureString pass = Dodatki.maskInputString();
            string hasloWpisane = new System.Net.NetworkCredential(string.Empty, pass).Password;
            Console.WriteLine("");
            if (hasloWpisane == hasloPoprawne)
            {
                Clear();
                statusHasla = 1;

            }
            else
            {
                Clear();
                statusHasla = 0;
            }
            return statusHasla;
        }


//---------------------------------------------------------------opcje administratora menu ----------------------------------------------------
        public static int adminOpcje() //do menu głównego opcji administratora
        {
            Admin admin = new Admin("Administrator");
            int wybor;
            string[] opcje = { "Wyłącz/włacz opcje kupowania","Zmień hasło","Modyfikacja asortymentu","Dodaj produkt","Usuń produkt","Pokaż tranzakcje", "Wyloguj się" };
            string tekst = @$"
Jestes zalogowany jako {admin.nazwa}?
Jaka dalszą operacje chcesz dokonać ? :
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)
";
            wybor = Menu.wysOpcje(tekst, opcje);
            return wybor;
        }

        //status autoamtu
        public static int StatusMenu() //administator -- menu wyłączania automatu
        {
            int wybor;
            string status=Dodatki.daneAutomatu(1);
            string tekst="";
            string[] opcje = { "Zmień", "Powrót" };
            if (status == "0")
            {
                tekst = "Automat jest: Włączony!! \n \n";
            }
            else if (status == "1")
            {
                tekst = "Automat jest: Wyłączony!! \n \n";

            }

            tekst += $@"Jaka dalszą operacje chcesz dokonać ? :
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)
            ";
            wybor = Menu.wysOpcje(tekst, opcje);
            Clear();
            return wybor;
        }
        
        public static void StatusZmiana() //zmiana statusu automatu
        {
            string status = File.ReadLines("automatDane.txt").Last();
            if (status == "0")
            {
                Dodatki.ZmianaLini("1", "automatDane.txt", 2);//2 linia ,bo pierwsza to haslo do admina
            }
            else if (status == "1")
            {
                Dodatki.ZmianaLini("0", "automatDane.txt", 2);
            }
            Clear();
            WriteLine("Zmieniono status automatu");
            Thread.Sleep(2000);
            
        }
        //czy zmienic haslo
        public static int czyHasloZmiana() //potwierdzenie zmiany hasla
        {
            int wybor;
            string status = Dodatki.daneAutomatu(1);
            string tekst = "";
            string[] opcje = { "Zmień", "Powrót" };    
            tekst = $@"Jaka dalszą operacje chcesz dokonać ? :
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)
            ";
            wybor = Menu.wysOpcje(tekst, opcje);
            Clear();
            return wybor;
        }

        //menu zmiana hasla administratora
        public static void HasloZmiana()
        {
            string stare = Dodatki.daneAutomatu(0);
            string nowe="";
            do
            {
                SecureString pass = Dodatki.maskInputString();
                nowe = new System.Net.NetworkCredential(string.Empty, pass).Password;
                if ((nowe == stare))
                    Console.WriteLine("hasło musi byc inne");
                Thread.Sleep(1000);
                Clear();
            }
            while ((nowe == stare));
            Dodatki.ZmianaLini(nowe, "automatDane.txt", 1);      
            Clear();
            WriteLine("Zmieniono hasło do konta administratora ");
            Thread.Sleep(2000);

        } 




        //-------------------------------------------------------dodawanie produktu-----------------------------------------------

        public static int DodawanieAsortMenu() //menu wyświetlające przy dodawaniu produktu- opcja administratora
        {
            int wybor;
            string[] opcje = { "Dodaj produkt", "Powrót" };
            string tekst = @$"Jaka dalszą operacje chcesz dokonać ? :
(Do nawigacji używaj strzałek i wcisnij Enter by zatwierdzić wybraną opcje!)
";
            wybor = Menu.wysOpcje(tekst, opcje);
            return wybor;
        } 
        public static Dictionary<int, Napoje> DodawanieProduktu(Dictionary<int, Napoje> dictionary) //menu - opcja administratora,typ napoju, wybór ID
        {
            
            int maxKluczy = 9;
            string tekst = "Wybierz typ napoju jaki chcesz dodać: ";
            string[] opcje = { "Ciepły napoj", "Zimny napoj" };
            int typNapoju= Menu.wysOpcje(tekst, opcje);
            Clear();
            List<int> existingKeys = dictionary.Keys.ToList();
            List<int> availableKeys = Enumerable.Range(0, maxKluczy + 1).Except(existingKeys).ToList();
            Console.Write($"Dostępne klucze do użycia: ");
            foreach (int key in availableKeys)
            {
                Console.Write($"{key}, ");
            }
            WriteLine(" ");
            WriteLine("Podaj dowolne ID z dostepnych powyzej:");
            Write("Wpisz: ");
            int idNapoju=int.Parse(ReadLine());
            WriteLine(" ");
            WriteLine("Podaj nazwe produktu");
            Write("Wpisz: ");
            string nazwa=ReadLine();
            WriteLine(" ");
            WriteLine("Podaj cene produktu (z przecinkiem jako separator!)");
            Write("Wpisz: ");
            double cena=Convert.ToDouble(ReadLine());

            if (typNapoju == 0)
            {
                dictionary.Add(idNapoju, new zimneNapoje(nazwa, cena, typNapoju));
            }
            else if (typNapoju == 1)
            {
                dictionary.Add(idNapoju, new ciepleNapoje(nazwa, cena, typNapoju));
            }

            Clear();
            return dictionary;

            


            
        }



    }
    


    }













    


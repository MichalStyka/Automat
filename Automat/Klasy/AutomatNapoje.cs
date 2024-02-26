using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using Automat.Aplikacja;

namespace Automat.Klasy
{
    public class AutomatNapoje
    {
        static Dictionary<int, Napoje> myDictionary = new(); //Słownik, int- identyfikator, musi być unikalny
        List<Napoje> listaNapoji = new List<Napoje>();
        static string filePath = "asortyment.txt";
        static string filePath1 = "Tranzakcje.txt";


        static Dictionary<int, List<string>> Tranzakcje = new Dictionary<int, List<string>>();
        


        int indexOpcji;
        //readonly Admin admin = new Admin("Administrator");
        
//------------------------------------------------------MENU GŁÓWNE -----------------------------------------------
public void panelLogowania() 
        {
            indexOpcji= OnScreen.wyborUser();//"Administrator", "Kupujący","Zamknij aplikacje"

            switch (indexOpcji)
            {
                case 0:
                    AdminLog();
                    break;
                case 1:
                    Klient();
                    break;
                case 2:
                    Dodatki.Wyjdz();
                    break;
            }            
        }
        
//-------------------------------------------------------- FUNKCJE - MENU GŁÓWNE ---------------------------------------------------
        public void Klient() //opcja 1
        {
            string status = Dodatki.daneAutomatu(1);//0-otwarte 1- zmkniete

            if(status == "0")
            {
                Zakupy();


            }
            else if (status == "1")
            {
                Clear();
                WriteLine("Automat nieczynny! Zapraszamy póżniej");
                Thread.Sleep(2000);
                panelLogowania();
            }

        }

        public void Zakupy() //opcja 1 - dodatkowe menu- zakupy napoju
        {
            pobieranieDanych();//pobiera z pliku aktualne dane
            Clear();
            DisplayMenu(myDictionary);
            Console.WriteLine("Wpisz id produktu ( liczba od 0-9) który chcesz kupić lub wpisz 'exit' aby anulować tranzakcje:");
            Write("Wpisz: ");
            string? userInput = Console.ReadLine();
            WriteLine("");
            if (userInput.ToLower() == "exit")
            {
                panelLogowania();
            }
            else if (int.TryParse(userInput, out int choice) && myDictionary.ContainsKey(choice))
            {
                Tranzakcja(choice);
                Clear();
                WriteLine("Dziękujemy za zakup!!!");
                Thread.Sleep(2000);
                Clear();
                panelLogowania();
            }
            else
            {
                Console.WriteLine("Wpisz poprawna opcje");
                Thread.Sleep(2000);
                Zakupy();

            }
        }


        public void Tranzakcja(int wybor) //opcja 1- podmenu 2 , wybor opcji platnosci
        {
            Clear();
            int kluczNapoju = wybor;
      

            if (myDictionary.ContainsKey(wybor))
            {
                string opcja = " ";
                Napoje napoje = myDictionary[wybor];
                string tekst=(@$"Wybrano produkt: {napoje.nazwa} o cenie {napoje.cena} PLN.
Wybierz prosze sposób płatności:");
                string[] opcje = { "Karta", "Gotówka", "Anuluj" };
                int wyborplatnosci = Menu.wysOpcje(tekst,opcje);
                int keyTranz = Tranzakcje.Count() + 1;
                string key = Convert.ToString(keyTranz);
                string cenaStr = Convert.ToString(napoje.cena);
                string dataTranzakcji = Convert.ToString(DateTime.Now);

                double CenaTemp = Convert.ToDouble(napoje.cena);
                decimal Cena = Convert.ToDecimal(CenaTemp);
                decimal reszta = 0;
                decimal cenaWplacona = 0;
                string temp;
                Clear();



                switch (wyborplatnosci)
                {
                    case 0:
                        //karta
                        WriteLine(@$"Zakupiono {napoje.nazwa}.
Proszę odebrać produkt...");
                        Thread.Sleep(2000);
                        Clear();
WriteLine($@"Twoje konto zostało obcjązone wartością {napoje.cena} PLN.
Dziękujemy i zapraszamy ponownie!");
                        Tranzakcje.Add(keyTranz, new List<string> { key, napoje.nazwa, cenaStr, dataTranzakcji });
                        zapisywanieDoPlikuTranz(Tranzakcje, "Tranzakcje.txt");
                        Thread.Sleep(2000);
                        Clear();
                        panelLogowania();

                        break;
                    case 1:
                        Clear();
                        //**************

                        

            do
                {
                            cenaWplacona = Math.Round(cenaWplacona, 2);
                            Clear();
                WriteLine(@$"Wrzuc kolejna monete :
Wprowadz: 0.5 albo 1 albo 2 albo 5 (uzywaj przecinka jako separator)
Wpłacono: {cenaWplacona}
Zostało do zapłaty: {Cena-cenaWplacona}");

                 Write("Wprowadz: ");
                 temp = ReadLine();
                WriteLine(" ");


                            if (temp == "0,5" || temp == "1" || temp == "2" || temp=="5")
                {
                  cenaWplacona += Convert.ToDecimal(temp);
                }
                   else
                      {
                         WriteLine("Nie rozpoznano monety, wrzuc 50gr, 1zl,2zl lub 5zl ");
                       }
                      WriteLine($"Zapłacono: {cenaWplacona} PLN");


                        } while (cenaWplacona < Cena);
                        reszta = cenaWplacona - Cena;                                             
                        reszta=Math.Round(reszta,2);

                WriteLine($"Wydana reszta: {reszta} PLN");
                WriteLine("\nZapłacono monetami, powrot do menu..."); //nie wraca do menu jak powinno ?

                Thread.Sleep(5000);

                        
                        Tranzakcje.Add(keyTranz, new List<string> { key, napoje.nazwa, cenaStr, dataTranzakcji});
                        zapisywanieDoPlikuTranz(Tranzakcje, "Tranzakcje.txt");
                        //*************
                        break;
                    case 2:
                        Zakupy();//anuluj
                        break;
                }

                   
   

                        
                }


            }




//------------------------------------------------------------ tranzakcje ---------------------------------------------------
        static void DisplayMenu(Dictionary<int, Napoje> dictionary)
        {
            Console.WriteLine("Menu:");
            foreach (var kvp in dictionary)
            {
                Console.WriteLine($"{kvp.Key}-{kvp.Value.nazwa}- Cena :{kvp.Value.cena} PLN");
                
            }
        }
        static void DisplayMenuTranz(Dictionary<int, List<string>> dictionary)
        {

            Console.WriteLine("Tranzakcje:");
                string[] lines = File.ReadAllLines("Tranzakcje.txt");
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
                
            
        }

//-------------------------------------------------------------LOGOWANIE ADMINISTRATOR ---------------------------------------------

        //logowanie administrator - menu "czy chcesz sie zalogować"

        public void AdminLog()
        {
            indexOpcji = OnScreen.adminLogIn();//logowanie admin / powrot
            switch (indexOpcji)
            {
                case 0:
                    AdminLogowanie();
                    break;
                case 1:
                    panelLogowania();
                    break;

            }
        }


//logowanie administrator - logowanie,wpisywanie hasła
        public void AdminLogowanie()
        {
            Clear();
            WriteLine("Wprowadz hasło do konta Adminsitrator :");
            indexOpcji = OnScreen.Logowanie();
            if (indexOpcji == 1)
            {
                Clear();
                AdminMain();
            }
            else if (indexOpcji == 0)
            {
                Clear();
                WriteLine("Hasło błędne !! Spróbuj ponownie");
                Thread.Sleep(2000);// 3 sek cooldown
                AdminLog();//wraca do Czy chcesz sie zalogować?
            }

        }

        //------------------------------------------Opcje administratora ---------------------------------------------------

        public void AdminMain()//"Wyłącz/włacz opcje kupowania","Zmień hasło","Modyfikacja asortymentu","Dodaj produkt","Usuń produkt", "Wyloguj się"
        {
            Clear();
            indexOpcji = OnScreen.adminOpcje();//
            switch (indexOpcji)
            {
                case 0:
                    StatusAutoamtu();
                    break;
                case 1:
                    CzyZmianaHasla();
                    break;
                case 2:
                    ModAsortymentu();
                    break;
                case 3:
                    DodajAsort();
                    break;
                case 4:
                    UsunAsort();
                    break;
                case 5:
                    Pokaztranz();
                    break;
                case 6:
                    panelLogowania(); //wyloguj
                    break;

            }

        }


        //---------------------------------------------------------------- Funkcje administratora ----------------------------------------------------

        //opcja 0 - status automatu, mozliwowac zablokowania możliwości kupowania napojów
        public void StatusAutoamtu()//"Wyłącz/włacz, "Powrót"
        {
            Clear();
            indexOpcji = OnScreen.StatusMenu();//
            switch (indexOpcji)
            {
                case 0:
                    OnScreen.StatusZmiana();
                    AdminMain();
                    break;
                case 1:
                    AdminMain();
                    break;


            }

        }

        // opcja 1 - zmiana hasla
        public void CzyZmianaHasla()//"zmien, "Powrót" 
        {
            Clear();
            indexOpcji = OnScreen.czyHasloZmiana();//
            switch (indexOpcji)
            {
                case 0:
                    OnScreen.HasloZmiana();
                    AdminMain();
                    break;
                case 1:
                    AdminMain();
                    break;


            }

        }

        public void ModAsortymentu() //opcja 2
        {
            pobieranieDanych();//pobiera z pliku aktualne dane
            Clear();
            DisplayMenu(myDictionary);
            Console.WriteLine("Wpisz id produktu ( liczba od 0-9) który chcesz edytować lub wpisz 'exit':");
            string? userInput = Console.ReadLine();

            if (userInput.ToLower() == "exit")
            {
                AdminMain();
            }
            else if (int.TryParse(userInput, out int choice) && myDictionary.ContainsKey(choice))
            {
                modyfiakcjaProduktu(choice);
                Clear();
                WriteLine("Zmiana została dokonana");
                Thread.Sleep(2000);
                Clear();
                AdminMain();
            }
            else
            {
                Console.WriteLine("Wpisz poprawna opcje");
                Thread.Sleep(2000);
                ModAsortymentu();

            }

        }

        public void modyfiakcjaProduktu(int wybor) //opcja 2 - dodatkowe menu
        {
            if (myDictionary.ContainsKey(wybor))
            {
                string? opcja = "";
                Napoje napoje = myDictionary[wybor];

                do
                {
                    Clear();
                    WriteLine("Co chcesz zmeinic?");
                    WriteLine($@"1 -- Nazwa tego produktu:  {napoje.nazwa}");
                    WriteLine($@"2 -- Cena tego produktu:  {napoje.cena}");
                    WriteLine($@"Jeżeli chcesz wyjść wpisz 'exit'");
                    opcja = ReadLine();
                    if (opcja != "1" && opcja != "2")
                    {
                        if (opcja.ToLower() == "exit")
                        {
                            AdminMain();
                        }
                        WriteLine("Podaj poprawną opcje!!");
                        Thread.Sleep(1000);
                        Clear();
                    }
                } while (opcja != "1" && opcja != "2");

                Clear();

                switch (opcja)
                {
                    case "1":
                        WriteLine(@$"Podaj nowa nazwe tego produktu:
stara nazwa {napoje.nazwa}");
                        string? nowaNazwa = ReadLine();
                        napoje.nazwa = nowaNazwa;
                        zapisywanieDoPliku(myDictionary, filePath);
                        break;
                    case "2":
                        WriteLine(@$"Podaj nowa cene tego produktu:
stara cena {napoje.cena} PLN
**Ceny proszę wpisywać z (,) jako separator części dziesietnych");
                        string? temp = ReadLine();
                        //double nowaCena = double.Parse(temp); 
                        double nowaCena = Convert.ToDouble(temp);
                        napoje.cena = nowaCena;

                        zapisywanieDoPliku(myDictionary, filePath);
                        break;
                }



            }


        }


        public void DodajAsort() //opcja 3
        {
            pobieranieDanych();
            int numberOfElements = myDictionary.Count;

            if (numberOfElements < 10) {
                Clear();
                indexOpcji = OnScreen.DodawanieAsortMenu();
                switch (indexOpcji)
                {
                    case 0://dodaj
                        myDictionary=OnScreen.DodawanieProduktu(myDictionary);
                        zapisywanieDoPliku(myDictionary, filePath);
                        AdminMain();
                        break;
                    case 1://wyjscie
                        AdminMain();
                        break;


                }
            }
            else
            {
                Clear();
                WriteLine(@$"Osiagnięto maksymalna liczbe napojow : 10
Aby dodać napoj zwolnij miejsce poprzez usuniecie napoju");
                Thread.Sleep(2000);
                ReadKey();
                AdminMain();

            }
        }


        public void UsunAsort() //opcja 4
        {
            pobieranieDanych();//pobiera z pliku aktualne dane
            Clear();
            DisplayMenu(myDictionary);
            Console.WriteLine("Wpisz id produktu ( liczba od 0-9) który chcesz usunąć lub wpisz 'exit' aby anulować akcje:");
            Write("Wpisz: "); 
            string ? userInput = Console.ReadLine();
            WriteLine(" ");
            if (userInput.ToLower() == "exit")
            {
                AdminMain();
            }
            else if (int.TryParse(userInput, out int choice) && myDictionary.ContainsKey(choice))
            {

                myDictionary.Remove(choice);
                zapisywanieDoPliku(myDictionary, filePath);
                Clear();
                WriteLine("Zmiana została dokonana");
                Thread.Sleep(2000);
                Clear();
                AdminMain();
            }
            else
            {
                Console.WriteLine("Wpisz poprawna opcje");
                Thread.Sleep(2000);
                ModAsortymentu();

            }

        }

        public void Pokaztranz() //opcja 5 - wystwietlenie listy transakcji
        {
            pobieranieDanychTranz();
            Clear();
            DisplayMenuTranz(Tranzakcje);
            Console.WriteLine("wpisz 'exit' aby wyjść:");
            string? userInput = Console.ReadLine();

            if (userInput.ToLower() == "exit")
            {
                AdminMain();
            }

            else
            {
                Console.WriteLine("Wpisz poprawna opcje");
                Thread.Sleep(2000);
                Pokaztranz();

            }




        }

        //opcja 6 to wyjscie do menu głównego - panelLogowania()

        //----------------------------------------------------------------------------operacje na danych z pliku-----------------------------------------------

        public void inicjalizacjaDanych()
        {
            if (File.Exists("asortyment.txt") == false)
            {
                myDictionary.Add(0, new ciepleNapoje("Kawa Czarna", 4.00, 1));
                myDictionary.Add(1, new ciepleNapoje("Kawa z mlekiem", 5.00, 1));
                myDictionary.Add(2, new ciepleNapoje("Espresso", 6.00, 1));
                myDictionary.Add(3, new ciepleNapoje("Herbata owocowa", 3.00, 1));
                myDictionary.Add(4, new ciepleNapoje("Barszcz", 3.5, 1));
                myDictionary.Add(5, new ciepleNapoje("Espresso", 4.5, 1));
                myDictionary.Add(6, new zimneNapoje("Cola", 6.5, 0));
                //zapis do pliku
                zapisywanieDoPliku(myDictionary, filePath);
            }
            else
            {

                pobieranieDanych();
            }

            //admin -plik + status automatu
            if (File.Exists("automatDane.txt") == false)
            {
                var plik = "automatDane.txt";
                // 0 - automat otwarty , 1 - automat zamkniety
                string[] dane = { "123", "0" };
                File.WriteAllLines(plik, dane);
            }
            //string[] danePliku = new string[2];
            //danePliku = File.ReadAllLines("automatDane.txt");
            //string haslo = danePliku[0];
            //int statusAutomatu = int.Parse(danePliku[1]);


            if (File.Exists("Tranzakcje.txt") == false)
            {
                var TranzakcjePlik = "Tranzakcje.txt";

                //utworzenie pliku                                
                string[] dane = { };
                File.WriteAllLines(TranzakcjePlik, dane);
            }
            else
            {
                Tranzakcje.Clear();
                try
                {
                    string[] lines = File.ReadAllLines("Tranzakcje.txt");
                    foreach (string line in lines)
                    {
                        // Podział linii na części, np. za pomocą spacji
                        string[] parts = line.Split(' ');

                        // Pierwsza część jako klucz
                        int key = int.Parse(parts[0]);
                        // Pozostałe części jako wartości w liście
                        List<string> values = new List<string>(parts[1].Split(','));
                        Tranzakcje.Add(key, values);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Bład przy odczytywaniu.Błąd: {ex.Message}");
                }


            }


        } // 1- przy uruchamianiu programu

        public static void pobieranieDanych()
        {
            myDictionary.Clear(); //Czysci slownik
            try { 
            using (StreamReader reader = new StreamReader(filePath))
            {

                while (!reader.EndOfStream) //dopóki nie dojdzie do końca pliku
                {
                    string line = reader.ReadLine(); //czyta linie
                    string[] parts = line.Split('-'); //dzieli linie oddzielone znakiem "-" i dodaje do tablicy
                    if(parts.Length >= 4)
                    {                           
                        int key = int.Parse(parts[0]); //przypisuje wartości z tablicy do zmiennych
                        string nazwa = parts[1];
                        double cena = double.Parse(parts[2]);
                        int typ = int.Parse(parts[3]);
                            if(typ == 0)
                            {
                                myDictionary.Add(key, new zimneNapoje(nazwa, cena, typ));
                            }else if(typ == 1)
                            {
                                myDictionary.Add(key, new ciepleNapoje(nazwa, cena, typ));
                            }

                             
                    }
                   
                                            
                }

            }
            }
            catch ( Exception ex )
            {
                Console.WriteLine($"Bład przy odczytywaniu.Błąd: {ex.Message}");
            }
        } //2 -jesli plik jest, to wtedy pobierają sie dane






         static void zapisywanieDoPliku(Dictionary<int, Napoje> dictionary, string filePath)//produkty
    {

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            
            foreach (var kvp in dictionary)
            {
                
                writer.WriteLine($"{kvp.Key}-{kvp.Value.nazwa}-{kvp.Value.cena}-{kvp.Value.typNapoju}");
            }


        }
    }


        static void zapisywanieDoPlikuTranz(Dictionary<int, List<string>> dictionaryTranz, string filePath1)//produkty
        {

            using (StreamWriter writer = new StreamWriter(filePath1))
            {
                // Iterate through the key-value pairs of the dictionary
                foreach (var kvp in dictionaryTranz)
                {

                    writer.WriteLine(string.Join("-", kvp.Value));
                }


            }
        }




        public static void pobieranieDanychTranz()
        {
            Tranzakcje.Clear();
            try
            {
                using (StreamReader reader = new StreamReader(filePath1))
                {
                    //petla przechodzi przez caly plik
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split('-');
                        //4 czesci string oddzielone '-' - plik transakcje.txt
                        if (parts.Length >= 4)
                        {
                            int key = int.Parse(parts[0]);
                            string keyStr = parts[0];
                            string nazwa = parts[1];
                            string cena = parts[2];
                            string data = parts[3];
                            //dodanie danych do slownika
                            Tranzakcje.Add(key, new List<string> { keyStr, nazwa, cena, data });
                          



                        }


                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bład przy odczytywaniu.Błąd: {ex.Message}");
            }
        }



    }
}
 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Automat.Aplikacja
{
    class Menu
    {
        private static int WybranaOpcja;


        //wyswietl opcje- klasa do tworzenia menu, mozna ja wykorzystywac wiele razy
        public static int wysOpcje(string? tekst, string[] opcje)
        {

            ConsoleKey keyPressed;
            do
            {
                Clear();
                Menu.Opcje(tekst, opcje);//wyswietlanie opcji
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                //strzałki
                if (keyPressed == ConsoleKey.UpArrow) //strałka w góre
                {
                    WybranaOpcja--;
                    if (WybranaOpcja == -1)
                    {
                        WybranaOpcja = opcje.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow) //strałka w dół
                {
                    WybranaOpcja++;
                    if (WybranaOpcja == opcje.Length)
                    {
                        WybranaOpcja = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter); //enter - wyjście

            int x = WybranaOpcja;
            WybranaOpcja = 0;// pierwsza opcja
            return x;
        }

        private static void Opcje(string? tekst, string[] opcje)
        {
            WriteLine(tekst);

            for (int i = 0; i < opcje.Length; i++)
            {
                string opcjaZaznaczona = opcje[i];

                
                if (i == WybranaOpcja)
                {
                     //przy zaznaczonej opcji zmienia się kolor tła, czcionki
                    ForegroundColor = ConsoleColor.Red;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    //opcja nie zaznaczona jest wyświetlana tak:
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }

                WriteLine($"{opcjaZaznaczona} ");
            }
            ResetColor();
        }



    }
}

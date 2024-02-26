using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;//do maskowania hasla
using System.Threading.Tasks;
using static System.Console;

namespace Automat.Aplikacja
{
    public static class Dodatki
    {
        //wychodzenie z aplikacji 
        public static void Wyjdz()
        {
            Clear();
            WriteLine("\n Nacisnij dowolny przycisk by wyjsc ");
            ReadKey(true);
            Environment.Exit(0);
        }


//maskowanie hasła
        public static SecureString maskInputString()
        {
            Console.WriteLine("Podaj Hasło: ");
            SecureString pass = new SecureString();
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    pass.AppendChar(keyInfo.KeyChar);
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass.RemoveAt(pass.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            {
                Clear();
                return pass;
            }


        }


//zwroć dane automatu

        public static string daneAutomatu(int dana)//0-haslo//1--status
        {
            string[] danePliku = new string[2];
            danePliku = File.ReadAllLines("automatDane.txt");
            string zmienna = danePliku[dana];
            return zmienna;
        }
        //zmiana linii w pliku do admina
        public static void ZmianaLini(string nowytext, string plik, int linia)
        {
            string[] arrLine = File.ReadAllLines(plik);
            arrLine[linia - 1] = nowytext;
            File.WriteAllLines(plik, arrLine);
            
        }


    }
}

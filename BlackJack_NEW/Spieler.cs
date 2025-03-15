using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_NEW
{
    internal class Spieler
    {
        public string Name { get; protected set; }
        public double Spieleinsatz { get; protected set; }
        public int alter { get; protected set; }

        public Spieler (string name, double spieleinsatz, int alter)
        {
            this.Name = name;
            this.Spieleinsatz = spieleinsatz;
            this.alter = alter;
        }
        // Erstellung eines Spielers über die Konsole
        public static Spieler ErstelleSpielerVonKonsole()
        {
           
            // Name eingeben
            Thread.Sleep(2000);
            Console.WriteLine("==================== !SPIELER DATEN! ====================");
            Console.WriteLine("");
            Console.Write("Bitte gib deinen Namen ein: ");
            string name = Console.ReadLine();

            // Spieleinsatz eingeben
            double spieleinsatz;
            Thread.Sleep(2000);
            Console.WriteLine("");
            Console.Write("Bitte gib deinen Spieleinsatz ein CHF: ");
            // Überprüfung
            while (!double.TryParse(Console.ReadLine(), out spieleinsatz) || spieleinsatz < 0)
            {
                Console.WriteLine("Ungültiger Spieleinsatz. Bitte gib einen gültigen Betrag ein:");
            }

            // Alter eingeben
            int alter;
            Thread.Sleep(2000);
            Console.WriteLine("");
            Console.Write("Bitte gib dein Alter ein: ");
            // Überprüfung
            while (!int.TryParse(Console.ReadLine(), out alter) || alter < 0 || alter > 120)
            {
                Console.WriteLine("Ungültiges Alter. Bitte gib ein gültiges Alter ein:");
            }
            Console.WriteLine("");
            Console.WriteLine("==================== !SPIELER DATEN! ====================");


            // Spieler mit den eingegebenen Werten erstellen
            return new Spieler(name, spieleinsatz, alter);
        }

    }
}

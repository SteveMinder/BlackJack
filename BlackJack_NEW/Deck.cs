using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_NEW
{

    internal class Deck
    {
        public List<string> Deckkarte { get; protected set; }
        public bool Karteausgeben { get; protected set; }

        private static readonly string[] Farben = { "Herz", "Karo", "Kreuz", "Pik" };
        private static readonly string[] Werte = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Bube", "Dame", "König", "Ass" };
        private static readonly Dictionary<string, char> FarbSymbole = new Dictionary<string, char> { { "Herz", '♥' }, { "Karo", '♦' }, { "Kreuz", '♣' }, { "Pik", '♠' } };

        public Deck()
        {
            Deckkarte = new List<string>();

            foreach (var farbe in Farben)
            {
                foreach (var wert in Werte)
                {
                    Deckkarte.Add($"{wert} von {farbe}");
                }
            }
        }


        public bool IstDeckLeer()
        {
            return Deckkarte.Count == 0;
        }

        public string GetKarte(bool Karteausgeben)
        {
            this.Karteausgeben = Karteausgeben;

            if (Karteausgeben && !IstDeckLeer())
            {
                Random random = new Random();
                int randomindex = random.Next(Deckkarte.Count);
                string gezogeneKarte = Deckkarte[randomindex];
                Deckkarte.RemoveAt(randomindex);  // Karte aus dem Deck entfernen
                return gezogeneKarte;
            }
            else if (IstDeckLeer())
            {
                return "Keine Karten mehr im Deck";
            }
            else
            {
                return "nicht gefunden";
            }
        }

        // Methode um die Karte als Grafik darzustellen

        public void PrintKarteGrafisch(string karte)
        {
            // Karte zerlegen in Wert und Farbe
            string[] parts = karte.Split(new[] { " von " }, StringSplitOptions.None);
            string wert = parts[0];
            string farbe = parts[1];

            // Farbensymbol zuordnen
            char symbol;
            if (FarbSymbole.TryGetValue(farbe, out symbol))
            {
                // Länge des Werts bestimmen
                string obenLinks = wert.Length == 2 ? wert : wert.Substring(0, 1);
                string untenRechts = wert.Length == 2 ? wert : wert.Substring(0, 1);

                // ASCII-Art der Karte erstellen
                string[] cardRepresentation = new string[]
                {
            "┌─────────┐",
            $"│{obenLinks.PadRight(2)}       │",
            "│         │",
            $"│    {symbol}    │",
            "│         │",
            $"│       {untenRechts.PadLeft(2)}│",
            "└─────────┘"
                };

                // Karte ausgeben
                foreach (string line in cardRepresentation)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("Symbol nicht gefunden für Farbe: " + farbe);
            }
        }
    }
}

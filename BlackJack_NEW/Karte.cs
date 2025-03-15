using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_NEW
{
    internal class Karte
    {
        public string Spielfarben { get; protected set; }
        public string Kartenwert { get; protected set; }

      
        public int Zahlenwert(string kartenwert)
        {
            this.Kartenwert = kartenwert;

            switch (Kartenwert)
            {
                // BUBE
                case "Bube von Herz":
                case "Bube von Karo":
                case "Bube von Kreuz":
                case "Bube von Pik":
                // DAME
                case "Dame von Herz":
                case "Dame von Karo":
                case "Dame von Kreuz":
                case "Dame von Pik":
                // KÖNIG
                case "König von Herz":
                case "König von Karo":
                case "König von Kreuz":
                case "König von Pik":
                    return 10;
                // ASS
                case "Ass von Herz":
                case "Ass von Karo":
                case "Ass von Kreuz":
                case "Ass von Pik":
                    return 11;
                // 2
                case "2 von Herz":
                case "2 von Karo":
                case "2 von Kreuz":
                case "2 von Pik":
                    return 2;
                // 3
                case "3 von Herz":
                case "3 von Karo":
                case "3 von Kreuz":
                case "3 von Pik":
                    return 3;
                // 4
                case "4 von Herz":
                case "4 von Karo":
                case "4 von Kreuz":
                case "4 von Pik":
                    return 4;
                // 5
                case "5 von Herz":
                case "5 von Karo":
                case "5 von Kreuz":
                case "5 von Pik":
                    return 5;
                // 6
                case "6 von Herz":
                case "6 von Karo":
                case "6 von Kreuz":
                case "6 von Pik":
                    return 6;
                // 7
                case "7 von Herz":
                case "7 von Karo":
                case "7 von Kreuz":
                case "7 von Pik":
                    return  7;
                // 8
                case "8 von Herz":
                case "8 von Karo":
                case "8 von Kreuz":
                case "8 von Pik":
                    return 8;
                // 9
                case "9 von Herz":
                case "9 von Karo":
                case "9 von Kreuz":
                case "9 von Pik":
                    return 9;
                // 10
                case "10 von Herz":
                case "10 von Karo":
                case "10 von Kreuz":
                case "10 von Pik":
                    return 10;
                default: return 0;
            }
        }
    }
}
using BlackJack_NEW;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
//using System.ComponentModel.Design;
//using System.Linq;
//using System.Net.Mail;
//using System.Reflection;
//using System.Text;
//using System.Threading;
//using System.Threading.Channels;
//using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlackJack_NEW
{
    internal class Tisch
    {

        public Spieler Spieler1 { get; protected set; }
        public Karte Karte1 { get; protected set; }
        public Deck Deck1 { get; protected set; }
        public Deck Deck2 { get; protected set; }
        public Deck Deck3 { get; protected set; }

        public Tisch(Spieler spieler, Karte karte1, Deck deck1, Deck deck2, Deck deck3)
        {
            this.Spieler1 = spieler;
            this.Karte1 = karte1;
            this.Deck1 = deck1;
            this.Deck2 = deck2;
            this.Deck3 = deck3;

        }

        public void StateMachine()
        {
            // Interne Variablen

            int            state                = 0;
            bool           running              = true;
            double         spieleinsatzGeld     = 0;
            double         restbestand          = Spieler1.Spieleinsatz;
            int            wertspielerkarten    = 0;
            int            wertdealerkarten     = 0;
            string         spielerkarte         = "0";
            string         dealerkarte          = "0";
            int            kartenwertspieler    = 0;
            int            kartenwertdealer     = 0;
            bool           verdoppelt           = false;
            int            Spielnummer          = 0;
            int            delay                = 2000;
            int            DeckZähler           = 1;
            bool           Deck1Sperren         = false;
            bool           Deck2Sperren         = false;
            bool           Deck3Sperren         = false;
            bool           DeckLeer             = false;


            // Methode zur Überprüfung ob ASS als Wert 1 oder 11 gezählt wird
            bool PrüfungAufAss(int Kartenwert, int AktuellerWert)
            {
                if (Kartenwert == 11 && AktuellerWert > 10)
                {
                    return true;
                }
                else
                    return false;
            }

            // Methode zum Deck wechsel
            bool DeckWechsel(bool Deckistleer, bool Deckgesperrt)
            {
                if (Deckistleer && !Deckgesperrt)
                {
                    Console.WriteLine("====!!BREAK!!====");
                    Thread.Sleep(delay);
                    Console.WriteLine("Das Deck " + DeckZähler + " ist leer");
                    Thread.Sleep(delay);
                    Console.WriteLine("Neues Deck....");
                    DeckZähler++;
                    Thread.Sleep(delay);
                    Console.WriteLine("====!!CONTINUE!!====");
                    return true;
                }
                else
                {
                    return false;
                }  
            }

            // Methode Deckprüfung
            void Deckprüfung()
            {
                // Deck wechsel

                if (DeckWechsel(Deck1.IstDeckLeer(), Deck1Sperren))
                {
                    Deck1Sperren = true;
                }

                if (DeckWechsel(Deck2.IstDeckLeer(), Deck2Sperren))
                {
                    Deck2Sperren = true;
                }

                if (DeckWechsel(Deck3.IstDeckLeer(), Deck3Sperren))
                {
                    Deck3Sperren = true;
                    DeckLeer = true;
                    Console.WriteLine("Kein Deck vorhanden, spiel ist beendet.");
                    state = 111;
                }
            }

            while (running)
            {
                // Deck prüfen
                Deckprüfung();

                // STATE MACHINE
                switch (state)
                {
                    //=================================================================================== CASE 0 Spielbegrüssung initialisierung
                    case 0:
                        Thread.Sleep(delay);
                        Console.WriteLine("");
                        Console.WriteLine("Ahoi, Matros*in" + " " + Spieler1.Name + " " + "ich begrüsse dich zum Black Jack");
                        Console.WriteLine("");
                        Thread.Sleep(delay);
                        Console.WriteLine("Willst du das Spiel starten?");
                        Console.WriteLine("Bitte 'j' für Ja oder 'n' für Nein eingeben.");

                        // evtl. Implementation der Spielanleitung (.txt bereits im Projekt eingebettet)
                        // ========================================

                        string startup = Console.ReadLine();
                        Console.WriteLine("");

                        if (startup == "j")
                        {
                            Spielnummer++;
                            state++; // Wechsel zum nächsten Zustand
                        }
                        else if (startup == "n")
                        {
                            Console.WriteLine("Schade vielleicht ein anderes Mal");
                            state = 111;
                        }
                        else
                            state = 0;
                        break;
                    //=================================================================================== CASE 1 Alter prüfung
                    case 1:
                        Console.WriteLine("==================== !JUGENDSCHUTZ! ====================");
                        Console.WriteLine("");
                        Thread.Sleep(delay);
                        Console.WriteLine("Dein Alter ist " + Spieler1.alter);
                        Thread.Sleep(delay);

                        if (Spieler1.alter >= 18)
                        {
                            
                            Console.WriteLine("Alter geprüft, du bist volljährig!");
                            Console.WriteLine("");
                            Console.WriteLine("==================== !JUGENDSCHUTZ! ====================");
                            Thread.Sleep(delay);
                            Console.WriteLine("");
                            Console.Write("Ich wünsche dir viel Glück, das Spiel startet gleich");

                            for (int s = 0; s < 8; s++)
                            {
                                Console.Write(".");
                                Thread.Sleep(delay);

                            }
                            Console.WriteLine("");
                            Thread.Sleep(delay);
                            state++;
                        }
                        else
                        {
                            Console.WriteLine("Alter geprüft, du bist leider nicht volljährig");
                            Console.WriteLine("==================== !JUGENDSCHUTZ! ====================");
                            state = 111;
                        }
                            
                           
                        break;
                    //=================================================================================== CASE 2 Einsatz von Spieler definieren und überprüfen
                    case 2:
                        Console.WriteLine("");
                        Console.WriteLine("=================" +" SPIELNUMMER "+ Spielnummer + " =================");
                        Console.WriteLine("KONTOSTAND CHF " + restbestand + ".-");
                        Thread.Sleep(delay);
                        if (restbestand <= 0)
                        {
                            Console.WriteLine("Du hast leider nicht genug Geld für die nächste Runde... Tur mir Leid :((");
                            state = 111;
                        }
                        else {
                            Console.WriteLine("");
                            Console.Write("Bitte gib deinen Einsatz in CHF: ");
                            // Überprüfung ob eine Zahl eingegeben wird
                            while (!double.TryParse(Console.ReadLine(), out spieleinsatzGeld) || spieleinsatzGeld < 0 || restbestand < spieleinsatzGeld)
                            {
                                Console.WriteLine("Ungültiger Spieleinsatz. Bitte gib einen gültigen Betrag ein:");
                            }
                            Console.WriteLine("");
                            Console.WriteLine("Dein Einsatz ist CHF: " + spieleinsatzGeld + ".-");
                            restbestand = restbestand - spieleinsatzGeld;
                            Thread.Sleep(delay);
                            Console.WriteLine("Dein neuer Kontostand ist CHF: " + restbestand + ".-");
                            Console.WriteLine("");
                            Thread.Sleep(delay);

                            if (restbestand < 0)
                            {
                                Console.WriteLine("Du hast leider nicht genug Geld für die nächste Runde... Tut mir Leid :((");
                                state = 111;
                            }
                            else
                            {
                                state++;
                            }
                        }
                           
                        break;
                    //=================================================================================== CASE 3 erste Spielkarte für Dealer ausgeben und anzeigen, zweite Karte ausgeben aber nicht anzeigen
                    case 3:
                        Console.WriteLine("Die erste Karte des Dealers:");
                        Thread.Sleep(delay);

                        if (DeckZähler == 1)
                        {
                            dealerkarte = Deck1.GetKarte(true);
                            Deck1.PrintKarteGrafisch(dealerkarte);
                        }
                        else if (DeckZähler == 2)
                        {
                            dealerkarte = Deck2.GetKarte(true);
                            Deck2.PrintKarteGrafisch(dealerkarte);
                        }
                        else if (DeckZähler == 3)
                        {
                            dealerkarte = Deck3.GetKarte(true);
                            Deck3.PrintKarteGrafisch(dealerkarte);
                        }

                        // Console.WriteLine(dealerkarte);
                        kartenwertdealer = Karte1.Zahlenwert(dealerkarte);
                        wertdealerkarten = wertdealerkarten + kartenwertdealer;

                        Deckprüfung();

                        if (DeckZähler == 1)
                        {
                            dealerkarte = Deck1.GetKarte(true);
                        }
                        else if (DeckZähler == 2)
                        {
                            dealerkarte = Deck2.GetKarte(true);
                        }
                        else if (DeckZähler == 3)
                        {
                            dealerkarte = Deck3.GetKarte(true);
                        }

                        kartenwertdealer = Karte1.Zahlenwert(dealerkarte);
                        // Überprüfung ob ASS als Wert 1 oder 11 gezählt wird
                        if (PrüfungAufAss(kartenwertdealer, wertdealerkarten))
                        {
                            wertdealerkarten = wertdealerkarten + 1;
                        }
                        else
                        {
                            wertdealerkarten = wertdealerkarten + kartenwertdealer;
                        }
                  
                        Thread.Sleep(delay);
                        state++;

                        break;
                    //=================================================================================== CASE 4 Die ersten zwei Spielkarten für Spieler ausgeben und auf BlackJack prüfen
                    case 4:
                        Console.WriteLine("Deine zwei Karten sind: ");

                            if (DeckZähler == 1)
                            {
                                spielerkarte = Deck1.GetKarte(true);
                                Deck1.PrintKarteGrafisch(spielerkarte);
                            }
                            else if (DeckZähler == 2)
                            {
                                spielerkarte = Deck2.GetKarte(true);
                                Deck2.PrintKarteGrafisch(spielerkarte);
                            }
                            else if (DeckZähler == 3)
                            {
                                spielerkarte = Deck3.GetKarte(true);
                                Deck3.PrintKarteGrafisch(spielerkarte);
                            }

                            // Console.WriteLine(spielerkarte);
                            kartenwertspieler = Karte1.Zahlenwert(spielerkarte);
                            wertspielerkarten = wertspielerkarten + kartenwertspieler;
                            Thread.Sleep(delay);

                            Deckprüfung();

                            if (DeckZähler == 1)
                            {
                                spielerkarte = Deck1.GetKarte(true);
                                Deck1.PrintKarteGrafisch(spielerkarte);
                            }
                            else if (DeckZähler == 2)
                            {
                                spielerkarte = Deck2.GetKarte(true);
                                Deck2.PrintKarteGrafisch(spielerkarte);
                            }
                            else if (DeckZähler == 3)
                            {
                                spielerkarte = Deck3.GetKarte(true);
                                Deck3.PrintKarteGrafisch(spielerkarte);
                            }

                            // Console.WriteLine(spielerkarte);
                            kartenwertspieler = Karte1.Zahlenwert(spielerkarte);

                            // Überprüfung ob ASS als Wert 1 oder 11 gezählt wird
                            if (PrüfungAufAss(kartenwertspieler, wertspielerkarten))
                            {
                                wertspielerkarten = wertspielerkarten + 1;
                            }
                            else
                            {
                                wertspielerkarten = wertspielerkarten + kartenwertspieler;
                            }
                            

                            Thread.Sleep(delay);
                            Console.WriteLine("Dein Kartenwert ist: " + wertspielerkarten);

                        if (wertspielerkarten == 21)
                        {
                            state = 103;

                        }
                        else if (wertspielerkarten == 9 || wertspielerkarten == 10 || wertspielerkarten == 11) // verdoppeln
                        {
                            state = 8;
                        }

                        else
                            state++;

                        break;
                    //=================================================================================== CASE 5 Hit / stand
                    case 5:
                        Console.WriteLine("");
                        Console.WriteLine("Hit or Stand?");
                        Console.WriteLine("Bitte 'h' für Hit oder 's' für Stand eingeben.");
                        string hitorstand = Console.ReadLine();
                        if (hitorstand == "h")
                        {
                            Console.WriteLine("========== Hit ==========");
                            Thread.Sleep(delay);
                            state = 6;
                        }
                        else if (hitorstand == "s")
                        {
                            Console.WriteLine("========== Stand ==========");
                            state = 7;
                        }
                        else
                        {
                            Console.WriteLine("Ungültige Eingabe. Bitte 'h' für Hit oder 's' für Stand eingeben.");
                            state = 5;
                        }
                            
                        break;
                    //=================================================================================== CASE 6 hit (noch eine Karte)
                    case 6:
                        if (DeckZähler == 1)
                        {
                            spielerkarte = Deck1.GetKarte(true);
                            Deck1.PrintKarteGrafisch(spielerkarte);
                        }
                        else if (DeckZähler == 2)
                        {
                            spielerkarte = Deck2.GetKarte(true);
                            Deck2.PrintKarteGrafisch(spielerkarte);
                        }
                        else if (DeckZähler == 3)
                        {
                            spielerkarte = Deck3.GetKarte(true);
                            Deck3.PrintKarteGrafisch(spielerkarte);
                        }

                        // Console.WriteLine("Deine Karte: " + spielerkarte);
                        kartenwertspieler = Karte1.Zahlenwert(spielerkarte);

                        // Überprüfung ob ASS als Wert 1 oder 11 gezählt wird
                        if (PrüfungAufAss(kartenwertspieler, wertspielerkarten))
                        {
                            wertspielerkarten = wertspielerkarten + 1;
                        }
                        else
                        {
                            wertspielerkarten = wertspielerkarten + kartenwertspieler;
                        }
                      
                        Thread.Sleep(delay);
                        Console.WriteLine("Dein Kartenwert ist: " + wertspielerkarten);
                        Thread.Sleep(delay);

                        if (wertspielerkarten > 21)
                        {
                            state = 101;
                        }
                        else
                        {
                            state = 5;
                        }

                        break;

                    //=================================================================================== CASE 7 stand (bleiben)
                    case 7:

                        if (wertdealerkarten < 17)
                        {
                            state = 10;
                        }
                        else
                        {
                            state = 9;

                        }
                        break;
                    //=================================================================================== CASE 8 double
                    case 8:
                        Console.WriteLine("Du kannst deinen Einsatz verdoppeln");
                        Console.WriteLine("Bitte 'j' für Ja oder 'n' für Nein eingeben.");
                        string verdoppeln = Console.ReadLine();

                        if (verdoppeln == "j")
                        {
                            restbestand = restbestand - spieleinsatzGeld;

                            if (DeckZähler == 1)
                            {
                                spielerkarte = Deck1.GetKarte(true);
                                Deck1.PrintKarteGrafisch(spielerkarte);
                            }
                            else if (DeckZähler == 2)
                            {
                                spielerkarte = Deck2.GetKarte(true);
                                Deck2.PrintKarteGrafisch(spielerkarte);
                            }
                            else if (DeckZähler == 3)
                            {
                                spielerkarte = Deck3.GetKarte(true);
                                Deck3.PrintKarteGrafisch(spielerkarte);
                            }
                            // Console.WriteLine(spielerkarte);
                            kartenwertspieler = Karte1.Zahlenwert(spielerkarte);

                            // Überprüfung ob ASS als Wert 1 oder 11 gezählt wird
                            if (PrüfungAufAss(kartenwertspieler, wertspielerkarten))
                            {
                                wertspielerkarten = wertspielerkarten + 1;
                            }
                            else
                            {
                                wertspielerkarten = wertspielerkarten + kartenwertspieler;
                            }

                            Console.WriteLine("Dein Kartenwert ist: " + wertspielerkarten);
                            verdoppelt = true;


                            if (wertspielerkarten > 21)
                            {
                                state = 101;
                            }
                            else
                            {
                                state = 7;
                            }

                        }
                        else if (verdoppeln == "n")
                        {
                            state = 5;
                        }
                        else
                        {
                            Console.WriteLine("Ungültige Eingabe. Bitte 'j' für Ja oder 'n' für Nein eingeben.");
                            state = 8;
                        }
                        
                        
                        break;
                    //=================================================================================== CASE 9 vergleich mit Bank
                    case 9:
                        if ((wertspielerkarten > wertdealerkarten) || wertdealerkarten >21)
                        {
                            state = 102;
                        }
                        else if (wertspielerkarten == wertdealerkarten)
                        {
                            state = 104;
                        }
                        else
                        {
                            state = 101;
                        }
                        break;
                    //=================================================================================== CASE 10 Bank Hit Karte
                    case 10:

                        if (DeckZähler == 1)
                        {
                            dealerkarte = Deck1.GetKarte(true);
                        }
                        else if (DeckZähler == 2)
                        {
                            dealerkarte = Deck2.GetKarte(true);
                        }
                        else if (DeckZähler == 3)
                        {
                            dealerkarte = Deck3.GetKarte(true);
                        }
                        kartenwertdealer = Karte1.Zahlenwert(dealerkarte);
                        // Überprüfung ob ASS als Wert 1 oder 11 gezählt wird
                        if (PrüfungAufAss(kartenwertdealer, wertdealerkarten))
                        {
                            wertdealerkarten = wertdealerkarten + 1;
                        }
                        else
                        {
                            wertdealerkarten = wertdealerkarten + kartenwertdealer;
                        }
                        Thread.Sleep(delay);
                        state = 7;
                        break;
                    //=================================================================================== CASE 101 BUST
                    case 101:
                        Console.WriteLine("");
                        Console.WriteLine("Du hast gegen die Bank verloren");
                        Thread.Sleep(delay);
                        Console.WriteLine("Dein Kartenwert ist: " + wertspielerkarten);
                        Thread.Sleep(delay);
                        Console.WriteLine("Kartenwert der Bank " + wertdealerkarten);
                        Thread.Sleep(delay);
                        Console.WriteLine("");
                        Console.WriteLine("Dein Verlust entspricht CHF ");
                        if (verdoppelt)
                        {
                            Console.WriteLine(spieleinsatzGeld * 2 + ".-");
                            Console.WriteLine("Dein neuer Kontostand CHF ");
                            Console.WriteLine(restbestand + ".-");
                            Thread.Sleep(delay);
                            state = 110;

                        }
                        else
                        {
                            Console.WriteLine(spieleinsatzGeld + ".-");
                            Console.WriteLine("Dein neuer Kontostand CHF ");
                            Console.WriteLine(restbestand + ".-");
                            Thread.Sleep(delay);
                            state = 110;
                        }
                        break;
                    //=================================================================================== CASE 102 WIN
                    case 102:
                        Console.WriteLine("");
                        Console.WriteLine("Du hast gegen die Bank gewonnen");
                        Thread.Sleep(delay);
                        Console.WriteLine("Dein Kartenwert: " + wertspielerkarten);
                        Thread.Sleep(delay);
                        Console.WriteLine("Kartenwert der Bank " + wertdealerkarten);
                        Thread.Sleep(delay);
                        Console.WriteLine("");
                        Console.WriteLine("Dein Gewinn entspricht CHF ");
                        if (verdoppelt)
                        {
                            Console.WriteLine(spieleinsatzGeld * 2 + ".-");
                            Console.WriteLine("Dein neuer Kontostand CHF ");
                            restbestand = restbestand + (spieleinsatzGeld * 2) + (spieleinsatzGeld * 2);
                            Console.WriteLine(restbestand + ".-");
                            Thread.Sleep(delay);
                            state = 110;

                        }
                        else
                        {
                            Console.WriteLine(spieleinsatzGeld);
                            Console.WriteLine("Dein neuer Kontostand CHF ");
                            restbestand = restbestand + (spieleinsatzGeld * 2);
                            Console.WriteLine(restbestand + ".-");
                            Thread.Sleep(delay);
                            state = 110;

                        }
                        break;
                    //=================================================================================== CASE 103 WIN Blackjack nach 2 Karten
                    case 103:
                        Console.WriteLine("");
                        Console.WriteLine("==========! BLACK JACK !==========");
                        Console.WriteLine("Du hast ein BlackJack dein Gewinn entspricht CHF " + spieleinsatzGeld * 1.5 + ".-");
                        Thread.Sleep(delay);
                        Console.WriteLine("Dein neuer Kontostand CHF ");
                        restbestand = restbestand + spieleinsatzGeld + (spieleinsatzGeld * 1.5);
                        Console.WriteLine(restbestand + ".-");
                        Thread.Sleep(delay);
                        state = 110;
                        break;
                    //=================================================================================== CASE 104 Unentschieden
                    case 104:
                        Console.WriteLine("");
                        Console.WriteLine("Unentschieden");
                        Thread.Sleep(delay);
                        Console.WriteLine("Dein Kartenwert: " + wertspielerkarten);
                        Thread.Sleep(delay);
                        Console.WriteLine("Kartenwert der Bank " + wertdealerkarten);
                        Thread.Sleep(delay);
                        Console.WriteLine("");
                        Console.WriteLine("Dein neuer Kontostand CHF ");
                        if (verdoppelt)
                        {
                            restbestand = restbestand + (spieleinsatzGeld * 2);
                        }
                        else
                        {
                            restbestand = restbestand + spieleinsatzGeld;
                        }
                        Console.WriteLine(restbestand + ".-");
                        Thread.Sleep(delay);
                        state = 110;
                        break;

                    //=================================================================================== CASE 110 REPLAY
                    case 110:
                        wertspielerkarten = 0;
                        wertdealerkarten = 0;
                        verdoppelt = false;
                        Console.WriteLine("_________________________________________________________________");
                        Console.WriteLine("Willst du noch einmal Spielen?");
                        Console.WriteLine("Bitte 'j' für Ja oder 'n' für Nein eingeben.");
                        string Replay = Console.ReadLine();

                        if (Replay == "j")
                        {
                            Spielnummer++;
                            state = 2;
                        }
                        else if (Replay == "n")
                        {
                            state = 111;
                        }
                        else
                        {
                            Console.WriteLine("Ungültige Eingabe. Bitte 'j' für Ja oder 'n' für Nein eingeben.");
                            state = 110;
                        }
                        break;

                    //=================================================================================== CASE 111 Spiel beenden
                    case 111:

                        Console.WriteLine("Spiel beendet!");

                        if (restbestand > Spieler1.Spieleinsatz)
                        {
                            Console.WriteLine("Du hast einen saftigen Gewinn gemacht :)))");
                            Thread.Sleep(delay);
                            Console.WriteLine("DEIN GEWINN CHF: ");
                            if (DeckLeer)
                            {
                                Console.WriteLine((restbestand + spieleinsatzGeld) - Spieler1.Spieleinsatz + ".-");
                                Thread.Sleep(delay);
                                Console.WriteLine("KONTOSTAND: " + (restbestand + spieleinsatzGeld) + ".-");
                            }
                            else
                            {
                                Console.WriteLine(restbestand - Spieler1.Spieleinsatz + ".-");
                                Thread.Sleep(delay);
                                Console.WriteLine("KONTOSTAND: " + restbestand + ".-");
                            }
                            Thread.Sleep(delay);
                            state = 111;
                            Console.WriteLine("Spiel beendet!");
                            running = false; // Beenden der Schleife
                        }
                        else if (restbestand == Spieler1.Spieleinsatz)
                        {
                            Console.WriteLine("Du hast keinen Gewinn/Verlust gemacht");
                            Thread.Sleep(delay);
                            if (DeckLeer)
                            {
                                Console.WriteLine("KONTOSTAND: " + (restbestand + spieleinsatzGeld) + ".-");
                            }
                            else
                            {
                                Console.WriteLine("KONTOSTAND: " + restbestand + ".-");
                            }
                            Thread.Sleep(delay);
                            Console.WriteLine("Spiel beendet!");
                            running = false; // Beenden der Schleife
                        }
                        else if (restbestand < Spieler1.Spieleinsatz)
                        {
                            Console.WriteLine("Du hast einen saftigen Verlust gemacht :(((");
                            Thread.Sleep(delay);
                            Console.WriteLine("DEIN VERLUST CHF: ");
                            if (DeckLeer)
                            {
                                Console.WriteLine(Spieler1.Spieleinsatz - (restbestand + spieleinsatzGeld) + ".-");
                                Thread.Sleep(delay);
                                Console.WriteLine("KONTOSTAND: " + (restbestand + spieleinsatzGeld) + ".-");
                            }
                            else
                            {
                                Console.WriteLine(Spieler1.Spieleinsatz - restbestand + ".-");
                                Thread.Sleep(delay);
                                Console.WriteLine("KONTOSTAND: " + restbestand + ".-");
                            }
                            Thread.Sleep(delay);
                            Console.WriteLine("Spiel beendet!");
                            running = false; // Beenden der Schleife
                        }
                            break;

                    //===================================================================================
                    default:
                        state = 111;
                        break;
                }
            }
        }
    }
}



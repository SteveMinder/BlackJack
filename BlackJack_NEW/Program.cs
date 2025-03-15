using System.Security.Cryptography.X509Certificates;

namespace BlackJack_NEW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Setze die Konsolencodierung auf UTF-8
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Begrüssung
            Console.WriteLine("=====!WELCOME TO THE GREAT BLACK JACK GAME!=====");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("==============================================");
            Console.WriteLine("OOP Kompetenznachweis 2.0 (TEKO BERN)");
            Console.Write("Created by ");
            Thread.Sleep(2000);
            Console.WriteLine("Steve Minder B-NIA-24-A-a");
            Console.WriteLine("==============================================");
            Console.WriteLine("");
            Thread.Sleep(2000);


            // Objekte
            Spieler spieler1 = Spieler.ErstelleSpielerVonKonsole();
            Deck deck1 = new Deck();
            Deck deck2 = new Deck();
            Deck deck3 = new Deck();
            Karte karte1 = new Karte(); 
            Tisch Tisch1 = new Tisch(spieler1,karte1,deck1,deck2,deck3);
            Tisch1.StateMachine();

        }
    }
}
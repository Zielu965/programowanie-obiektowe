using System;
using System.Collections.Generic;
using System.Text;
namespace WypozyczalniaZimowa.Modele
{
    // Klasa reprezentująca klienta wypożyczalni
    internal class Klient
    {
        // Unikalny identyfikator klienta (zgodny z bazą danych)
        public string Id { get; set; }

        // Imię klienta
        public string Imie { get; set; }

        // Nazwisko klienta
        public string Nazwisko { get; set; }

        // Numer telefonu klienta
        public string Telefon { get; set; }

        // Konstruktor domyślny
        public Klient()
        {
        }

        // Czytelna prezentacja klienta
        public override string ToString()
        {
            return $"[{Id}] {Imie} {Nazwisko}, tel: {Telefon}";
        }
    }
}

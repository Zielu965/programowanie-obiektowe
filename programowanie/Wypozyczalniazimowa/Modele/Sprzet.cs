using System;
using System.Collections.Generic;
using System.Text;
namespace WypozyczalniaZimowa.Modele
{
    // Klasa reprezentująca sprzęt zimowy w wypożyczalni
    internal class Sprzet
    {
        // Unikalny identyfikator sprzętu (zgodny z bazą danych)
        public string Id { get; set; }

        // Nazwa sprzętu (np. Łyżwy, Kask)
        public string Nazwa { get; set; }

        // Typ sprzętu (np. Lyzwy, Kask)
        public string Typ { get; set; }

        // Rozmiar sprzętu (np. 42, M, L)
        public string Rozmiar { get; set; }

        // Cena wypożyczenia za dobę
        public decimal CenaZaDobe { get; set; }

        // Informacja czy sprzęt jest dostępny
        public bool Dostepny { get; set; }

        // Konstruktor domyślny
        public Sprzet()
        {
            Dostepny = true;
        }

        // Czytelna prezentacja sprzętu
        public override string ToString()
        {
            string status = Dostepny ? "Dostępny" : "Wypożyczony";
            return $"[{Id}] {Nazwa} ({Typ}, {Rozmiar}) - {CenaZaDobe} zł/doba - {status}";
        }
    }
}

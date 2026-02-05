using System;

namespace WypozyczalniaZimowa.Modele
{
    // Klasa reprezentująca wypożyczenie sprzętu
    internal class Wypozyczenie
    {
        // Unikalny identyfikator wypożyczenia
        public int Id { get; set; }

        // Klient, który wypożycza sprzęt
        public Klient Klient { get; set; }

        // Sprzęt, który jest wypożyczany
        public Sprzet Sprzet { get; set; }

        // Data rozpoczęcia wypożyczenia
        public DateTime DataOd { get; set; }

        // Data zakończenia wypożyczenia (null jeśli trwa)
        public DateTime? DataDo { get; set; }

        // Informacja czy wypożyczenie jest aktywne
        public bool Aktywne { get; set; }

        // Konstruktor domyślny
        public Wypozyczenie()
        {
            Aktywne = true;
            DataOd = DateTime.Now;
        }

        // Konstruktor z parametrami
        public Wypozyczenie(Klient klient, Sprzet sprzet)
        {
            Klient = klient;
            Sprzet = sprzet;
            DataOd = DateTime.Now;
            Aktywne = true;
        }

        // Metoda kończąca wypożyczenie
        public void Zakoncz()
        {
            DataDo = DateTime.Now;
            Aktywne = false;
            Sprzet.Dostepny = true;
        }

        // Czytelna prezentacja wypożyczenia
        public override string ToString()
        {
            string status = Aktywne ? "AKTYWNE" : "ZAKOŃCZONE";
            return $"[{Id}] {Klient.Imie} {Klient.Nazwisko} -> {Sprzet.Nazwa} ({status})";
        }
    }
}

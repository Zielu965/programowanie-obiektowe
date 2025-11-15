using System;
using System.Collections.Generic;
using System.Text;

namespace lab2zad2
{
    internal class Osoba
    {

        private string imie, nazwisko;
        private int wiek;

        public string Imie
        {
            get
            {
                return imie;
            }
            set
            {
                if (value.Length >= 2)
                {
                    imie = value;

                }
                else
                {
                    throw new ArgumentException("Imie musi zawierać co najmniej 2 znaki.");

                }
            }
        }
        public string Nazwisko
        {
            get
            {
                return nazwisko;
            }
            set
            {
                if (value.Length >= 2)
                {
                    nazwisko = value;
                }
                else
                {
                    throw new ArgumentException("Nazwisko musi zawierać co najmniej 2 znaki.");
                }
            }
        }
        public int Wiek
        {
            get
            {
                return wiek;
            }
            set
            {
                if (value >= 0)
                {
                    wiek = value;
                }
                else
                {
                    throw new ArgumentException("Wiek musi być liczbą dodatnia.");
                }
            }
        }

        public Osoba(string imie, string nazwisko, int wiek)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
        }
        public void WyswietlInformacje()
        {
            Console.WriteLine($"Imię: {Imie}, Nazwisko: {Nazwisko}, Wiek: {Wiek}");
        }   
    }
}

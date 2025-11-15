using System;
using System.Collections.Generic;
using System.Text;

namespace lab2zad3
{
    internal class Student
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        private List<int> oceny;

        public Student(string imie, string nazwisko)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            oceny = new List<int>();
        }
        public double SredniaOcen
        {
            get
            {
                if (oceny.Count == 0)
                    return 0;
                return oceny.Average();
            }

        }
        public void DodajOcene(int ocena)
        {
            oceny.Add(ocena);
        }
    }
}
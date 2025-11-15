using System;
using System.Collections.Generic;
using System.Text;

namespace lab2zad4
{
    internal class Licz
    {
        private int value;
        public Licz(int initialValue)
        {
            value = initialValue;
        }
        public void Dodaj(int toAdd)
        {             value += toAdd;
        }
        public void Odejmij(int toSubtract)
        {
            value -= toSubtract;
        }
        public void WypiszStan()
        {
            Console.WriteLine("Aktualna wartość: " + value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace lab2zad5
{
    internal class Sumator
    {
        private int[] Liczby { get; set; }
        public Sumator(int[] liczby)
        {
            Liczby = liczby;
        }
        public int Suma()
        {
            int sumaLiczb = 0;

            for (int i =0; i < Liczby.Length; i++)
            {
                sumaLiczb += Liczby[i];
            }
            return sumaLiczb;
        }
        public int SumaPodziel2()
        {
            int sumaLiczb = 0;
            for (int i = 0; i < Liczby.Length; i++)
            {
                if (Liczby[i] % 2 == 0)
                {
                    sumaLiczb += Liczby[i];
                }
            }
            return sumaLiczb;
        }
        public int IleElementow()
        { return Liczby.Length;
        }
        public void WypiszElementy()
        {
            for (int i = 0; i < Liczby.Length; i++)
            {
                Console.Write(Liczby[i] + " ");
            }
        }
        public void WypiszElementyZakres(int lowIndex, int highIndex)
        { for (int i = lowIndex; i <= highIndex; i++) 
            { if (i >=0 && i < Liczby.Length)
                { Console.WriteLine(Liczby[i]);
                }
} }
    }
}

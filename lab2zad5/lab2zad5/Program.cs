// See https://aka.ms/new-console-template for more information


using lab2zad5;

Sumator sumator = new Sumator(new int[] { 11, 2, 58, 62, 14, 74, 3, 5, 7, 102, 26, 90 });

Console.WriteLine("Liczba elementów w tablicy: ");
sumator.IleElementow();

Console.WriteLine("\nWypisanie elementów tablicy: ");
sumator.WypiszElementy();

Console.WriteLine("\nWypisanie elementów tablicy w zakresie od 3 do 7: ");
sumator.WypiszElementyZakres(3, 7);

Console.WriteLine("\nSuma liczb podzielnych przez 2 : " + sumator.SumaPodziel2());

Console.WriteLine("\nSuma wszystkich liczb: " + sumator.Suma());
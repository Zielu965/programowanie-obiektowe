using System;
using WypozyczalniaZimowa.Modele;
using WypozyczalniaZimowa.Repozytorium;

Console.Title = "Wypożyczalnia Sprzętu Zimowego";

SprzetRepository sprzetRepo = new SprzetRepository();
KlientRepository klientRepo = new KlientRepository();
WypozyczenieRepository wypRepo = new WypozyczenieRepository();

bool dziala = true;

while (dziala)
{
    Console.Clear();
    Console.WriteLine("=== WYPOŻYCZALNIA SPRZĘTU ZIMOWEGO ===");
    Console.WriteLine("1. Dodaj sprzęt");
    Console.WriteLine("2. Wyświetl sprzęt");
    Console.WriteLine("3. Dodaj klienta");
    Console.WriteLine("4. Wyświetl klientów");
    Console.WriteLine("5. Wypożycz sprzęt");
    Console.WriteLine("6. Zwróć sprzęt");
    Console.WriteLine("0. Wyjście");
    Console.WriteLine();
    Console.Write("Wybierz opcję: ");

    string opcja = Console.ReadLine();

    Console.Clear();

    switch (opcja)
    {
        case "1":
            Console.Write("ID sprzętu: ");
            string sid = Console.ReadLine();

            Console.Write("Nazwa: ");
            string nazwa = Console.ReadLine();

            Console.Write("Typ: ");
            string typ = Console.ReadLine();

            Console.Write("Rozmiar: ");
            string rozmiar = Console.ReadLine();

            Console.Write("Cena za dobę: ");
            decimal cena;
            if (!decimal.TryParse(Console.ReadLine(), out cena))
            {
                Console.WriteLine("Błędna cena.");
                break;
            }

            Sprzet s = new Sprzet
            {
                Id = sid,
                Nazwa = nazwa,
                Typ = typ,
                Rozmiar = rozmiar,
                CenaZaDobe = cena,
                Dostepny = true
            };

            sprzetRepo.Dodaj(s);
            break;

        case "2":
            sprzetRepo.WyswietlWszystkie();
            break;

        case "3":
            Console.Write("ID klienta: ");
            string kid = Console.ReadLine();

            Console.Write("Imię: ");
            string imie = Console.ReadLine();

            Console.Write("Nazwisko: ");
            string nazwisko = Console.ReadLine();

            Console.Write("Telefon: ");
            string tel = Console.ReadLine();

            Klient k = new Klient
            {
                Id = kid,
                Imie = imie,
                Nazwisko = nazwisko,
                Telefon = tel
            };

            klientRepo.Dodaj(k);
            break;

        case "4":
            klientRepo.WyswietlWszystkie();
            break;

        case "5":
            Console.Write("ID wypożyczenia: ");
            string wid = Console.ReadLine();

            Console.Write("ID klienta: ");
            string kidw = Console.ReadLine();

            Console.Write("ID sprzętu: ");
            string sidw = Console.ReadLine();

            wypRepo.Wypozycz(wid, kidw, sidw);
            break;

        case "6":
            Console.Write("ID wypożyczenia: ");
            string zw = Console.ReadLine();

            wypRepo.Zwroc(zw);
            break;

        case "0":
            dziala = false;
            continue;

        default:
            Console.WriteLine("Nieznana opcja.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Naciśnij klawisz...");
    Console.ReadKey();
}

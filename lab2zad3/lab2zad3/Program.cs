// See https://aka.ms/new-console-template for more information


using lab2zad3;

Student student = new Student("Jan", "Koalski");
student.DodajOcene(4);
student.DodajOcene(3);
student.DodajOcene(5);
student.DodajOcene(2);
student.DodajOcene(1);
Console.WriteLine($"Średnia ocen studenta {student.Imie} {student.Nazwisko} wynosi: {student.SredniaOcen}");

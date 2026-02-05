using System;
using Microsoft.Data.SqlClient;
using WypozyczalniaZimowa.Data;
using WypozyczalniaZimowa.Interfejsy;
using WypozyczalniaZimowa.Modele;

namespace WypozyczalniaZimowa.Repozytorium
{
    // Repozytorium odpowiedzialne za operacje na sprzęcie
    internal class SprzetRepository : ICrud<Sprzet>
    {
        // Dodaje nowy sprzęt do bazy danych
        public void Dodaj(Sprzet s)
        {
            if (s == null ||
                string.IsNullOrWhiteSpace(s.Id) ||
                string.IsNullOrWhiteSpace(s.Nazwa) ||
                string.IsNullOrWhiteSpace(s.Typ) ||
                string.IsNullOrWhiteSpace(s.Rozmiar) ||
                s.CenaZaDobe <= 0)
            {
                Console.WriteLine("Błąd: niepoprawne dane sprzętu.");
                return;
            }

            if (Znajdz(s.Id) != null)
            {
                Console.WriteLine("Błąd: sprzęt o takim ID już istnieje.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))
                {
                    con.Open();

                    string sql = @"INSERT INTO Sprzet
                                   (IdSprzetu, Nazwa, Typ, Rozmiar, CenaZaDobe, Dostepny)
                                   VALUES (@id, @nazwa, @typ, @rozmiar, @cena, @dostepny)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", s.Id);
                        cmd.Parameters.AddWithValue("@nazwa", s.Nazwa);
                        cmd.Parameters.AddWithValue("@typ", s.Typ);
                        cmd.Parameters.AddWithValue("@rozmiar", s.Rozmiar);
                        cmd.Parameters.AddWithValue("@cena", s.CenaZaDobe);
                        cmd.Parameters.AddWithValue("@dostepny", s.Dostepny);

                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Dodano sprzęt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd dodawania sprzętu: " + ex.Message);
            }
        }

        // Usuwa sprzęt po ID
        public void Usun(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Błąd: podaj ID sprzętu.");
                return;
            }

            if (Znajdz(id) == null)
            {
                Console.WriteLine("Nie znaleziono sprzętu.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))
                {
                    con.Open();

                    string sql = "DELETE FROM Sprzet WHERE IdSprzetu = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Usunięto sprzęt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd usuwania sprzętu: " + ex.Message);
            }
        }

        // Wyszukuje sprzęt po ID
        public Sprzet Znajdz(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            try
            {
                using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))
                {
                    con.Open();

                    string sql = @"SELECT IdSprzetu, Nazwa, Typ, Rozmiar, CenaZaDobe, Dostepny
                                   FROM Sprzet
                                   WHERE IdSprzetu = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                return new Sprzet
                                {
                                    Id = r["IdSprzetu"].ToString(),
                                    Nazwa = r["Nazwa"].ToString(),
                                    Typ = r["Typ"].ToString(),
                                    Rozmiar = r["Rozmiar"].ToString(),
                                    CenaZaDobe = Convert.ToDecimal(r["CenaZaDobe"]),
                                    Dostepny = Convert.ToBoolean(r["Dostepny"])
                                };
                            }
                        }
                    }
                }
            }
            catch { }

            return null;
        }

        // Wyświetla listę sprzętu wraz z informacją, kto aktualnie wypożyczył
        public void WyswietlWszystkie()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))
                {
                    con.Open();

                    string sql = @"
                        SELECT 
                            s.IdSprzetu,
                            s.Nazwa,
                            s.Typ,
                            s.Rozmiar,
                            s.CenaZaDobe,
                            s.Dostepny,
                            w.IdKlienta
                        FROM Sprzet s
                        LEFT JOIN Wypozyczenie w 
                            ON s.IdSprzetu = w.IdSprzetu AND w.Aktywne = 1
                        ORDER BY s.IdSprzetu";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        Console.WriteLine("=== LISTA SPRZĘTU ===");

                        bool any = false;

                        while (r.Read())
                        {
                            any = true;

                            string id = r["IdSprzetu"].ToString();
                            string nazwa = r["Nazwa"].ToString();
                            string typ = r["Typ"].ToString();
                            string rozmiar = r["Rozmiar"].ToString();
                            decimal cena = Convert.ToDecimal(r["CenaZaDobe"]);
                            bool dostepny = Convert.ToBoolean(r["Dostepny"]);

                            if (dostepny)
                            {
                                Console.WriteLine(
                                    $"[{id}] {nazwa} ({typ}, {rozmiar}) - {cena} zł/doba - Dostępny"
                                );
                            }
                            else
                            {
                                string idKlienta = r["IdKlienta"]?.ToString();

                                Console.WriteLine(
                                    $"[{id}] {nazwa} ({typ}, {rozmiar}) - {cena} zł/doba - Wypożyczony przez: {idKlienta}"
                                );
                            }
                        }

                        if (!any)
                            Console.WriteLine("Brak sprzętu w bazie.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd wyświetlania sprzętu: " + ex.Message);
            }
        }
    }
}

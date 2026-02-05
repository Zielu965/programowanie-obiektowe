using System;
using Microsoft.Data.SqlClient;
using WypozyczalniaZimowa.Data;
using WypozyczalniaZimowa.Interfejsy;
using WypozyczalniaZimowa.Modele;

namespace WypozyczalniaZimowa.Repozytorium
{
    // Repozytorium klientów (CRUD + walidacja)
    internal class KlientRepository : ICrud<Klient>
    {
        public void Dodaj(Klient k)
        {
            if (k == null ||
                string.IsNullOrWhiteSpace(k.Id) ||
                string.IsNullOrWhiteSpace(k.Imie) ||
                string.IsNullOrWhiteSpace(k.Nazwisko) ||
                string.IsNullOrWhiteSpace(k.Telefon))
            {
                Console.WriteLine("Błąd: niepoprawne dane klienta.");
                return;
            }

            if (Znajdz(k.Id) != null)
            {
                Console.WriteLine("Błąd: klient o takim ID już istnieje.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))
                {
                    con.Open();
                    string sql = @"INSERT INTO Klient (IdKlienta, Imie, Nazwisko, Telefon)
                                   VALUES (@id, @imie, @nazwisko, @telefon)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", k.Id);
                        cmd.Parameters.AddWithValue("@imie", k.Imie);
                        cmd.Parameters.AddWithValue("@nazwisko", k.Nazwisko);
                        cmd.Parameters.AddWithValue("@telefon", k.Telefon);
                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Dodano klienta do bazy.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd dodawania klienta: " + ex.Message);
            }
        }

        public void Usun(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Błąd: podaj ID klienta.");
                return;
            }

            if (Znajdz(id) == null)
            {
                Console.WriteLine("Nie znaleziono klienta o podanym ID.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))
                {
                    con.Open();
                    string sql = "DELETE FROM Klient WHERE IdKlienta = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Usunięto klienta.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd usuwania klienta: " + ex.Message);
            }
        }

        public Klient Znajdz(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;

            try
            {
                using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))
                {
                    con.Open();
                    string sql = @"SELECT IdKlienta, Imie, Nazwisko, Telefon
                                   FROM Klient WHERE IdKlienta = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                return new Klient
                                {
                                    Id = r["IdKlienta"].ToString(),
                                    Imie = r["Imie"].ToString(),
                                    Nazwisko = r["Nazwisko"].ToString(),
                                    Telefon = r["Telefon"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch { return null; }

            return null;
        }

        public void WyswietlWszystkie()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))
                {
                    con.Open();
                    string sql = @"SELECT IdKlienta, Imie, Nazwisko, Telefon
                                   FROM Klient ORDER BY IdKlienta";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        Console.WriteLine("=== Lista klientów ===");
                        bool any = false;
                        while (r.Read())
                        {
                            any = true;
                            Klient k = new Klient
                            {
                                Id = r["IdKlienta"].ToString(),
                                Imie = r["Imie"].ToString(),
                                Nazwisko = r["Nazwisko"].ToString(),
                                Telefon = r["Telefon"].ToString()
                            };
                            Console.WriteLine(k);
                        }
                        if (!any) Console.WriteLine("Brak klientów.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd pobierania klientów: " + ex.Message);
            }
        }
    }
}

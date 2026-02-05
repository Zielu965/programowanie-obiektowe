using System;
using Microsoft.Data.SqlClient;
using WypozyczalniaZimowa.Data;
using WypozyczalniaZimowa.Modele;

namespace WypozyczalniaZimowa.Repozytorium
{
    // Repozytorium odpowiedzialne za wypożyczenia sprzętu
    internal class WypozyczenieRepository
    {
        private readonly SprzetRepository sprzetRepo = new SprzetRepository();
        private readonly KlientRepository klientRepo = new KlientRepository();

        // Tworzy nowe wypożyczenie
        public void Wypozycz(string idWyp, string idKlienta, string idSprzetu)
        {
            // Sprawdzenie danych wejściowych
            if (string.IsNullOrWhiteSpace(idWyp) ||
                string.IsNullOrWhiteSpace(idKlienta) ||
                string.IsNullOrWhiteSpace(idSprzetu))
            {
                Console.WriteLine("Błąd: niepoprawne dane wypożyczenia.");
                return;
            }

            // Sprawdzenie klienta
            Klient klient = klientRepo.Znajdz(idKlienta);
            if (klient == null)
            {
                Console.WriteLine("Błąd: klient nie istnieje.");
                return;
            }

            // Sprawdzenie sprzętu
            Sprzet sprzet = sprzetRepo.Znajdz(idSprzetu);
            if (sprzet == null)
            {
                Console.WriteLine("Błąd: sprzęt nie istnieje.");
                return;
            }

            if (!sprzet.Dostepny)
            {
                Console.WriteLine("Błąd: sprzęt jest już wypożyczony.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))
                {
                    con.Open();

                    string sql = @"INSERT INTO Wypozyczenie
                                   (IdWypozyczenia, IdKlienta, IdSprzetu, DataOd, Aktywne)
                                   VALUES (@id, @klient, @sprzet, GETDATE(), 1)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", idWyp);
                        cmd.Parameters.AddWithValue("@klient", idKlienta);
                        cmd.Parameters.AddWithValue("@sprzet", idSprzetu);
                        cmd.ExecuteNonQuery();
                    }

                    // Zmiana statusu sprzętu
                    string update = "UPDATE Sprzet SET Dostepny = 0 WHERE IdSprzetu = @id";
                    using (SqlCommand cmd2 = new SqlCommand(update, con))
                    {
                        cmd2.Parameters.AddWithValue("@id", idSprzetu);
                        cmd2.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Wypożyczenie zapisane.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd wypożyczenia: " + ex.Message);
            }
        }

        // Zwraca sprzęt
        public void Zwroc(string idWyp)
        {
            if (string.IsNullOrWhiteSpace(idWyp))
            {
                Console.WriteLine("Błąd: podaj ID wypożyczenia.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))
                {
                    con.Open();

                    // Pobranie sprzętu
                    string get = @"SELECT IdSprzetu FROM Wypozyczenie
                                   WHERE IdWypozyczenia = @id AND Aktywne = 1";

                    string idSprzetu = null;

                    using (SqlCommand cmd = new SqlCommand(get, con))
                    {
                        cmd.Parameters.AddWithValue("@id", idWyp);
                        var result = cmd.ExecuteScalar();
                        if (result == null)
                        {
                            Console.WriteLine("Nie znaleziono aktywnego wypożyczenia.");
                            return;
                        }
                        idSprzetu = result.ToString();
                    }

                    // Zakończenie wypożyczenia
                    string updWyp = @"UPDATE Wypozyczenie
                                      SET Aktywne = 0, DataDo = GETDATE()
                                      WHERE IdWypozyczenia = @id";

                    using (SqlCommand cmd = new SqlCommand(updWyp, con))
                    {
                        cmd.Parameters.AddWithValue("@id", idWyp);
                        cmd.ExecuteNonQuery();
                    }

                    // Ustawienie sprzętu jako dostępnego
                    string updSprzet = "UPDATE Sprzet SET Dostepny = 1 WHERE IdSprzetu = @id";
                    using (SqlCommand cmd2 = new SqlCommand(updSprzet, con))
                    {
                        cmd2.Parameters.AddWithValue("@id", idSprzetu);
                        cmd2.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Sprzęt został zwrócony.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd zwrotu: " + ex.Message);
            }
        }
    }
}

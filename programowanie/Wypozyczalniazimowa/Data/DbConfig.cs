namespace WypozyczalniaZimowa.Data
{
    // Klasa przechowująca konfigurację połączenia z bazą danych
    // Dzięki temu connection string jest w jednym miejscu
    internal static class DbConfig
    {
        // Connection string do SQL Server Express
        // Wskazuje na bazę danych: WypozyczalniaZimowa
        public static string ConnectionString =
            @"Server=.\SQLEXPRESS;Database=WypozyczalniaZimowa;Trusted_Connection=True;TrustServerCertificate=True;";

           
    }
}

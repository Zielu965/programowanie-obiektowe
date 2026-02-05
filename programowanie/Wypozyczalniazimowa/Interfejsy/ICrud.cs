using System;
using System.Collections.Generic;
using System.Text;

namespace WypozyczalniaZimowa.Interfejsy
{
    // Prosty interfejs CRUD - wymaganie na ocenę 4.0
    internal interface ICrud<T>
    {
        void Dodaj(T obiekt);
        void Usun(string id);
        T Znajdz(string id);
        void WyswietlWszystkie();
    }
}

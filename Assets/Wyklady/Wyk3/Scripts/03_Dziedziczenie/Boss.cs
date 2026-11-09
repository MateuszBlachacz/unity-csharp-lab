// Wyk3, slajd „protected, sealed i ukrywanie metod”
namespace Wyklady.Wyk3
{
    // sealed: po tej klasie nie można już dziedziczyć.
    public sealed class Boss : DescribedEnemy
    {
        public Boss()
        {
            hp = 500;   // pole protected z klasy bazowej
        }

        // override: wersję wybiera typ obiektu w czasie działania.
        public override string Describe()
        {
            return $"boss ({hp} HP)";
        }

        // new: ukrycie metody; wersję wybiera typ zmiennej w czasie kompilacji.
        public new string Kind()
        {
            return "boss";
        }
    }

    // class MegaBoss : Boss { }
    // Błąd CS0509: nie można dziedziczyć po klasie zapieczętowanej (sealed).
}

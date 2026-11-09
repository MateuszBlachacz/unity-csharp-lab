// Wyk3, slajd „Konstruktory”
namespace Wyklady.Wyk3
{
    // Nazwa PickupData, żeby nie kolidowała z abstrakcyjnym Pickup z części 5.
    public class PickupData
    {
        // Tylko do odczytu: wartość ustawia wyłącznie konstruktor.
        public string Id { get; }
        public int Value { get; set; }
        public float Lifetime { get; set; } = 10f;

        // Łańcuch konstruktorów: wersja z jednym parametrem woła pełną z wartością 1.
        public PickupData(string id) : this(id, 1)
        {
        }

        public PickupData(string id, int value)
        {
            Id = id;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Id}: wartość {Value}, czas życia {Lifetime} s";
        }
    }
}

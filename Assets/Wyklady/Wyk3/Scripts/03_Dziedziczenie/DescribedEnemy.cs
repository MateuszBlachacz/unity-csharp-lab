// Wyk3, slajd „protected, sealed i ukrywanie metod”
namespace Wyklady.Wyk3
{
    public class DescribedEnemy
    {
        protected int hp = 100;

        public virtual string Describe()
        {
            return $"przeciwnik ({hp} HP)";
        }

        // Metoda niewirtualna: klasa pochodna może ją tylko ukryć słowem new.
        public string Kind()
        {
            return "zwykły przeciwnik";
        }
    }
}

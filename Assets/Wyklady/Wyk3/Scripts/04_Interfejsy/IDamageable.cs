// Wyk3, slajd „Interfejs: kontrakt”
namespace Wyklady.Wyk3
{
    // Kontrakt: każdy, kto go implementuje, ma IsAlive i TakeDamage.
    // Pocisk nie musi wiedzieć, czy trafił przeciwnika, gracza czy skrzynię.
    public interface IDamageable
    {
        bool IsAlive { get; }
        void TakeDamage(int amount);
    }
}

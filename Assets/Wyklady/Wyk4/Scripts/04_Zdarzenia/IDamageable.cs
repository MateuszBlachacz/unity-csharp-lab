// Wyk4, slajd „Zdarzenie w C#: event” (zdrowie jako cel obrażeń) i „Zbiór: HashSet<T>”
namespace Wyklady.Wyk4
{
    public interface IDamageable
    {
        bool IsAlive { get; }
        void TakeDamage(int amount);
    }
}

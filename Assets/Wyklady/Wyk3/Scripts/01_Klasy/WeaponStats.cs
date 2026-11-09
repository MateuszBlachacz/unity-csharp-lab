// Wyk3, slajd „Własna klasa”
namespace Wyklady.Wyk3
{
    // Zwykła klasa C# (nie MonoBehaviour): obiekt tworzymy przez new.
    public class WeaponStats
    {
        private string name;
        private int damage;
        private float fireRate;

        public WeaponStats(string name, int damage, float fireRate)
        {
            // this. odróżnia pole od parametru o tej samej nazwie.
            this.name = name;
            this.damage = damage;
            this.fireRate = fireRate;
        }

        // Czas między strzałami w sekundach.
        public float Cooldown => 1f / fireRate;

        public int DamageFor(bool crit)
        {
            return crit ? damage * 2 : damage;
        }

        public override string ToString()
        {
            return $"{name} (obrażenia {damage}, {fireRate} strz./s)";
        }
    }
}

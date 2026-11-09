// Wyk3, slajd „Konstruktory w hierarchii: base”
using UnityEngine;

namespace Wyklady.Wyk3
{
    // Nazwa ItemWeapon, żeby nie kolidowała z komponentem Weapon z części 1.
    public class ItemWeapon : Item
    {
        public int Damage { get; }

        // : base(name) wywołuje konstruktor klasy bazowej przed treścią tego konstruktora.
        public ItemWeapon(string name, int damage) : base(name)
        {
            Damage = damage;
            Debug.Log($"[Wyk3] ItemWeapon({name}, {damage})");
        }

        public override string Describe()
        {
            return $"{base.Describe()}, obrażenia {Damage}";
        }
    }
}

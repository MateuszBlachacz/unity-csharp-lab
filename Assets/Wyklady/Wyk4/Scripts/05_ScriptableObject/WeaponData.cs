// Wyk4, slajd „ScriptableObject: dane jako asset”
using UnityEngine;

namespace Wyklady.Wyk4
{
    // Na slajdzie menuName = "Arena/Weapon"; tu inna ścieżka, żeby nie dublować pozycji menu z L10.
    [CreateAssetMenu(menuName = "Wyklady/Wyk4/Weapon")]
    public class WeaponData : ScriptableObject
    {
        public string displayName = "Pistolet";
        public int damage = 10;
        public float fireRate = 3f; // strzały na sekundę
        public Projectile projectile;
    }
}

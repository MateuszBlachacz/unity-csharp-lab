// Wyk3, slajd „Klasy serializowane”
using UnityEngine;

namespace Wyklady.Wyk3
{
    // [System.Serializable] sprawia, że Inspector pokazuje pola tej klasy
    // wewnątrz komponentu, który ją przechowuje (tu: Weapon).
    [System.Serializable]
    public class SerializedWeaponStats
    {
        public string name = "Pistolet";
        [Min(0)] public int damage = 10;
        [Range(0.1f, 10f)] public float fireRate = 2f;

        public override string ToString()
        {
            return $"{name} (obrażenia {damage}, {fireRate} strz./s)";
        }
    }
}

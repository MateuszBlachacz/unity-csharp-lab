// Wyk4, slajd „ScriptableObject: dane jako asset” (komponent korzystający z WeaponData)
using UnityEngine;

namespace Wyklady.Wyk4
{
    // ScriptableObject opisuje, jaki jest typ rzeczy; komponent przechowuje, co się z nią teraz dzieje.
    // WeaponData mówi, ile zadaje obrażeń i jak szybko strzela; Weapon pamięta, kiedy wolno strzelić znowu.
    // Uwaga: zmiana pól assetu w trybie Play zostaje po wyjściu z Play (to plik w projekcie, nie kopia w scenie).
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData data;
        [SerializeField] private Transform muzzle;

        private float nextFireTime;

        public WeaponData Data => data;

        // Uzupełnienie do slajdu: zmiana broni w trakcie gry.
        public void SetData(WeaponData newData)
        {
            data = newData;
            nextFireTime = 0f;
        }

        public void Fire()
        {
            if (data == null || Time.time < nextFireTime)
            {
                return;
            }

            // Uzupełnienie do slajdu: czas odnowienia z fireRate.
            nextFireTime = Time.time + 1f / data.fireRate;
            Projectile p = Instantiate(data.projectile, muzzle.position, muzzle.rotation);
            p.Damage = data.damage;
            p.Owner = gameObject;
        }
    }
}

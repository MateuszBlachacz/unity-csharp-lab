// Wyk3, slajdy „Klasy pochodne: Chaser i Turret”, „Wywołanie wersji bazowej: base”
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class Turret : Enemy
    {
        [SerializeField] private DemoProjectile projectilePrefab;
        [SerializeField] private float fireInterval = 1.5f;

        private Transform barrel;
        private float timer;

        protected override void Awake()
        {
            base.Awake();       // najpierw wspólna część: gracz i Rigidbody
            barrel = transform.Find("Barrel");
        }

        // AimAndShoot ze slajdu: obrót do gracza i strzał co fireInterval sekund.
        protected override void Act()
        {
            Vector3 toPlayer = player.transform.position - transform.position;
            toPlayer.y = 0f;
            if (toPlayer != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(toPlayer);
            }

            timer += Time.deltaTime;
            if (timer >= fireInterval)
            {
                timer = 0f;
                Attack();
            }
        }

        public override void Attack()
        {
            base.Attack();      // komunikat z klasy bazowej
            Transform muzzle = barrel != null ? barrel : transform;
            Vector3 spawn = muzzle.position + transform.forward * 0.8f;
            DemoProjectile shot = Instantiate(projectilePrefab, spawn, Quaternion.identity);
            shot.Launch(transform.forward, gameObject);
        }
    }
}

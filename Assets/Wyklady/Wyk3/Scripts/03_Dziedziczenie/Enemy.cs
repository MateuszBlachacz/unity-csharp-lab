// Wyk3, slajdy „Klasa bazowa Enemy”, „Metody wirtualne: virtual i override”, „Klasy abstrakcyjne”
using UnityEngine;

namespace Wyklady.Wyk3
{
    // Klasa abstrakcyjna: nie można jej dodać jako komponentu ani utworzyć,
    // ale każda klasa pochodna dostaje jej pola i metody.
    public abstract class Enemy : MonoBehaviour
    {
        // protected: widoczne w klasach pochodnych, niewidoczne dla innych skryptów.
        [SerializeField] protected float speed = 2f;
        [SerializeField] protected int scoreValue = 10;

        protected DemoPlayer player;
        protected Rigidbody body;

        private bool dead;

        protected virtual void Awake()
        {
            player = FindAnyObjectByType<DemoPlayer>();
            body = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!DemoGame.IsRunning || player == null)
            {
                return;
            }

            Act();
        }

        // Każdy przeciwnik musi określić swoje zachowanie.
        protected abstract void Act();

        public virtual void Attack()
        {
            Debug.Log($"[Wyk3] {name} atakuje");
        }

        public virtual void Die()
        {
            // Zabezpieczenie przed podwójną śmiercią (np. pocisk i KillAll w tej samej klatce).
            if (dead)
            {
                return;
            }

            dead = true;
            if (player != null)
            {
                player.AddScore(scoreValue);
            }

            Debug.Log($"[Wyk3] {name} ({GetType().Name}) pokonany, +{scoreValue} pkt");
            Destroy(gameObject);
        }

        // Uzupełnienie do slajdu: wspólny ruch w stronę gracza w płaszczyźnie XZ.
        protected void MoveTowardsPlayer(float moveSpeed)
        {
            Vector3 target = player.transform.position;
            target.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }
}

// Wyk3, slajd pokazowy „Tank: nadpisanie Die()”
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class Tank : Enemy
    {
        [SerializeField] private int debrisCount = 6;
        [SerializeField] private float debrisSpeed = 5f;

        // Uzupełnienie: czołg powoli jedzie w stronę gracza.
        protected override void Act()
        {
            MoveTowardsPlayer(speed);
        }

        public override void Die()
        {
            SpawnExplosion();   // najpierw własny efekt
            base.Die();         // potem wspólna logika: punkty i zniszczenie
        }

        // Uzupełnienie: „wybuch” z kilku małych kul rozlatujących się na boki.
        private void SpawnExplosion()
        {
            Material material = GetComponent<Renderer>().sharedMaterial;
            for (int i = 0; i < debrisCount; i++)
            {
                float angle = i * Mathf.PI * 2f / debrisCount;
                Vector3 dir = new Vector3(Mathf.Cos(angle), 0.3f, Mathf.Sin(angle));

                GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                piece.name = "W3_Debris";
                piece.transform.position = transform.position + dir * 0.5f;
                piece.transform.localScale = Vector3.one * 0.3f;
                piece.GetComponent<Renderer>().sharedMaterial = material;
                Destroy(piece.GetComponent<Collider>());

                Rigidbody rb = piece.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.linearVelocity = dir * debrisSpeed;
                Destroy(piece, 1f);
            }
        }
    }
}

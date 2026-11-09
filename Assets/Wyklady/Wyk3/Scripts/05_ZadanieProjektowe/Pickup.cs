// Wyk3, slajd „Zadanie projektowe: klasa bazowa Pickup”
using UnityEngine;

namespace Wyklady.Wyk3
{
    // Odpowiedź do karty projektowej:
    // - klasa bazowa: Pickup, bo wszystkie przedmioty wspólnie się obracają, wykrywają gracza
    //   i znikają po zebraniu (ta logika jest tu raz, a nie w każdej klasie);
    // - wirtualne: CanPickUp, bo większość przedmiotów zawsze da się zebrać (domyślnie true),
    //   a tylko niektóre dodają warunek (Medkit przy pełnym zdrowiu);
    // - abstrakcyjne: Apply, bo każdy przedmiot działa inaczej i nie ma sensownej wersji domyślnej;
    // - interfejs: tam, gdzie zachowanie mają klasy spoza tej hierarchii, np. IDamageable
    //   dla przeciwników, gracza i skrzyń, albo osobny interfejs dla wszystkiego, co da się zebrać
    //   lub kupić, gdyby takie obiekty nie dziedziczyły po Pickup.
    [RequireComponent(typeof(Collider))]
    public abstract class Pickup : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 90f;

        private void Update()
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out DemoPlayer p) || !CanPickUp(p))
            {
                return;
            }

            Apply(p);
            Destroy(gameObject);
        }

        protected virtual bool CanPickUp(DemoPlayer p)
        {
            return true;
        }

        protected abstract void Apply(DemoPlayer p);
    }
}

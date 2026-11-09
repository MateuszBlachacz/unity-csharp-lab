// Wyk3, slajd „Hermetyzacja w praktyce” (wersja bez hermetyzacji)
using UnityEngine;

namespace Wyklady.Wyk3
{
    // Pole publiczne: każdy skrypt może wpisać np. hp = -50 albo hp = 9999
    // i nikt nie sprawdzi, czy wartość ma sens ani czy obiekt powinien zginąć.
    public class HealthNoEncapsulation : MonoBehaviour
    {
        public int hp = 100;
    }
}

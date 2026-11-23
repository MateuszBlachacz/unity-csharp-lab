// Wyk4, pokazy z końca wykładu (lista klawiszy w HUD)
using TMPro;
using UnityEngine;

namespace Wyklady.Wyk4
{
    public class DemoControlsHud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;

        private void Start()
        {
            label.text =
                "WSAD – ruch, spacja – strzał\n" +
                "1 / 2 – pistolet / strzelba\n" +
                "E – wybuch (HashSet)\n" +
                "N – następna fala, T – statystyki\n" +
                "I – ekwipunek\n" +
                "X – pułapka foreach, C – poprawnie\n" +
                "H – 20 obrażeń dla gracza";
        }
    }
}

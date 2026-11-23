// Wyk4, slajd pokazowy „Rejestr przeciwników i ekwipunek” (licznik w HUD)
using TMPro;
using UnityEngine;

namespace Wyklady.Wyk4
{
    public class EnemyCountHud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;

        private int shown = -1;

        private void Update()
        {
            // Count z rejestru, bez szukania obiektów w scenie.
            int count = Enemy.All.Count;
            if (count != shown)
            {
                shown = count;
                label.text = $"Przeciwnicy: {count}";
            }
        }
    }
}

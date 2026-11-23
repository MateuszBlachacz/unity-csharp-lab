// Wyk4, slajd „ScriptableObject: dane jako asset” (wymiana danych broni w trakcie gry)
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wyklady.Wyk4
{
    [DefaultExecutionOrder(-10)]
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private WeaponData pistol;
        [SerializeField] private WeaponData shotgun;
        [SerializeField] private Weapon weapon;

        private void Start()
        {
            Debug.Log("=== Wyk4, część 5: ScriptableObject ===");
            Debug.Log($"Broń gracza: {weapon.Data.displayName} ({weapon.Data.damage} obr., {weapon.Data.fireRate} strz./s). Klawisze 1/2 zmieniają broń.");
        }

        private void Update()
        {
            Keyboard kb = Keyboard.current;
            if (kb == null)
            {
                return;
            }

            if (kb.digit1Key.wasPressedThisFrame)
            {
                Switch(pistol);
            }
            else if (kb.digit2Key.wasPressedThisFrame)
            {
                Switch(shotgun);
            }
        }

        private void Switch(WeaponData data)
        {
            weapon.SetData(data);
            Debug.Log($"Broń: {data.displayName} ({data.damage} obr., {data.fireRate} strz./s)");
        }
    }
}

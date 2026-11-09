// Wyk3, slajd „Klasy serializowane”
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class Weapon : MonoBehaviour
    {
        // Pojedynczy obiekt i tablica obiektów klasy serializowanej, oba widoczne w Inspectorze.
        [SerializeField] private SerializedWeaponStats stats = new SerializedWeaponStats();
        [SerializeField] private SerializedWeaponStats[] upgrades = new SerializedWeaponStats[0];

        private void Start()
        {
            Debug.Log($"[Wyk3] Weapon: {stats}, ulepszeń: {upgrades.Length}");
            for (int i = 0; i < upgrades.Length; i++)
            {
                Debug.Log($"[Wyk3]   ulepszenie {i + 1}: {upgrades[i]}");
            }
        }
    }
}

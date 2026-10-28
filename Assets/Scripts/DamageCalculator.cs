using UnityEngine;
using UnityEngine.InputSystem;

// Zadanie 7 z L2: obrażenia po naciśnięciu spacji.
public class DamageCalculator : MonoBehaviour
{
    private const int ComboBonus = 5;

    [SerializeField] private int baseDamage = 10;
    [SerializeField] private float weaponMultiplier = 1.5f;
    [SerializeField, Range(0f, 1f)] private float critChance = 0.2f;

    private int hitCount;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Hit();
        }
    }

    private void Hit()
    {
        hitCount++;

        float damage = baseDamage * weaponMultiplier;
        bool crit = Random.value < critChance;
        if (crit)
        {
            damage *= 2f;
        }
        if (hitCount % 3 == 0)
        {
            damage += ComboBonus;
        }

        // Przy baseDamage 7 wyniki to 10, 10, 16: Mathf.RoundToInt zaokrągla x,5 do liczby parzystej,
        // więc 10,5 daje 10, a 15,5 daje 16.
        int result = Mathf.RoundToInt(damage);
        string kind = crit ? "krytyczne" : "zwykłe";
        Debug.Log($"Uderzenie {hitCount}: {kind}, {result} obrażeń");
    }
}

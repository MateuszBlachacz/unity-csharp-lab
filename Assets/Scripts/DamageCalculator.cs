using UnityEngine;
using UnityEngine.InputSystem;

public class DamageCalculator : MonoBehaviour
{
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private float weaponMultiplier = 1.5f;
    [SerializeField] private float critChance = 0.2f;
    [SerializeField] private int everyThirdBonus = 5;

    private int hitCount;

    private void Update()
    {
        if (Keyboard.current == null || !Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            return;
        }

        hitCount++;

        // int * float daje float, zaokrąglamy dopiero na końcu.
        float damage = baseDamage * weaponMultiplier;

        bool crit = Random.value < critChance;
        if (crit)
        {
            damage *= 2f;
        }

        if (hitCount % 3 == 0)
        {
            damage += everyThirdBonus;
        }

        int finalDamage = Mathf.RoundToInt(damage);
        string critText = crit ? "tak" : "nie";
        Debug.Log($"Uderzenie {hitCount}: krytyczne: {critText}, obrażenia: {finalDamage}");
    }
}

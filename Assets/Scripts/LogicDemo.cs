using UnityEngine;
using UnityEngine.InputSystem;

// Zadanie 3 z L2: warunki gry jako wyrażenia typu bool. Raport po naciśnięciu K.
public class LogicDemo : MonoBehaviour
{
    [SerializeField] private int hp = 50;
    [SerializeField] private int ammo = 3;
    [SerializeField] private int enemiesNear = 1;
    [SerializeField] private int coins = 4;
    [SerializeField] private bool isReloading;
    [SerializeField] private bool hasKey;

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            Report();
        }
    }

    private void Report()
    {
        bool canShoot = ammo > 0 && !isReloading;
        bool inDanger = hp < 30 || enemiesNear > 3;
        bool canOpenDoor = hasKey || coins >= 10;
        string status = hp > 0 ? "żyje" : "pokonany";
        Debug.Log($"canShoot: {canShoot}, inDanger: {inDanger}, canOpenDoor: {canOpenDoor}, status: {status}");

        // Krótkie obliczanie: przy isReloading = false && nie wywołuje HasAmmo(), bo wynik i tak jest false.
        // & zawsze oblicza obie strony, dlatego na jedno naciśnięcie K pojawia się jeden komunikat
        // „sprawdzam amunicję” (z drugiego wyrażenia).
        bool lazy = isReloading && HasAmmo();
        bool eager = isReloading & HasAmmo();
        Debug.Log($"isReloading && HasAmmo(): {lazy}, isReloading & HasAmmo(): {eager}");

        // Pułapka = i == (linie usunięte):
        // if (hp = 0)          błąd kompilacji: hp = 0 ma typ int, a warunek musi mieć typ bool
        // if (hasKey = true)   kompiluje się z ostrzeżeniem, bo przypisanie ma typ bool;
        //                      warunek jest zawsze spełniony, a hasKey zostaje ustawione na true
    }

    private bool HasAmmo()
    {
        Debug.Log("sprawdzam amunicję");
        return ammo > 0;
    }
}

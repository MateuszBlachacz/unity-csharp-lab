// Zadanie 5: odpowiedzi (dokumentacja Unity 6, "Event function execution order").
//
// 1. Kolejność po wejściu w tryb Play: Awake, OnEnable, Start, Update.
//    Awake i OnEnable wykonują się zaraz po załadowaniu obiektu, a Start
//    tuż przed pierwszym wywołaniem Update tego skryptu, więc Start i
//    pierwszy Update wypisują ten sam numer klatki. Przy wyjściu z trybu
//    Play: OnDisable, potem OnDestroy.
//
// 2. Tylko raz w życiu komponentu wykonują się Awake, Start i OnDestroy.
//    OnEnable i OnDisable wywołują się przy każdym włączeniu i wyłączeniu
//    komponentu (checkbox w Inspectorze) lub obiektu.
//
// 3. Komponent wyłączony od początku (obiekt aktywny): Awake i tak się
//    wykona, ale OnEnable, Start i Update nie. Po włączeniu komponentu
//    wywoła się OnEnable, a Start tuż przed jego pierwszym Update.
//    Jeśli wyłączony jest cały obiekt, nie wykona się nawet Awake.
//
// 4. Awake wywołuje się zawsze przed Start, niezależnie od tego, czy
//    komponent jest włączony. Służy do przygotowania własnego stanu
//    i pobrania referencji do komponentów na tym samym obiekcie.
//    Start wywołuje się dopiero, gdy komponent jest włączony, i po Awake
//    wszystkich obiektów w scenie, więc nadaje się do kodu, który
//    korzysta z innych obiektów.

using UnityEngine;

public class LifecycleLogger : MonoBehaviour
{
    private bool firstFrameLogged;

    private void Awake()
    {
        Debug.Log($"[{Time.frameCount}] Awake");
    }

    private void OnEnable()
    {
        Debug.Log($"[{Time.frameCount}] OnEnable");
    }

    private void Start()
    {
        Debug.Log($"[{Time.frameCount}] Start");
    }

    private void Update()
    {
        // Update wywołuje się co klatkę, więc wypisujemy tylko pierwszą.
        if (firstFrameLogged)
        {
            return;
        }

        Debug.Log($"[{Time.frameCount}] Update");
        firstFrameLogged = true;
    }

    private void OnDisable()
    {
        Debug.Log($"[{Time.frameCount}] OnDisable");
    }

    private void OnDestroy()
    {
        Debug.Log($"[{Time.frameCount}] OnDestroy");
    }
}

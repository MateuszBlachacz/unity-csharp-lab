// Wykład 1, slajd: Debugowanie: konsola i Debug.Log
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

void Start()
{
    Debug.Log("Start gracza");                    // biały wpis
    Debug.LogWarning("Brak kamery, używam Main"); // żółty
    Debug.LogError("Nie przypisano prefabu!");    // czerwony

    Debug.Log($"Pozycja: {transform.position}, hp: {hp}");
    Debug.Log("Kliknij mnie", gameObject);         // podświetli obiekt
}

void OnDrawGizmos()
{
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, attackRange);
}

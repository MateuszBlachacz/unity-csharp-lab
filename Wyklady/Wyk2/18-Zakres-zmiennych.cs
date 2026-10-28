// Wykład 2, slajd: Zakres zmiennych
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

public class Spawner : MonoBehaviour
{
    int spawned;                    // pole: cały obiekt

    void SpawnWave(int count)       // parametr: cała metoda
    {
        for (int i = 0; i < count; i++)  // i: tylko pętla
        {
            Vector3 pos = RandomPoint();  // pos: tylko ciało pętli
            Instantiate(prefab, pos, Quaternion.identity);
            spawned++;
        }
        // Debug.Log(i);   // błąd CS0103: i nie istnieje
        // Debug.Log(pos); // błąd CS0103
    }
}

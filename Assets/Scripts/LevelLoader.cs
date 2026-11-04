using System.Collections.Generic;
using UnityEngine;

// Zadania 1–2 (L5): poziom wczytywany z pliku tekstowego, po jednym znaku na komórkę.
// '#' ściana, '.' puste pole, 'C' moneta, 'P' start gracza. Zastępuje ArenaBuilder i CoinSpawner z L4.
public class LevelLoader : MonoBehaviour
{
    private const float PlayerHeight = 1f;

    [SerializeField] private TextAsset levelFile;
    [SerializeField] private GameObject wallPrefab;
    // Kolejność dowolna; dla każdego 'C' losowany jest jeden prefab.
    [SerializeField] private GameObject[] coinPrefabs;
    [SerializeField] private Transform player;
    [SerializeField] private Transform wallsParent;
    [SerializeField] private Transform coinsParent;
    [SerializeField] private GameScore score;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private float wallHeight = 0.5f;
    [SerializeField] private float coinHeight = 0.8f;

    private void Start()
    {
        Build(levelFile);
    }

    // Zwraca false, gdy plik ma błędy; poziom nie jest wtedy budowany.
    public bool Build(TextAsset file)
    {
        if (file == null)
        {
            Debug.LogError("LevelLoader: nie przypisano pliku poziomu.");
            return false;
        }

        if (!TryReadLines(file.text, out string[] lines))
        {
            Debug.LogError($"Poziom '{file.name}' ma błędy, nie zostanie zbudowany.");
            return false;
        }

        ClearLevel();

        int rows = lines.Length;
        int cols = lines[0].Length;   // po walidacji wszystkie wiersze mają tę samą długość
        int coinCount = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                switch (lines[row][col])
                {
                    case '#':
                        Instantiate(wallPrefab, GridMath.CellToWorld(row, col, rows, cols, cellSize, wallHeight),
                                    Quaternion.identity, wallsParent);
                        break;
                    case 'C':
                        GameObject prefab = coinPrefabs[Random.Range(0, coinPrefabs.Length)];
                        Instantiate(prefab, GridMath.CellToWorld(row, col, rows, cols, cellSize, coinHeight),
                                    prefab.transform.rotation, coinsParent);
                        coinCount++;
                        break;
                    case 'P':
                        PlacePlayer(GridMath.CellToWorld(row, col, rows, cols, cellSize, PlayerHeight));
                        break;
                }
            }
        }

        score.SetCoinTotal(coinCount);
        Debug.Log($"Poziom '{file.name}': {rows} × {cols}, monet: {coinCount}");
        return true;
    }

    // Zadanie 2: dzieli tekst na wiersze, pomija puste i sprawdza poprawność.
    // Każda usterka ma osobny komunikat; numery wierszy i kolumn liczone od 1 jak w edytorze tekstu.
    private bool TryReadLines(string text, out string[] lines)
    {
        string[] rawLines = text.Split('\n');
        var kept = new List<string>();
        var lineNumbers = new List<int>();   // numer wiersza w pliku do komunikatów

        for (int i = 0; i < rawLines.Length; i++)
        {
            // Pliki z Windowsa kończą wiersze znakami \r\n, więc po podziale zostaje \r.
            string line = rawLines[i].TrimEnd('\r');
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            kept.Add(line);
            lineNumbers.Add(i + 1);
        }

        lines = kept.ToArray();
        bool ok = true;

        if (lines.Length == 0)
        {
            Debug.LogError("Plik poziomu jest pusty.");
            return false;
        }

        int width = lines[0].Length;
        int playerCount = 0;
        int coinCount = 0;

        for (int row = 0; row < lines.Length; row++)
        {
            string line = lines[row];
            if (line.Length != width)
            {
                Debug.LogError($"Wiersz {lineNumbers[row]} ma długość {line.Length}, a pierwszy wiersz {width}. Wszystkie wiersze muszą być równe.");
                ok = false;
            }

            for (int col = 0; col < line.Length; col++)
            {
                char c = line[col];
                switch (c)
                {
                    case '#':
                    case '.':
                        break;
                    case 'C':
                        coinCount++;
                        break;
                    case 'P':
                        playerCount++;
                        break;
                    default:
                        Debug.LogError($"Nieznany znak '{c}' w wierszu {lineNumbers[row]}, kolumnie {col + 1}.");
                        ok = false;
                        break;
                }
            }
        }

        if (playerCount == 0)
        {
            Debug.LogError("Brak pozycji startowej gracza: w pliku nie ma znaku 'P'.");
            ok = false;
        }
        else if (playerCount > 1)
        {
            Debug.LogError($"Znak 'P' występuje {playerCount} razy, a gracz może startować tylko z jednego miejsca.");
            ok = false;
        }

        if (coinCount == 0)
        {
            Debug.LogError("Na poziomie nie ma żadnej monety (znak 'C').");
            ok = false;
        }

        return ok;
    }

    private void PlacePlayer(Vector3 pos)
    {
        player.position = pos;
        // Gracz ma Rigidbody, więc ustawiamy też pozycję ciała i zerujemy prędkość.
        if (player.TryGetComponent(out Rigidbody rb))
        {
            rb.position = pos;
            rb.linearVelocity = Vector3.zero;
        }
    }

    // Usuwa ściany i monety poprzedniego poziomu (potrzebne przy ponownym budowaniu).
    private void ClearLevel()
    {
        foreach (Transform child in wallsParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in coinsParent)
        {
            Destroy(child.gameObject);
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;

// Zadanie 4 (L5): nazwa gracza wpisywana przed startem. Do czasu poprawnej nazwy gra stoi.
public class PlayerNameForm : MonoBehaviour
{
    private const int MinLength = 3;
    private const int MaxLength = 12;

    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private GameScore score;

    private void Awake()
    {
        // timeScale 0 zatrzymuje fizykę i czas gry; UI działa dalej.
        Time.timeScale = 0f;
        errorText.text = "";

        // Moduł UI bez przypisanego zestawu akcji dostaje akcje domyślne (mysz, klawiatura).
        InputSystemUIInputModule uiModule = FindFirstObjectByType<InputSystemUIInputModule>();
        if (uiModule != null && uiModule.actionsAsset == null)
        {
            uiModule.AssignDefaultActions();
        }
    }

    // Podpięta w Inspectorze przycisku Start: On Click ().
    public void OnStartClicked()
    {
        if (!TryNormalizeName(nameInput.text, out string playerName, out string error))
        {
            errorText.text = error;
            return;
        }

        score.SetPlayerName(playerName);
        Debug.Log($"Start gry, gracz: {playerName}");
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    // Czysta metoda: tylko sprawdza i poprawia tekst, nie zmienia stanu gry.
    public static bool TryNormalizeName(string raw, out string result, out string error)
    {
        result = "";
        string name = raw.Trim();   // spacje z początku i końca

        if (name.Length < MinLength)
        {
            error = $"Nazwa jest za krótka ({name.Length} zn.), potrzeba co najmniej {MinLength} znaków.";
            return false;
        }
        if (name.Length > MaxLength)
        {
            error = $"Nazwa jest za długa ({name.Length} zn.), dozwolone najwyżej {MaxLength} znaków.";
            return false;
        }

        // char.IsLetterOrDigit uznaje polskie litery za litery, więc są dozwolone.
        foreach (char c in name)
        {
            if (c == ' ')
            {
                error = "Nazwa nie może zawierać spacji w środku. Użyj podkreślnika _.";
                return false;
            }
            if (!char.IsLetterOrDigit(c) && c != '_')
            {
                error = $"Niedozwolony znak '{c}'. Dozwolone są tylko litery, cyfry i _.";
                return false;
            }
        }

        result = char.ToUpper(name[0]) + name.Substring(1);
        error = "";
        return true;
    }

    [ContextMenu("Test przykładów z karty")]
    private void TestExamples()
    {
        string[] examples = { "  ola_2 ", "ab", "jan kowalski", "zażółć!" };
        foreach (string example in examples)
        {
            bool ok = TryNormalizeName(example, out string result, out string error);
            Debug.Log(ok ? $"\"{example}\" → \"{result}\"" : $"\"{example}\" odrzucona: {error}");
        }
    }
}

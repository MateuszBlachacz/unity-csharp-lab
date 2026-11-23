# Wykład 4: typy generyczne, kolekcje, delegaty i zdarzenia

Przykłady z wykładu 4 kursu „Programowanie w C# w środowisku Unity” zebrane w jednej scenie pokazowej, w kolejności wykładu.

## Jak uruchomić

1. Otwórz scenę `Assets/Wyklady/Wyk4/Wyk4_Przyklady.unity`.
2. Wciśnij Play i otwórz okno Console. Każda część wykładu wypisuje swoje przykłady z nagłówkiem (`=== Wyk4, część 1: typy generyczne ===` … `część 6`), a każdy slajd ma podnagłówek `--- nazwa slajdu ---`.
3. Sprawdź oba pokazy z końca wykładu: rejestr przeciwników z ekwipunkiem oraz pasek zdrowia z Game Over i Restartem.

Na scenie:

- `DemoPlayer`: gracz (`Health` 100, `Weapon` z `W4_Pistol`, `PlayerDeath`, `Inventory`);
- `Demos`: dzieci `01_Generyki` … `06_Pokazy` z komponentami każdej części (kolejność wypisywania ustala `[DefaultExecutionOrder]`);
- `Enemies`: trzech przeciwników `W4_Enemy` (zdrowie 30, dwa collidery: ciało i dziecko `Head`);
- `Items`: trzy monety (`coin`, 10 pkt), klejnot (`gem`) i klucz (`key`);
- `PressurePlate` i `Door`: płyta naciskowa z `UnityEvent` podpiętymi w Inspectorze (otwarcie drzwi, 50 punktów);
- `Canvas`: pasek zdrowia, wynik, licznik przeciwników, przyciski `Poziom 1–3`, lista klawiszy i ukryty panel „Koniec gry” z przyciskiem Restart.

## Sterowanie

| Klawisz   | Akcja                                                                  |
|-----------|------------------------------------------------------------------------|
| W/A/S/D   | ruch gracza                                                            |
| spacja    | strzał w kierunku patrzenia (przytrzymanie – ogień w tempie broni)     |
| `1` / `2` | pistolet / strzelba (`WeaponData` jako ScriptableObject)               |
| `E`       | wybuch wokół gracza: każdy cel trafiony raz (`HashSet<IDamageable>`)    |
| `N`       | następna fala przeciwników z kolejki `3, 5, 8`                          |
| `I`       | zawartość ekwipunku (`Dictionary<string, int>`)                        |
| `T`       | statystyki zabójstw po typie przeciwnika                               |
| `X`       | pułapka: wyłączanie przeciwników bliżej niż 4 w `foreach` (wyjątek)     |
| `C`       | to samo poprawnie: pętla `for` od końca                                |
| `H`       | 20 obrażeń dla gracza (szybki pokaz paska i Game Over)                 |

Przyciski `Poziom 1–3` w lewym górnym rogu tylko wypisują wybór w konsoli. Pole `showClosureBug` komponentu `LevelButtons` (obiekt `Demos/03_Delegaty`) włącza wersję z pułapką domknięcia: wtedy każdy przycisk przekazuje `i == 3`.

## Slajd wykładu → plik i klasa

| Slajd                                          | Plik (w `Scripts/`)                                                  | Klasa                                       |
|------------------------------------------------|----------------------------------------------------------------------|---------------------------------------------|
| **Część 1: typy generyczne**                   |                                                                      |                                             |
| Rozgrzewka: dziedziczenie                      | `01_Generyki/WarmupQuiz.cs`                                          | `WarmupQuiz`                                |
| Po co generyki?                                | `01_Generyki/GenericsDemo.cs`                                        | `GenericsDemo`                              |
| Metody generyczne                              | `01_Generyki/GenericUtils.cs`                                        | `GenericUtils`                              |
| Klasy generyczne                               | `01_Generyki/Registry.cs`                                            | `Registry<T>`                               |
| Ograniczenia parametru typu                    | `01_Generyki/ConstraintExamples.cs`                                  | `ConstraintExamples`                        |
| Generyki w API Unity                           | `01_Generyki/GenericsDemo.cs`                                        | `GenericsDemo`                              |
| **Część 2: kolekcje**                          |                                                                      |                                             |
| Lista: List\<T\>, usuwanie podczas przechodzenia | `02_Kolekcje/CollectionsDemo.cs`                                   | `CollectionsDemo`                           |
| Słownik, kolejka i stos                        | `02_Kolekcje/CollectionsDemo.cs`                                     | `CollectionsDemo`                           |
| Zbiór: HashSet\<T\>                            | `02_Kolekcje/CollectionsDemo.cs`, `Explosion.cs`                     | `CollectionsDemo`, `Explosion`              |
| Słownik w praktyce, sortowanie list, LINQ      | `02_Kolekcje/CollectionsDemo.cs`, `ScoreEntry.cs`, `EnemyInfo.cs`, `Boss.cs` | `CollectionsDemo`, `ScoreEntry`, `EnemyInfo`, `Boss` |
| Kolekcje w Inspectorze i inicjalizacja         | `02_Kolekcje/WaveConfig.cs`                                          | `WaveConfig`                                |
| Sprawdź się: kolekcje                          | `02_Kolekcje/CollectionsDemo.cs`                                     | `CollectionsDemo`                           |
| **Część 3: delegaty i lambdy**                 |                                                                      |                                             |
| Delegat: typ dla metod                         | `03_Delegaty/DamageModifier.cs`, `DelegatesDemo.cs`                  | `DamageModifier`, `DelegatesDemo`           |
| Gotowe delegaty: Action i Func                 | `03_Delegaty/DelegatesDemo.cs`                                       | `DelegatesDemo`                             |
| Wyrażenia lambda i domknięcia                  | `03_Delegaty/DelegatesDemo.cs`, `LevelButtons.cs`                    | `DelegatesDemo`, `LevelButtons`             |
| Delegat z wieloma metodami, Sprawdź się        | `03_Delegaty/DelegatesDemo.cs`                                       | `DelegatesDemo`                             |
| **Część 4: zdarzenia**                         |                                                                      |                                             |
| Zdarzenie w C#: event                          | `04_Zdarzenia/Health.cs`, `IDamageable.cs`                           | `Health`, `IDamageable`                     |
| Subskrypcja: OnEnable i OnDisable              | `04_Zdarzenia/HealthBar.cs`, `GameOverScreen.cs`                     | `HealthBar`, `GameOverScreen`               |
| UnityEvent                                     | `04_Zdarzenia/PressurePlate.cs`, `Door.cs`                           | `PressurePlate`, `Door`                     |
| Globalne zdarzenia gry                         | `04_Zdarzenia/GameEvents.cs`, `ScoreCounter.cs`, `ScoreHud.cs`       | `GameEvents`, `ScoreCounter`, `ScoreHud`    |
| Przykład: osiągnięcia na zdarzeniach           | `04_Zdarzenia/Achievements.cs`                                       | `Achievements`                              |
| Zdarzenia: najczęstsze błędy                   | `04_Zdarzenia/EventMistakesDemo.cs`                                  | `EventMistakesDemo`                         |
| **Część 5: ScriptableObject**                  |                                                                      |                                             |
| ScriptableObject: dane jako asset              | `05_ScriptableObject/WeaponData.cs`, `Weapon.cs`, `Projectile.cs`, `WeaponSwitcher.cs` | `WeaponData`, `Weapon`, `Projectile`, `WeaponSwitcher` |
| **Część 6: pokazy**                            |                                                                      |                                             |
| Pokaz: rejestr przeciwników i ekwipunek        | `06_Pokazy/Enemy.cs`, `EnemySpawner.cs`, `EnemyCountHud.cs`, `Inventory.cs`, `CollectibleItem.cs`, `KillStats.cs`, `RegistryTrapDemo.cs` | `Enemy`, `EnemySpawner`, … |
| Pokaz: pasek zdrowia i Game Over               | `06_Pokazy/DemoPlayer.cs`, `PlayerDeath.cs`, `DemoControlsHud.cs`, `04_Zdarzenia/GameOverPanel.cs` | `DemoPlayer`, `PlayerDeath`, `GameOverPanel`, … |

## Która kolekcja?

| Potrzebuję…                                         | Kolekcja                   |
|-----------------------------------------------------|----------------------------|
| listy o zmiennej długości, dostępu po indeksie      | `List<T>`                  |
| wartości po kluczu (nazwa → liczba, typ → licznik)  | `Dictionary<TKey, TValue>` |
| kolejności „pierwszy przyszedł, pierwszy wyszedł”   | `Queue<T>` (fale, kolejka zadań) |
| kolejności „ostatni włożony, pierwszy wyjęty”       | `Stack<T>` (ekrany menu, cofanie) |
| unikalnych elementów i szybkiego „czy już jest?”    | `HashSet<T>`               |
| stałej liczby elementów                             | tablica `T[]`              |

## event C# czy UnityEvent?

| | `event` C# | `UnityEvent` |
|---|---|---|
| Kto podpina słuchaczy | programista w kodzie (`+=` / `-=`) | projektant w Inspectorze (albo kod: `AddListener`) |
| Widoczność w Inspectorze | nie | tak |
| Kto może wywołać | tylko klasa, która je deklaruje | każdy, kto ma referencję |
| Wydajność | szybsze | wolniejsze (refleksja przy podpinaniu) |
| Typowe użycie | systemy gry (zdrowie, wynik, Game Over) | proste reakcje w scenie (płyta → drzwi, przycisk → akcja) |

## Zadanie projektowe „Zdarzenia w Arenie” – proponowane rozwiązanie

| Sytuacja | Kto ogłasza | Zdarzenie i parametry | Kto słucha |
|---|---|---|---|
| 1. Zabicie przeciwnika | `Enemy` (gdy jego `Health.Died`) | `GameEvents.EnemyKilled` (`Action<Enemy>`) | `ScoreCounter` (punkty), `KillStats` (statystyki), `Achievements` („Łowca”) |
| 2. Zebranie monety | `CollectibleItem` z `itemId = "coin"` | `GameEvents.CoinCollected` (`Action<int>`, wartość monety) | `ScoreCounter`, który ogłasza `GameEvents.ScoreChanged` (`Action<int>`) dla `ScoreHud` |
| 3. Śmierć gracza | `Health` gracza (`Died`, zdarzenie lokalne) → `PlayerDeath` | `GameEvents.GameOver` (`Action`) | `EnemySpawner` (koniec fal), `Achievements` („Pacyfista”); panel pokazuje `GameOverScreen` z `Health.Died` |

HUD nie słucha monet ani zabójstw, tylko `ScoreChanged`: nie musi wiedzieć, skąd biorą się punkty. Śmierć gracza to zdarzenie lokalne (`Health.Died` jednego obiektu), które `PlayerDeath` zamienia na globalne `GameOver`, żeby inne systemy nie musiały znać gracza.

## Niezależność od gry

- Wszystkie skrypty są w przestrzeni nazw `Wyklady.Wyk4`, więc klasy `Health`, `Enemy`, `Weapon`, `Projectile` czy `WeaponData` nie kolidują z klasami z laboratoriów ani z folderem `Wyk3`.
- Scena `Wyk4_Przyklady` nie jest w Build Settings. Restart w edytorze przeładowuje ją przez `EditorSceneManager.LoadSceneInPlayMode`; wersja ze slajdu (`SceneManager.LoadScene(buildIndex)`) działa w zbudowanej grze, gdy scena jest w Build Profiles.
- `CreateAssetMenu` ma ścieżkę `Wyklady/Wyk4/Weapon`, a nie `Arena/Weapon`.
- Statyczne zdarzenia `GameEvents` i lista `Enemy.All` są czyszczone przy starcie gry (`RuntimeInitializeOnLoadMethod`), więc działają także przy wyłączonym przeładowaniu domeny.
- `FindObjectsByType<Enemy>(FindObjectsSortMode.None)` ze slajdu jest w Unity 6000.6 przestarzałe (ostrzeżenie CS0618), dlatego w kodzie jest przeciążenie bez parametru `FindObjectsSortMode`.

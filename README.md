# Arena

Projekt `Arena` rozwijany na kolejnych laboratoriach kursu „Programowanie w C# w środowisku Unity” (UAFM).

## Wymagania

- Unity 6000.6.5f1 (Unity 6)
- Universal Render Pipeline (projekt z szablonu Universal 3D)

## Jak otworzyć

1. Sklonuj repozytorium i przełącz się na gałąź wybranego laboratorium.
2. W Unity Hub wybierz Add → Add project from disk i wskaż folder repozytorium.
3. Otwórz scenę `Assets/Scenes/Arena.unity`.

## Gałęzie

Każde laboratorium ma osobną gałąź nazwaną od jego numeru:

| Gałąź    | Zawartość                                       |
|----------|-------------------------------------------------|
| `main`   | punkt startowy: pusty projekt Universal 3D       |
| `lab-01` | Laboratorium 1: scena areny i pierwsze skrypty   |
| `lab-02` | Laboratorium 2: typy, operatory i ruch gracza    |

Kolejne laboratoria trafiają na gałęzie `lab-02`, `lab-03` itd., odgałęzione od poprzedniego laboratorium.

```
git checkout lab-01
```

## Laboratorium 1

Scena `Assets/Scenes/Arena.unity`:

- `Floor`: podłoga 18 × 10 (Plane w skali 1,8 × 1 × 1), materiał `M_Floor`;
- `Player`: kapsuła stojąca na podłodze, materiał `M_Player`, skrypt `HelloArena`;
- `Coins`: pusty obiekt grupujący monety `Coin1`, `Coin2`, `Coin3` (spłaszczone cylindry, materiał `M_Coin`, skrypt `Spinner` z prędkościami 90, 150 i −60°/s; `Coin1` ma też `LifecycleLogger`);
- `Main Camera`: widok ukośnie z góry na całą arenę;
- `Directional Light`: światło kierunkowe z cieniami;
- `Global Volume`: postprocessing z szablonu;
- `Debug` i `CoinRing`: obiekty z zadań dodatkowych (opis niżej).

Skrypty w `Assets/Scripts`:

- `HelloArena`: cztery pola edytowalne w Inspectorze (imię, życia, prędkość, tryb łatwy) i komunikat z ich wartościami w `Start`;
- `LifecycleLogger`: wypisuje wywołania `Awake`, `OnEnable`, `Start`, `Update` (tylko pierwsza klatka), `OnDisable` i `OnDestroy` z numerem klatki; na górze pliku odpowiedzi na pytania z zadania 5;
- `Spinner`: obraca obiekt wokół pionowej osi świata i pulsuje jego skalą, niezależnie od liczby klatek dzięki `Time.deltaTime`.

## Zadania dodatkowe

- `FpsCounter` (zadanie 8): podpięty do pustego obiektu `Debug`; raz na sekundę wypisuje średni FPS z ostatniej sekundy i najdłuższy czas klatki w milisekundach.
- `CoinRing` (zadanie 10): obiekt `CoinRing` w środku areny; w `Start` tworzy z prefabu `Assets/Prefabs/Coin.prefab` monety rozłożone równo na okręgu (domyślnie 8 monet, promień 4) jako swoje dzieci. Prefab ma `Spinner`, ale nie ma `LifecycleLogger`, żeby nie zaśmiecać konsoli.
- `Docs/L01-typowe-bledy.md` (zadanie 11): objawy i naprawa czterech częstych błędów.
- Zadanie 9 (debuger) wykonuje się w edytorze kodu i nie zostawia plików w repozytorium.

## Laboratorium 2

Scena `Assets/Scenes/Arena.unity`:

- `Player` dostał komponenty `PlayerMovement` i `DamageCalculator`;
- `Demos`: nowy pusty obiekt ze skryptami pokazowymi `TypesDemo`, `LogicDemo` i `FrameRateLimiter`. Obiekt jest **wyłączony**, żeby nie zaśmiecał konsoli i nie zmieniał liczby klatek; włącz go w Inspectorze, gdy chcesz powtórzyć zadania 1–4.

Skrypty w `Assets/Scripts`:

- `TypesDemo` (zadania 1–2): wypisuje wyniki 24 wyrażeń z tabel A i B (dzielenie całkowite, rzutowanie, zaokrąglanie do parzystej, `i++`/`++i`, łączenie tekstu, przepełnienie, porównania `float` i `double`) z przewidywaniem i wyjaśnieniem w komentarzu przy każdym; `ShowTypes()` pokazuje dobór typów, `const` i `readonly`; w komentarzach opis błędów kompilacji przy zmianie stałej, pola `readonly` i zmiennej `var`;
- `LogicDemo` (zadanie 3): po naciśnięciu K wypisuje warunki `canShoot`, `inDanger`, `canOpenDoor` i `status`; porównuje krótkie obliczanie `&&` z operatorem `&` i opisuje pułapkę `=` zamiast `==`;
- `FrameRateLimiter` (zadanie 4): wyłącza synchronizację pionową i ustawia docelową liczbę klatek (pole `targetFps`) do testu niezależności ruchu od FPS;
- `PlayerMovement` (zadania 4–6): ruch w płaszczyźnie XZ przez akcję `Move` z Input System, mnożony przez `Time.deltaTime`; ograniczenie pozycji do areny (`arenaHalfSize` 8,5 × 4,5); sprint pod lewym Shiftem (×1,8) z wytrzymałością 0–100 (−30/s przy sprincie, +15/s bez), po wyczerpaniu sprint wraca od 25; podgląd `measuredSpeed`, `stamina` i `exhausted` w Inspectorze;
- `DamageCalculator` (zadanie 7): po naciśnięciu spacji liczy obrażenia: bazowe × mnożnik broni, krytyk (20%) podwaja, co trzecie uderzenie +5, wynik przez `Mathf.RoundToInt`.

Sterowanie: WASD lub strzałki – ruch, lewy Shift – sprint, spacja – uderzenie, K – raport `LogicDemo` (gdy `Demos` jest włączony).

### Zadania dodatkowe

- Ruch po przekątnej (zadanie 9): wejście skracane przez `Vector2.ClampMagnitude(input, 1f)`. Kompozyt WASD akcji `Move` działa w domyślnym trybie *Digital Normalized*, więc klawiatura daje po skosie długość 1 już bez poprawki; poprawka chroni przed trybem *Digital* i nie spowalnia lekkiego wychylenia drążka.
- Granice w edytorze (zadanie 10): `OnDrawGizmosSelected` w `PlayerMovement` rysuje żółtą ramkę dozwolonego obszaru po zaznaczeniu gracza.
- Rozpędzanie i hamowanie (zadanie 11): pole `acceleration` (20 j/s²); bieżąca prędkość zbliża się do docelowej przez `Vector3.MoveTowards`, a przy krawędzi areny zeruje się jej składowa w tej osi.
- Przepełnienie i precyzja (zadanie 12): `OverflowDemo()` w `TypesDemo` porównuje `decimal`, `double` z tolerancją i `Mathf.Approximately`, a na końcu celowo przepełnia `int` w bloku `checked` (wyjątek `OverflowException` w konsoli).

## Struktura folderów

```
Assets/
  Materials/   materiały M_Floor, M_Player, M_Coin
  Prefabs/     prefab monety
  Scenes/      scena Arena
  Scripts/     skrypty C#
  Settings/    ustawienia URP z szablonu
Docs/          notatki do laboratoriów
Packages/      lista pakietów
ProjectSettings/
```

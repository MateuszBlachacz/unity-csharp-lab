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

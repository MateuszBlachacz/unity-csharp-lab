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
| `lab-03` | Laboratorium 3: zbieranie monet i reguły gry      |
| `lab-04` | Laboratorium 4: tablice, arena z tablicy 2D       |
| `lab-05` | Laboratorium 5: poziom z pliku tekstowego i HUD   |
| `lab-06` | Laboratorium 6: zdrowie, broń i pociski           |
| `lab-07` | Laboratorium 7: gotowi przeciwnicy z animacjami   |

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

## Laboratorium 3

Scena `Assets/Scenes/Arena.unity`:

- `Player`: tag `Player`, `Rigidbody` z *Is Kinematic* (potrzebny do zdarzeń triggerów) i skrypt `PlayerCollector`;
- `GameManager`: nowy obiekt ze skryptami `GameScore` i `CoinSpawner`; utworzone monety stają się jego dziećmi;
- monety `Coins` z L1 i obiekt `CoinRing` zostały usunięte ze sceny (skrypty `CoinRing` i `LifecycleLogger` zostają w projekcie).

Prefab `Assets/Prefabs/Coin.prefab`: tag `Coin`, collider z *Is Trigger*, skrypty `Spinner` i `Coin`.

Skrypty w `Assets/Scripts`:

- `CoinType`: typ wyliczeniowy `Bronze`, `Silver`, `Gold`;
- `Coin` (zadanie 2): pole typu `CoinType`; `GetValue()` zwraca 1, 3 lub 10 punktów (wyrażenie `switch`), a `Start` ustawia kolor monety (instrukcja `switch`);
- `PlayerCollector` (zadania 1 i 3): w `OnTriggerEnter` rozpoznaje monetę po tagu, dodaje jej wartość przez `GameScore.AddPoints(int, string)` i niszczy ją;
- `GameScore` (zadania 3 i 5): wynik z metodami `AddPoints(int)`, przeciążonym `AddPoints(int, string)` i `GetScore()`; ujemne wartości są pomijane z ostrzeżeniem. Tu liczone są też zebrane monety: po zebraniu wszystkich utworzonych pojawia się jeden komunikat o wygranej z czasem gry z dokładnością do 0,1 s;
- `CoinSpawner` (zadanie 4): na starcie tworzy 10 monet w losowych miejscach areny, nie bliżej niż 2 jednostki od gracza; na monetę przypada najwyżej 20 prób, potem jest pomijana z ostrzeżeniem; typ każdej monety jest losowy; liczbę faktycznie utworzonych monet przekazuje do `GameScore`.

### Zadania dodatkowe

- Losowanie z wagami (zadanie 6): `CoinSpawner.RollCoinType()` losuje brązowe w 60%, srebrne w 30% i złote w 10% przypadków. Test 1000 losowań uruchomisz z menu kontekstowego komponentu `CoinSpawner` (trzy kropki w Inspectorze → *Test losowania 1000 typów*).
- Seria monet (zadanie 7): `PlayerCollector` wywołuje `GameScore.AddCoinPoints`. Moneta zebrana w ciągu `comboWindow` (1,5 s) od poprzedniej podnosi mnożnik serii o 1, najwyżej do x3; po dłuższej przerwie mnożnik wraca do x1. Nowy mnożnik liczy czysta metoda `NextMultiplier`.
- Ranga za czas (zadanie 8): po wygranej `GetRank` (wyrażenie `switch` z wzorcami relacyjnymi) przyznaje rangę S < 20 s, A < 35 s, B < 60 s albo C, a `GetRankBonus` (instrukcja `switch`) dodaje premię 50/25/10/0 punktów. Komunikat o wygranej podaje czas, rangę i wynik końcowy.

## Laboratorium 4

Scena `Assets/Scenes/Arena.unity`:

- `Player`: `Rigidbody` już nie kinematyczny, z interpolacją i *Freeze Rotation* X, Y, Z (kapsuła się nie przewraca); nowy skrypt `CoinFinder` i dziecko `Arrow` (spłaszczony sześcian z materiałem `M_Arrow`, bez collidera);
- `Level`: nowy obiekt ze skryptem `ArenaBuilder`; jego dziecko `Walls` zbiera wszystkie ściany tworzone na starcie gry;
- `CoinSpawnPoints`: 8 pustych obiektów `SpawnPoint_1…8` na wolnych polach mapy (wysokość 0,8);
- `GameManager`: `CoinSpawner` korzysta z punktów spawnu i trzech prefabów monet, domyślnie tworzy 5 monet.

Prefaby i materiały:

- `Assets/Prefabs/Wall.prefab`: sześcian 1 × 1 × 1 z `BoxCollider`, materiał `M_Wall` (jaśniejszy szary niż podłoga), tag `Wall`;
- `Coin_Bronze`, `Coin_Silver`, `Coin_Gold`: warianty prefabu `Coin` z ustawionym typem.

Skrypty w `Assets/Scripts`:

- `PlayerMovement` (zadanie 1): wejście i sprint z wytrzymałością liczone w `Update`, prędkość ustawiana w `FixedUpdate` przez `rb.linearVelocity` z zachowaniem składowej `y` (grawitacja). Ograniczanie pozycji z L2, pole `arenaHalfSize` i ramka `OnDrawGizmosSelected` zostały usunięte, bo gracza zatrzymują ściany. Z zadań ★ L2 zostały `ClampMagnitude` na wejściu i rozpędzanie (`Vector3.MoveTowards` na poziomej prędkości ciała). `measuredSpeed` to długość poziomej składowej `rb.linearVelocity`;
- `ArenaBuilder` (zadanie 2): mapa 7 × 12 zapisana w polu `private readonly int[,] map` (1 – ściana, 0 – puste pole); w `Start` tworzy ściany z prefabu jako dzieci `Walls`, na wysokości 0,5, z rozmiarem komórki `cellSize` (1). Arena jest wyśrodkowana na (0, 0, 0), a pierwszy wiersz tablicy to najdalszy rząd (największe Z);
- `CoinSpawner` (zadanie 3): tablice `Transform[] spawnPoints` i `GameObject[] coinPrefabs`; tasuje indeksy punktów algorytmem Fishera–Yatesa i bierze pierwsze `count`, więc w jednym punkcie nie powstaną dwie monety. Gdy `count` jest większe od liczby punktów, wypisuje ostrzeżenie i tworzy tyle monet, ile jest punktów. Przy trzech prefabach (kolejność jak w `CoinType`) zostaje losowanie z wagami 60/30/10 z L3;
- `CoinFinder` (zadanie 4): w każdej klatce pobiera monety przez `FindGameObjectsWithTag("Coin")`, szuka najbliższej, rysuje do niej żółtą linię `Debug.DrawLine` (widok Scene albo Game z włączonymi *Gizmos*) i obraca strzałkę w poziomie w jej stronę, przesuwając ją wokół gracza. Gdy monet nie ma, strzałka się chowa;
- `Coin`: właściwość tylko do odczytu `Type`;
- `GameScore` (zadanie 5): tablica `int[] countByType` o długości równej liczbie typów monet, liczona w `CoinCollected(Coin)`; klawisz `Tab` wypisuje liczbę monet każdego typu, łączną liczbę i średnią wartość zebranej monety (przy zerze monet „brak”);
- `PlayerCollector`: przekazuje zebraną monetę do `GameScore.CoinCollected`.

Sterowanie: doszedł klawisz `Tab` – statystyki zebranych monet w konsoli.

### Zadania dodatkowe

- Drugi poziom w tablicy postrzępionej (zadanie 6): `ArenaBuilder` ma drugi poziom zapisany jako `int[][] level2` w kształcie litery L (dwa górne wiersze mają 8 komórek, pozostałe 12). Pole `level` (typ `LevelChoice`: `Level1`, `Level2`) w Inspectorze obiektu `Level` wybiera poziom. Liczbę wierszy daje `level2.Length`, a długość wiersza `level2[row].Length`; arena jest wyśrodkowana według najdłuższego wiersza. Gracz startuje w (0, 1, 0), czyli na wolnym polu obu poziomów.
- Punkty spawnu monet: zamiast osobnego zestawu punktów dla każdego poziomu `CoinSpawner` dostał referencję do `ArenaBuilder` i pomija punkty, które według tablicy wybranego poziomu leżą w ścianie albo poza mapą (`ArenaBuilder.IsFreeCell`), z komunikatem w konsoli. W poziomie 2 odpada `SpawnPoint_3` (prawy górny róg poza literą L), zostaje 7 punktów na 5 monet. Sprawdzanie korzysta tylko z tablicy, więc nie zależy od tego, czy ściany są już zbudowane.

## Laboratorium 5

Przy pierwszym otwarciu sceny Unity zapyta o *TMP Essentials*: kliknij *Import TMP Essentials* (folder `Assets/TextMesh Pro` nie jest w repozytorium, a teksty interfejsu korzystają z domyślnej czcionki TextMeshPro).

Scena `Assets/Scenes/Arena.unity`:

- `Level`: nowy skrypt `LevelLoader`; komponent `ArenaBuilder` jest **wyłączony** (zostaje w projekcie); nowe dziecko `Coins` zbiera monety z pliku poziomu;
- `GameManager`: komponent `CoinSpawner` jest **wyłączony**, monety tworzy teraz `LevelLoader`;
- `Canvas` (Screen Space – Overlay, *Canvas Scaler* „Scale With Screen Size” 1920 × 1080) ze skryptem `Hud`:
  - `HudText`: wynik i czas, zakotwiczony w lewym górnym rogu;
  - `WinText`: gratulacje na środku ekranu, ukryty do wygranej;
  - `StartPanel`: pole `NameInput` (TMP Input Field), przycisk `StartButton` (On Click → `PlayerNameForm.OnStartClicked`) i `ErrorText` na komunikaty o błędach;
- `EventSystem` z `InputSystemUIInputModule` (obsługa myszy i klawiatury w UI przez Input System).

Plik `Assets/Levels/level1.txt` (od zadania 6 ★ w `Assets/Resources/Levels/`): mapa 7 × 12, po jednym znaku na komórkę (`#` ściana, `.` puste pole, `C` moneta, `P` start gracza).

Skrypty w `Assets/Scripts`:

- `GridMath`: statyczna metoda `CellToWorld` przelicza (wiersz, kolumna) na pozycję w świecie; korzystają z niej `ArenaBuilder` i `LevelLoader`;
- `LevelLoader` (zadania 1–2): dzieli `levelFile.text` na wiersze (`Split('\n')`, potem `TrimEnd('\r')` dla plików z Windowsa), pomija puste wiersze i sprawdza plik. Osobne komunikaty dostają: wiersz o innej długości niż pierwszy, brak `P`, więcej niż jedno `P`, brak monet i nieznany znak (z numerem wiersza i kolumny w pliku, od 1). Przy błędach poziom się nie buduje. Poprawny plik daje ściany, monety losowego typu i gracza w miejscu `P` (Y = 1); liczba monet do wygranej trafia do `GameScore`;
- `Hud` (zadanie 3): co klatkę pisze `Wynik: {wynik:D4}   Czas: {czas:F1} s` (format `F1` bierze separator z ustawień regionalnych, w polskim systemie przecinek); po wygranej zatrzymuje czas i pokazuje `Gratulacje, <nazwa>!`;
- `PlayerNameForm` (zadanie 4): do czasu poprawnej nazwy gra stoi (`Time.timeScale = 0`). Po kliknięciu *Start* nazwa jest obcinana (`Trim`), musi mieć 3–12 znaków i składać się z liter, cyfr lub `_` (`char.IsLetterOrDigit`, więc polskie litery są dozwolone); pierwsza litera staje się wielka. Każdy błąd ma własny komunikat pod polem: `"ab"` – za krótka, `"jan kowalski"` – spacja w środku, `"zażółć!"` – niedozwolony znak `!`; `"  ola_2 "` daje `Ola_2`. Przykłady z karty sprawdzisz z menu kontekstowego komponentu (*Test przykładów z karty*);
- `GameScore` (zadanie 5): przechowuje nazwę gracza i podaje ją w komunikacie o wygranej; dziennik w `StringBuilder` dostaje przy każdej monecie linię `[czas] Typ +wartość`; klawisz `L` wypisuje cały dziennik i liczbę wpisów ze słowem `Gold`.

Sterowanie: doszedł klawisz `L` – dziennik zebranych monet w konsoli.

### Zadania dodatkowe

- Wybór poziomu (zadanie 6): poziomy leżą w `Assets/Resources/Levels/` (`level1.txt` przeniesiony razem z plikiem `.meta`, więc referencja w `LevelLoader` została, oraz nowy `level2.txt` z przegrodą w środku i przeszkodą przy prawej ścianie). Póki widać `StartPanel`, klawisze `1` i `2` wczytują poziom przez `LevelLoader.LoadLevel(number)`: nazwa pliku powstaje z numeru (`$"Levels/level{number}"`), a `Resources.Load<TextAsset>` szuka go bez rozszerzenia. Poprzednie ściany i monety są usuwane, gracz trafia na nowe `P`, a liczba monet do wygranej się aktualizuje. Napis `LevelLabel` na panelu pokazuje bieżący poziom. Gdy pole nazwy ma fokus, cyfry trafiają do nazwy, a nie zmieniają poziomu.

Sterowanie: `1` / `2` – wybór poziomu przed startem (kliknij poza polem nazwy).

## Laboratorium 6

Scena `Assets/Scenes/Arena.unity`:

- `Player`: nowe komponenty `Health` (100 punktów, *Destroy On Death* wyłączone, śmierć gracza dojdzie w L8) i `PlayerShooter` z prefabem pocisku;
- `Dummy`: czerwony sześcian (materiał `M_Dummy`) z `BoxCollider` i `Health` (50 punktów, znika po śmierci), stoi na polu wolnym w obu poziomach (wiersz 6, kolumna 8 pliku poziomu);
- `Demos`: doszedł skrypt `ValueVsReferenceDemo` (obiekt nadal jest wyłączony; włącz go, żeby zobaczyć wynik zadania 5 w konsoli).

Prefaby i materiały:

- `Assets/Prefabs/Projectile.prefab`: kula o skali 0,25 z materiałem `M_Projectile` (biały), `SphereCollider` z *Is Trigger*, `Rigidbody` kinematyczny bez grawitacji i skrypt `Projectile`;
- prefab `Wall` ma tag `Wall` od L4, po nim pocisk rozpoznaje ściany.

Skrypty w `Assets/Scripts`:

- `Health` (zadanie 1): `maxHealth` i `destroyOnDeath` w Inspectorze; `Current { get; private set; }` daje odczyt z zewnątrz i zapis tylko w klasie, więc `health.Current = 100;` w innym skrypcie kończy się błędem kompilatora CS0272. `TakeDamage` pomija wartości ujemne i zero oraz nie schodzi poniżej 0, `Heal` nie przekracza `Max`, `IsDead` zwraca `true` przy zerze. Każda zmiana zdrowia trafia do konsoli;
- `WeaponStats` (zadanie 2): zwykła klasa z `[System.Serializable]` (bez `MonoBehaviour`) z prywatnymi polami `damage` 10, `shotsPerSecond` 4, `projectileSpeed` 15 i `projectileLifetime` 2 oraz właściwościami tylko do odczytu. Ma konstruktor bezparametrowy (z niego korzysta Unity) i konstruktor z czterema parametrami, właściwość `Cooldown` = 1 / `shotsPerSecond` i `ToString()`, które `PlayerShooter` wypisuje na starcie (`Broń gracza: 10 obr., 4 strz./s, …`). W Inspectorze `PlayerShooter` widać ją jako rozwijaną sekcję *Stats*;
- `PlayerShooter` (zadanie 3): w `Update` zamienia pozycję kursora na promień z kamery i przecina go z płaszczyzną podłogi (`Plane.Raycast`); obrót w poziomie ustawia ciału przez `rb.MoveRotation` w `FixedUpdate`. Przytrzymany lewy przycisk myszy strzela seryjnie, a pole `nextShotTime` ogranicza tempo do `Cooldown` (4 strzały na sekundę, czyli 8 pocisków w 2 sekundy). Pocisk powstaje 0,8 przed graczem na wysokości 1 nad podłogą. Póki widać panel startowy (`timeScale` = 0), gracz nie celuje i nie strzela;
- `Projectile` (zadanie 4): `Init(damage, velocity, lifetime)` wywoływane zaraz po `Instantiate` zapamiętuje obrażenia i prędkość oraz planuje `Destroy(gameObject, lifetime)`. Ruch przez `rb.MovePosition` w `FixedUpdate`. W `OnTriggerEnter` pomija gracza (tag `Player`), obiekt z `Health` rani i znika, na ścianie (tag `Wall`) znika, a monety mija. `Dummy` znika po pięciu trafieniach;
- `ValueVsReferenceDemo` (zadanie 5): struktura `DamageInfoStruct` i klasa `DamageInfoClass` z polami `amount` i `direction`, trzy wersje `SetAmount` (struktura, klasa, struktura przez `ref`). W konsoli: struktura bez `ref` – 10, klasa – 999, struktura z `ref` – 999. Komentarz nad klasą zawiera przewidywanie i wyjaśnienie (kopia wartości a kopia referencji);
- `CoinFinder`: gracz się teraz obraca, więc strzałka jest ustawiana we współrzędnych świata (`arrow.position`), a nie lokalnych;
- `PlayerMovement`, `GameScore`, `PlayerNameForm`, `DamageCalculator`, `LogicDemo`: sprawdzają, czy `Keyboard.current` nie jest `null` (bez podłączonej klawiatury skrypty nie rzucają wyjątku).

Sterowanie: doszła mysz – kursor wskazuje kierunek patrzenia gracza, lewy przycisk strzela (przytrzymanie – ogień seryjny).

### Zadania dodatkowe

- Licznik pocisków (zadanie 6): `Projectile` ma statyczną właściwość `AliveCount { get; private set; }`, zwiększaną w `Awake` i zmniejszaną w `OnDestroy`, więc pokazuje liczbę pocisków istniejących w danej chwili (przy 4 strzałach na sekundę i czasie życia 2 s najwyżej 8). Metoda z atrybutem `[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]` zeruje licznik przy każdym starcie gry, także przy wyłączonym *Domain Reload*. `Hud` dopisuje do górnego paska `Pociski: n`.

## Laboratorium 7

### Gotowi przeciwnicy

W tej gałęzi jest folder `Assets/Enemy` z pięcioma gotowymi przeciwnikami zbudowanymi z prymitywów: `Chaser` (czerwona kapsuła z pazurami), `Shooter` (fioletowa kostka z działkiem), `Tank` (do zadania 6 ★), `Swarmer` (mała kolczasta kulka) i `Barrel` (beczka do zadania 7 ★). Każdy ma `Rigidbody` (nie kinematyczny, zablokowane obroty X, Y, Z), collider, `Animator` z animacjami `Idle`, `Move`, `Attack` i `Hit` (beczka: `Idle` i `Hit`) oraz skrypt `EnemyAnimator`. W tym samym folderze są materiały, kontrolery animacji, scena pokazowa `EnemyShowcase` i README. Folder nie zależy od innych skryptów projektu, więc można go skopiować do własnego projektu i podpiąć prefaby do skryptów.

### Jak zaimportować przeciwników do swojego projektu

1. **Pobierz folder.** Na stronie <https://github.com/MateuszBlachacz/unity-csharp-lab/tree/lab-07> kliknij **Code → Download ZIP** i rozpakuj archiwum. Potrzebny jest tylko folder `Assets/Enemy`. Jeśli masz sklonowane repozytorium kursu (osobno od swojego projektu), przełącz je na gałąź `lab-07` (`git fetch origin`, `git checkout lab-07`) i skopiuj folder stamtąd.
2. **Skopiuj go do projektu.** Skopiuj cały folder `Enemy` **razem z plikami `.meta`** do folderu `Assets` swojego projektu `Arena` i wróć do Unity. Po chwili w oknie *Project* pojawi się `Assets/Enemy`. Bez plików `.meta` prefaby stracą materiały i animacje. Projekt musi używać URP (szablon *Universal 3D*); inaczej materiały będą różowe.
3. **Obejrzyj przeciwników.** Otwórz scenę `Assets/Enemy/EnemyShowcase` i naciśnij **Play**: przeciwnicy sami pokazują wszystkie animacje. Potem wróć do sceny `Arena`.
4. **Dodaj swoje skrypty do prefabów.** Prefaby celowo nie mają `Health` ani skryptów przeciwników, bo te piszesz na zajęciach (gdyby były w prefabie, po skopiowaniu do innego projektu odwołania by się zepsuły). Kliknij dwukrotnie prefab, żeby otworzyć go w trybie edycji prefabu, i przez **Add Component** dodaj:

   | Prefab | Dodaj |
   | --- | --- |
   | `Chaser`, `Swarmer` | `Health` i `Chaser` |
   | `Shooter` | `Health` i `Shooter` |
   | `Tank` | `ArmoredHealth` i `Tank` |
   | `Barrel` | swój skrypt beczki z `IDamageable` (bez `Health`) |

   Nie usuwaj komponentów `Animator` i `EnemyAnimator`, które już są na prefabach.
5. **Podłącz prefaby do spawnera.** Zaznacz obiekt z `EnemySpawner` i przeciągnij prefaby z `Assets/Enemy` do tablicy `Enemy Prefabs`. Pole typu `Enemy[]` przyjmie prefab dopiero wtedy, gdy ma komponent dziedziczący po `Enemy` (krok 4).
6. **Włącz animację ataku.** Bieg i trafienie animują się same (`EnemyAnimator` patrzy na prędkość `Rigidbody` i spadek `Health`). Animację ataku włącz w miejscu, w którym przeciwnik zadaje obrażenia albo strzela:

   ```csharp
   GetComponent<EnemyAnimator>().PlayAttack();
   ```

   W beczce, która nie ma `Health`, trafienie włączasz ręcznie: `GetComponent<EnemyAnimator>().PlayHit();`.

## Struktura folderów

```
Assets/
  Enemy/       gotowi przeciwnicy z L7: prefaby, materiały, animacje, EnemyAnimator, scena EnemyShowcase
  Materials/   materiały M_Floor, M_Player, M_Coin, M_Wall, M_Arrow, M_Dummy, M_Projectile
  Prefabs/     prefab monety, jego warianty, prefab ściany i prefab pocisku
  Resources/   Levels/: poziomy w plikach tekstowych
  Scenes/      scena Arena
  Scripts/     skrypty C#
  Settings/    ustawienia URP z szablonu
Docs/          notatki do laboratoriów
Wyklady/       przykłady kodu z wykładów 1 i 2 (poza Assets, Unity ich nie kompiluje)
Packages/      lista pakietów
ProjectSettings/
```

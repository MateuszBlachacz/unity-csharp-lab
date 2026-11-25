# Folder Enemy – gotowi przeciwnicy do L7

Wszystko w jednym folderze: prefaby, materiały, animacje, kontrolery animacji, skrypt
`EnemyAnimator` i scena pokazowa. Folder nie zależy od żadnego skryptu z projektu,
więc wystarczy go skopiować (razem z plikami `.meta`) do `Assets` dowolnego projektu z URP.

## Co jest w środku

| Prefab | Wygląd | Do zadania | Rigidbody / collider |
|---|---|---|---|
| `Chaser` | czerwona kapsuła z rogami, żółtymi oczami i pazurami | 3 – przeciwnik goniący | masa 1, `CapsuleCollider` |
| `Shooter` | fioletowa kostka z okiem, antenką i działkiem | 4 – przeciwnik strzelający | masa 1, `BoxCollider` |
| `Tank` | duży opancerzony kolos z pięściami | 6 ★ – `Tank : Chaser` | masa 5, `CapsuleCollider` |
| `Swarmer` | mała pomarańczowa kulka z kolcami | dodatkowy, szybki `Chaser` | masa 0,5, `SphereCollider` |
| `Barrel` | czerwona beczka z żółtymi pasami | 7 ★ – wybuchająca beczka | masa 3, `CapsuleCollider` |

Każdy prefab ma `Rigidbody` (nie kinematyczny, zablokowane obroty X, Y, Z), collider,
`Animator` i `EnemyAnimator`. Punkt zaczepienia (pivot) jest przy podłodze.

Scena `EnemyShowcase` pokazuje wszystkich naraz – po wciśnięciu Play przełączają się
same między staniem, biegiem, atakiem i trafieniem.

## Jak zaimportować do swojego projektu

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

## Animacje

Każdy przeciwnik ma kontroler z animacjami `Idle`, `Move`, `Attack` i `Hit`
(beczka: `Idle` i `Hit`). Animowany jest obiekt potomny `Model`, a nie korzeń z `Rigidbody`,
więc animacje nie przeszkadzają fizyce.

`EnemyAnimator` robi większość sam:

- **Move** – włącza się, gdy `Rigidbody` porusza się w poziomie (parametr `Speed`),
- **Hit** – włącza się, gdy spadnie `Current` w komponencie `Health` na tym samym obiekcie,
- **Attack** – wywołaj ze swojego skryptu, np. przy strzale albo uderzeniu:

```csharp
GetComponent<EnemyAnimator>().PlayAttack();
```

Trafienie można też włączyć ręcznie: `GetComponent<EnemyAnimator>().PlayHit();`
(przydatne w beczce, która nie ma `Health`).

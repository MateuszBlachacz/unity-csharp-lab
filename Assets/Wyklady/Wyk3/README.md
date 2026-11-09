# Wykład 3: klasy, struktury, dziedziczenie i interfejsy

Przykłady z wykładu 3 kursu „Programowanie w C# w środowisku Unity” zebrane w jednej scenie pokazowej.

## Jak uruchomić

1. Otwórz scenę `Assets/Wyklady/Wyk3/Wyk3_Przyklady.unity`.
2. Wciśnij Play i otwórz okno Console: przykłady z `Start` wypisują wyniki z przedrostkiem `[Wyk3]`.
3. Steruj graczem (niebieska kapsuła) i sprawdź pokazy: przeciwników, pociski, skrzynie i przedmioty do zebrania.

Na scenie:

- `DemoPlayer`: gracz z `DemoHealth` (100);
- `Demos`: obiekt z pokazami do konsoli (`ClassesDemo`, `ValueVsReferenceDemo`, `InheritanceDemo`, `ScoreRankingDemo`, `Weapon`, `NestedTypesEnemy`), pauzą (`DemoGameInput`) i `EnemySpawner`;
- `Chaser`, `Turret`, `Tank` w rogach podłogi (zdrowie 30, 50 i 80);
- `Crate1`, `Crate2`: skrzynie niszczone jednym trafieniem;
- `Coin1`–`Coin3`, `Medkit`, `Shield`: przedmioty do zebrania.

## Sterowanie

| Klawisz | Akcja                                  |
|---------|----------------------------------------|
| W/A/S/D | ruch gracza                            |
| spacja  | strzał w kierunku ruchu                |
| `1`     | fala 3 losowych przeciwników           |
| `K`     | zabicie wszystkich przeciwników        |
| `P`     | pauza i wznowienie                     |

## Slajd wykładu → plik i klasa

| Slajd                                        | Plik (w `Scripts/`)                                         | Klasa                                   |
|----------------------------------------------|-------------------------------------------------------------|-----------------------------------------|
| Własna klasa                                 | `01_Klasy/WeaponStats.cs`                                   | `WeaponStats`                           |
| Konstruktory                                 | `01_Klasy/PickupData.cs`                                    | `PickupData`                            |
| Hermetyzacja w praktyce                      | `01_Klasy/HealthNoEncapsulation.cs`, `HealthEncapsulated.cs` | `HealthNoEncapsulation`, `HealthEncapsulated` |
| Klasy serializowane                          | `01_Klasy/SerializedWeaponStats.cs`, `Weapon.cs`            | `SerializedWeaponStats`, `Weapon`       |
| Typy zagnieżdżone i klasy statyczne          | `01_Klasy/NestedTypesEnemy.cs`, `ArenaMath.cs`              | `NestedTypesEnemy`, `ArenaMath`         |
| Wszystko dziedziczy po object                | `01_Klasy/ClassesDemo.cs`                                   | `ClassesDemo`                           |
| Struktura                                    | `02_WartoscReferencja/DamageInfo.cs`                        | `DamageInfo`                            |
| Quiz: struct czy class?, transform.position, null | `02_WartoscReferencja/ValueVsReferenceDemo.cs`         | `ValueVsReferenceDemo`                  |
| Klasa bazowa, virtual i override, abstract   | `03_Dziedziczenie/Enemy.cs`                                 | `Enemy`                                 |
| Klasy pochodne, base                         | `03_Dziedziczenie/Chaser.cs`, `Turret.cs`                   | `Chaser`, `Turret`                      |
| Slajd pokazowy: nadpisanie Die()             | `03_Dziedziczenie/Tank.cs`                                  | `Tank`                                  |
| Konstruktory w hierarchii: base              | `03_Dziedziczenie/Item.cs`, `ItemWeapon.cs`                 | `Item`, `ItemWeapon`                    |
| protected, sealed i ukrywanie metod          | `03_Dziedziczenie/DescribedEnemy.cs`, `Boss.cs`             | `DescribedEnemy`, `Boss`                |
| is, as, switch; quiz o konstruktorach        | `03_Dziedziczenie/InheritanceDemo.cs`                       | `InheritanceDemo`                       |
| Polimorfizm w praktyce                       | `03_Dziedziczenie/EnemySpawner.cs`                          | `EnemySpawner`                          |
| Interfejs: kontrakt                          | `04_Interfejsy/IDamageable.cs`, `DemoHealth.cs`, `Crate.cs` | `IDamageable`, `DemoHealth`, `Crate`    |
| Interfejsy w Unity: GetComponent             | `04_Interfejsy/DemoProjectile.cs`, `ContactDamage.cs`       | `DemoProjectile`, `ContactDamage`       |
| IComparable                                  | `04_Interfejsy/ScoreEntry.cs`, `ScoreRankingDemo.cs`        | `ScoreEntry`, `ScoreRankingDemo`        |
| Zadanie projektowe: przedmioty do zebrania   | `05_ZadanieProjektowe/Pickup.cs`, `Coin.cs`, `Medkit.cs`, `Shield.cs` | `Pickup`, `Coin`, `Medkit`, `Shield` |
| (pomocnicze do pokazów)                      | `05_ZadanieProjektowe/DemoPlayer.cs`, `DemoGame.cs`, `DemoGameInput.cs` | `DemoPlayer`, `DemoGame`, `DemoGameInput` |

## Kiedy struct, a kiedy class?

| `struct` (typ wartościowy)                                   | `class` (typ referencyjny)                                 |
|--------------------------------------------------------------|------------------------------------------------------------|
| małe dane, które traktujemy jak jedną wartość (`Vector3`, `DamageInfo`) | obiekty z tożsamością i stanem zmieniającym się w czasie (gracz, przeciwnik, broń) |
| przypisanie i przekazanie do metody kopiuje dane             | przypisanie i przekazanie kopiuje tylko referencję          |
| nie może być `null` (chyba że `T?`), ma wartość `default`     | może być `null`                                             |
| nie dziedziczy po innych typach (może implementować interfejsy) | może dziedziczyć i być bazą dla innych klas             |
| dobra, gdy obiekt jest krótkotrwały i niezmienny             | dobra, gdy obiekt współdzielą różne części programu        |

## Niezależność od gry

Folder jest niezależny od gry z laboratoriów: wszystkie skrypty są w przestrzeni nazw `Wyklady.Wyk3` i nie używają klas z `Assets/Scripts` (gracz, zdrowie i menedżer gry mają tu własne, uproszczone wersje `DemoPlayer`, `DemoHealth`, `DemoGame`). Scena `Wyk3_Przyklady` nie jest dodana do Build Settings, a projekt nie ma nowych tagów ani warstw.

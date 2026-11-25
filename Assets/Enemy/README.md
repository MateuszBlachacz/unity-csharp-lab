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

## Jak podłączyć

1. Skopiuj folder `Enemy` do `Assets`.
2. Otwórz prefab (np. `Chaser`) i dodaj swoje komponenty: `Health` (w `Tank` – `ArmoredHealth`)
   i swój skrypt przeciwnika (`Chaser`, `Shooter`...). Beczka dostaje tylko swój skrypt z `IDamageable`.
3. Przeciągnij prefaby do tablicy `enemyPrefabs` w `EnemySpawner`.

Prefaby celowo nie mają `Health` ani skryptów przeciwników – te piszesz sam na zajęciach,
a gdyby były w prefabie, po skopiowaniu do innego projektu odwołania by się zepsuły.

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

# L01: typowe błędy

Cztery błędy z zadania 11, każdy z objawem w Unity 6 i sposobem naprawy.

## 1. Nazwa klasy inna niż nazwa pliku

Przykład: plik `Spinner.cs`, a w nim `public class CoinSpinner : MonoBehaviour`.

Objaw: kod kompiluje się bez błędów, ale skryptu nie da się dodać do obiektu. Unity informuje, że nie może znaleźć klasy skryptu i każe sprawdzić, czy nazwa pliku i nazwa klasy są zgodne. Na obiektach, które już miały ten skrypt, komponent pokazuje komunikat *The associated script can not be loaded*, a jego pola znikają z Inspectora.

Naprawa: nazwa klasy musi być identyczna z nazwą pliku, łącznie z wielkością liter. Zmień nazwę klasy albo pliku (najlepiej w oknie Project, żeby plik `.meta` poszedł razem z nim).

## 2. Metoda `update` napisana małą literą

Przykład: `private void update() { ... }`.

Objaw: brak jakiegokolwiek błędu w konsoli, a kod w metodzie nigdy się nie wykonuje (moneta stoi w miejscu). Unity szuka metod po dokładnej nazwie, a `update` to dla niej zwykła metoda, której nikt nie wywołuje.

Naprawa: `Update` wielką literą. To samo dotyczy `Start`, `Awake`, `OnEnable` i pozostałych metod cyklu życia.

## 3. Brak średnika

Przykład: `transform.Rotate(0f, degreesPerSecond * Time.deltaTime, 0f, Space.World)` bez `;` na końcu.

Objaw: czerwony błąd kompilacji w konsoli, np. `error CS1002: ; expected`, z nazwą pliku oraz numerem linii i kolumny. Próba uruchomienia gry kończy się komunikatem, że przed wejściem w tryb Play trzeba naprawić wszystkie błędy kompilacji. Błąd w jednym skrypcie blokuje grę w całym projekcie, bo Unity kompiluje wszystkie skrypty razem.

Naprawa: kliknij dwukrotnie błąd w konsoli, edytor kodu otworzy plik we wskazanym miejscu. Dopisz średnik, zapisz plik i poczekaj na ponowną kompilację.

## 4. Zmiana wartości domyślnej pola po podłączeniu skryptu

Przykład: w `HelloArena` zmieniasz `lives = 3` na `lives = 5`, a obiekt `Player` miał już ten skrypt.

Objaw: po uruchomieniu gry nadal widać 3 życia. Wartość pola zapisała się w scenie w chwili dodania komponentu i to ona obowiązuje. Wartość z kodu jest używana tylko przy dodaniu skryptu do nowego obiektu.

Naprawa: zmień wartość w Inspectorze albo kliknij menu komponentu (trzy kropki) → Reset, żeby wczytać wartości domyślne z kodu. Reset przywraca wszystkie pola komponentu, nie tylko to jedno.

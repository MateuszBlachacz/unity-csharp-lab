// Wykład 2, slajd: Zadanie na żywo: przykładowe rozwiązanie
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

string FormatTime(float seconds)
{
    int t = (int)seconds;                   // 125
    return $"{t / 60:D2}:{t % 60:D2}";      // "02:05"
}
bool IsValidName(string name)
{
    if (name == null || name.Length < 3 || name.Length > 12) return false;
    foreach (char c in name) if (!char.IsLetterOrDigit(c)) return false;
    return true;
}
string Initials(string fullName)
{
    string result = "";
    foreach (string part in fullName.Split(' '))
        if (part.Length > 0) result += char.ToUpper(part[0]);
    return result;
}

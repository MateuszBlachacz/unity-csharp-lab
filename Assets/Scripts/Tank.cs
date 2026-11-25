// Zadanie 6 ★ (L7): Tank zachowuje się jak Chaser (dziedziczy ruch i obrażenia przy zetknięciu),
// a różni się wartościami w prefabie: jest wolniejszy, większy, mocniej uderza i ma ArmoredHealth.
public class Tank : Chaser
{
    public override string Describe()
    {
        string armor = TryGetComponent(out ArmoredHealth health) ? $", pancerz {health.Armor}" : "";
        return base.Describe() + $", opancerzony{armor}";
    }
}

// Zadanie 1 (L7): wszystko, co może przyjąć obrażenia. Pocisk nie musi wiedzieć,
// czy trafił w Health, ArmoredHealth czy w beczkę – wystarczy mu ten interfejs.
public interface IDamageable
{
    void TakeDamage(int amount);
}

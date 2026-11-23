// Wyk4, slajdy o kolekcjach (dane przeciwnika do przykładów w konsoli)
namespace Wyklady.Wyk4
{
    // Zwykła klasa C#: zastępuje komponent Enemy tam, gdzie wystarczą same dane
    // (bez obiektów w scenie, więc przykłady z kolekcji da się wypisać w konsoli).
    public class EnemyInfo
    {
        public string Name { get; }
        public string TypeName { get; }
        public bool IsDead { get; set; }
        public bool IsAlive => !IsDead;
        public float Distance { get; set; }

        public EnemyInfo(string name, string typeName, float distance, bool isDead = false)
        {
            Name = name;
            TypeName = typeName;
            Distance = distance;
            IsDead = isDead;
        }

        public override string ToString() => $"{Name} ({TypeName}, {Distance:F1} m{(IsDead ? ", martwy" : "")})";
    }
}

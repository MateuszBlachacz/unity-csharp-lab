// Wyk4, slajd „Krótko o LINQ” (FirstOrDefault(e => e is Boss))
namespace Wyklady.Wyk4
{
    public class Boss : EnemyInfo
    {
        public Boss(string name, float distance) : base(name, "Boss", distance)
        {
        }
    }
}

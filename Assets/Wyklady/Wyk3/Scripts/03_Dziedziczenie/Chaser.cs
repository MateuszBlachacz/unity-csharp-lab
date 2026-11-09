// Wyk3, slajd „Klasy pochodne: Chaser i Turret”
namespace Wyklady.Wyk3
{
    public class Chaser : Enemy
    {
        // ChasePlayer ze slajdu: ruch w stronę gracza przez Vector3.MoveTowards.
        protected override void Act()
        {
            MoveTowardsPlayer(speed);
        }
    }
}

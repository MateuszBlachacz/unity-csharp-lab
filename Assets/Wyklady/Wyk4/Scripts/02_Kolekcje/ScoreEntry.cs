// Wyk4, slajd „Sortowanie list”
namespace Wyklady.Wyk4
{
    public class ScoreEntry
    {
        public string Name;
        public int Score;
        public float Time;

        public ScoreEntry(string name, int score, float time)
        {
            Name = name;
            Score = score;
            Time = time;
        }

        public override string ToString() => $"{Name} {Score} ({Time:F1} s)";
    }
}

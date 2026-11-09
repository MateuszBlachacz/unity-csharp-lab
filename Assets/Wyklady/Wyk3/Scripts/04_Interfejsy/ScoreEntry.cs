// Wyk3, slajd „Interfejsy z biblioteki: IComparable”
using System;

namespace Wyklady.Wyk3
{
    // IComparable<T> pozwala List<T>.Sort() porównywać obiekty tej klasy.
    public class ScoreEntry : IComparable<ScoreEntry>
    {
        public string PlayerName { get; }
        public int Points { get; }

        public ScoreEntry(string playerName, int points)
        {
            PlayerName = playerName;
            Points = points;
        }

        // Porównanie odwrócone (other z this), więc sortowanie jest malejące.
        public int CompareTo(ScoreEntry other)
        {
            if (other == null)
            {
                return -1;
            }

            return other.Points.CompareTo(Points);
        }

        public override string ToString()
        {
            return $"{PlayerName}: {Points} pkt";
        }
    }
}

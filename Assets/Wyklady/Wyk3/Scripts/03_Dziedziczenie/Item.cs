// Wyk3, slajd „Konstruktory w hierarchii: base”
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class Item
    {
        public string Name { get; }

        public Item(string name)
        {
            Name = name;
            Debug.Log($"[Wyk3] Item({name})");
        }

        public virtual string Describe()
        {
            return $"przedmiot {Name}";
        }
    }
}

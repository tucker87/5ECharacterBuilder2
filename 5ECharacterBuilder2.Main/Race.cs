using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder2.Main
{
    public abstract class Race
    {
        public string Name { get; set; }
        public List<int> BonusValues { get; set; } = Enumerable.Repeat(0, 6).ToList();
    }
}
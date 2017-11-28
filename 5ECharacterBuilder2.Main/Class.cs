using System;
using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder2.Main
{
    public abstract class Class
    {
        public int Level { get; set; } = 1;
        public List<Func<int>> BonusValues { get; set; } = Enumerable.Repeat<Func<int>>(() => 0, 6).ToList();
    }
}
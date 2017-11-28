using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder2.Main
{
    public class AttributeCollection
    {
        private readonly Character _character;

        public AttributeCollection(Character character)
        {
            _character = character;
            var i = 0;
            List = Enumerable.Repeat(new Attribute(this, i++), 6).ToList();
        }

        public List<Attribute> List { get; set; }
        public Attribute Strength => List[0];
        public Attribute Dexterity => List[1];
        public Attribute Constitution => List[2];
        public Attribute Intelligence => List[3];
        public Attribute Wisdom => List[4];
        public Attribute Charisma => List[5];

        public List<int> BonusValues()
        {
            var classBonuses = _character.Classes.Aggregate(Enumerable.Repeat(0, 6), (a, b) => a.Zip(b.BonusValues, (c, d) => c + d()));
            return _character.Race == null
                ? classBonuses.ToList()
                : classBonuses.Zip(_character.Race.BonusValues, (a, b) => a + b).ToList();
        }
    }
}
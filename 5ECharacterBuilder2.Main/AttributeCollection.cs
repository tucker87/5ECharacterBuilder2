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
            List = Enumerable.Range(0, 6).Select(i => new Attribute(this, i)).ToList();
        }

        public List<Attribute> List { get; set; }
        public Attribute Strength => List[0];
        public Attribute Dexterity => List[1];
        public Attribute Constitution => List[2];
        public Attribute Intelligence => List[3];
        public Attribute Wisdom => List[4];
        public Attribute Charisma => List[5];
    }
}
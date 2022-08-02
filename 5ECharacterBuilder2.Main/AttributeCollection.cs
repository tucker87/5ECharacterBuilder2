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
            List = Enumerable.Range(0, 6).Select(i => new CharacterAttribute(this, i)).ToList();
        }

        public List<CharacterAttribute> List { get; set; }
        public CharacterAttribute Strength => List[0];
        public CharacterAttribute Dexterity => List[1];
        public CharacterAttribute Constitution => List[2];
        public CharacterAttribute Intelligence => List[3];
        public CharacterAttribute Wisdom => List[4];
        public CharacterAttribute Charisma => List[5];
    }
}
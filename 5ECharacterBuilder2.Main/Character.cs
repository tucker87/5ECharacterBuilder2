using System.Collections.Generic;

namespace _5ECharacterBuilder2.Main
{
    public class Character
    {
        public Character()
        {
            Attributes = new AttributeCollection(this);
        }

        public AttributeCollection Attributes { get; set; }
        public List<Class> Classes { get; set; } = new List<Class>();
        public Race Race { get; set; }
    }

    public class TestRace : Race
    {
        public TestRace()
        {
            Name = "TestRace";
            BonusValues[0] = 2;
        }
    }
}
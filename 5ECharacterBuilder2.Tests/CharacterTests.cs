using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using _5ECharacterBuilder2.Main;

namespace _5ECharacterBuilder2.Tests
{
    public class CharacterTests
    {
        [Fact]
        public void Can_Make_A_Character()
        {
            //Arrange
            //Act
            var character = new Character();
            //Assert
            Assert.NotNull(character);
        }

        [Fact]
        public void Characters_Have_Attributes_That_Start_At_10()
        {
            //Arrange
            //Act
            var character = new Character();
            //Assert
            Assert.Equal(10, character.Attributes.Strength.Value);
            Assert.Equal(10, character.Attributes.Dexterity.Value);
            Assert.Equal(10, character.Attributes.Constitution.Value);
            Assert.Equal(10, character.Attributes.Intelligence.Value);
            Assert.Equal(10, character.Attributes.Wisdom.Value);
            Assert.Equal(10, character.Attributes.Constitution.Value);
        }

        [Fact]
        public void Characters_Have_Class_Levels_Starting_At_1()
        {
            //Arrange
            //Act
            var character = new Character
            {
                Classes = new List<Class> { new TestClass() }
            };
            //Assert
            Assert.IsType<TestClass>(character.Classes.First());
            Assert.Equal(1, character.Classes.First().Level);
        }

        [Fact]
        public void Characters_Classes_Can_Alter_Attribute_Bonuses()
        {
            //Arrange
            //Act
            var character = new Character
            {
                Classes = new List<Class> {new TestClass()}
            };

            //Assert
            Assert.Equal(12, character.Attributes.Strength.Value);
        }

        [Theory]
        [InlineData(2, 14)]
        [InlineData(3, 16)]
        [InlineData(4, 18)]
        public void Characters_Classes_Can_Alter_Attribute_Bonuses_Based_On_Level(int level, int stat)
        {
            //Arrange
            //Act
            var character = new Character
            {
                Classes = new List<Class> {new TestClass {Level = level}}
            };

            //Assert
            Assert.Equal(stat, character.Attributes.Strength.Value);
        }

        [Fact]
        public void Characters_Can_Have_A_Race()
        {
            //Arrange
            //Act
            var character = new Character
            {
                Race = new TestRace()
            };

            //Assert
            Assert.Equal("TestRace", character.Race.Name);
        }

        [Fact]
        public void Characters_Race_Can_Alter_Attribute_Bonuses()
        {
            //Arrange
            //Act
            var character = new Character
            {
                Race = new TestRace()
            };

            //Assert
            Assert.Equal(12, character.Attributes.Strength.Value);
        }

        [Fact]
        public void Characters_Race_And_Class_Attribute_Bonuses_Can_Stack()
        {
            //Arrange
            //Act
            var character = new Character
            {
                Race = new TestRace(),
                Classes = new List<Class> { new TestClass() }
            };

            //Assert
            Assert.Equal(14, character.Attributes.Strength.Value);
        }
    }
}

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

    public class Attribute
    {
        private readonly AttributeCollection _attributeCollection;
        private readonly int _index;

        public Attribute(AttributeCollection attributeCollection, int index)
        {
            _attributeCollection = attributeCollection;
            _index = index;
        }

        public int BaseValue { get; set; } = 10;

        public int Modifier => (int) Math.Floor((decimal) (Value - 10) / 2);
        public int Value => BaseValue + _attributeCollection.BonusValues()[_index];
    }

    public abstract class Class
    {
        public int Level { get; set; } = 1;
        public List<Func<int>> BonusValues { get; set; } = Enumerable.Repeat<Func<int>>(() => 0, 6).ToList();
    }

    public class TestClass : Class
    {
        public TestClass()
        {
            BonusValues[0] = () => Level * 2;
        }
    }

    public abstract class Race
    {
        public string Name { get; set; }
        public List<int> BonusValues { get; set; } = Enumerable.Repeat(0, 6).ToList();
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
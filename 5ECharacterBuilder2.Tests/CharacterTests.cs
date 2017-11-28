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
using _5ECharacterBuilder2.Main;
using _5ECharacterBuilder2.Main.Data;

namespace _5ECharacterBuilder2.Tests;

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
    public async void Classes_Can_Effect_BaseAttributes()
    {
        //Arrange
        var parser = new Parser("TestInputs");

        //Act
        var levelOneCharacter = await parser.LoadCharacter(new CharacterSave
        {
            Race = "TestRace",
            Classes = new List<ClassSave> { new() { Name = "TestClass" } }
        });

        var levelTwoCharacter = await parser.LoadCharacter(new CharacterSave
        {
            Race = "TestRace",
            Classes = new List<ClassSave> { new() { Name = "TestClass", Level = 2 } }
        });

        //Assert
        Assert.Equal(12, levelOneCharacter.Attributes.Strength.BaseValue);
        Assert.Equal(14, levelTwoCharacter.Attributes.Strength.BaseValue);
    }

    [Fact]
    public async void Effects_Are_Based_On_Level()
    {
        //Arrange
        var parser = new Parser("TestInputs");

        //Act
        var character = await parser.LoadCharacter(new CharacterSave
        {
            Race = "TestRace",
            Classes = new List<ClassSave> { new() { Name = "TestClass", Level = 3 } }
        });

        //Assert
        Assert.Equal(14, character.Attributes.Strength.BaseValue);
    }
}
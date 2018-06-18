using Xunit;
using _5ECharacterBuilder2.ParserNS;

namespace _5ECharacterBuilder2.Tests
{
    public class RogueTests
    {
        [Fact]
        public async void Can_Load_A_Character()
        {
            //Arrange
            var parser = new Parser();

            //Act
            var character = await parser.LoadCharacter("TestInputs/TestRogue.json");
            //Assert
            Assert.NotNull(character);
        }
    }
}
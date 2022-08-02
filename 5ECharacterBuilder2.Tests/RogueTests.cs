using _5ECharacterBuilder2.ParserNS;

namespace _5ECharacterBuilder2.Tests
{
    [UsesVerify]
    public class RogueTests
    {
        [Fact]
        public async Task Can_Load_A_Character()
        {
            //Arrange
            var parser = new Parser();

            //Act
            var character = await parser.LoadCharacter("TestInputs/TestRogue.json");
            
            //Assert
            await Verify(character);
        }
    }
}
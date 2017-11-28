using Xunit;
using _5ECharacterBuilder2.Main;

namespace _5ECharacterBuilder2.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Can_Make_Characters()
        {
            //Arrange
            //Act
            var character = new Character();
            //Assert
            Assert.NotNull(character);
        }
    }

    
}

namespace _5ECharacterBuilder2.Main
{
    public class Character
    {
    }
}

using Xunit;
using _5ECharacterBuilder2.Main;

namespace _5ECharacterBuilder2.Tests
{
    public class AttributeTests
    {
        [Theory]
        [InlineData(0, -5)]
        [InlineData(1, -5)]
        [InlineData(2, -4)]
        [InlineData(3, -4)]
        [InlineData(4, -3)]
        [InlineData(5, -3)]
        [InlineData(6, -2)]
        [InlineData(7, -2)]
        [InlineData(8, -1)]
        [InlineData(9, -1)]
        [InlineData(10, 0)]
        [InlineData(11, 0)]
        [InlineData(12, 1)]
        [InlineData(13, 1)]
        [InlineData(14, 2)]
        [InlineData(15, 2)]
        [InlineData(16, 3)]
        [InlineData(17, 3)]
        [InlineData(18, 4)]
        [InlineData(19, 4)]
        [InlineData(20, 5)]
        public void Modifiers_Raise_Every_Other_Value_Increase(int value, int modifier)
        {
            //Arrange
            //Act
            var attribute = new Attribute(new AttributeCollection(new Character()), 0)
            {
                BaseValue = value
            };
            //Assert
            Assert.Equal(modifier, attribute.Modifier);
        }
    }
}

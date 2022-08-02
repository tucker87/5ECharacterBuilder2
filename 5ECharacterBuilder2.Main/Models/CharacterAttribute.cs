namespace _5ECharacterBuilder2.Main.Data;

public class CharacterAttribute
{
    private readonly AttributeCollection _attributeCollection;
    private readonly int _index;

    public CharacterAttribute(AttributeCollection attributeCollection, int index)
    {
        _attributeCollection = attributeCollection;
        _index = index;
    }

    public int BaseValue { get; set; } = 10;

    public int Modifier => (int)Math.Floor((decimal)(Value - 10) / 2);
    public int Value => BaseValue;
}
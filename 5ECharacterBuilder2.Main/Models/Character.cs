namespace _5ECharacterBuilder2.Main;

public class Character
{
    public Character()
    {
        Attributes = new AttributeCollection(this);
    }

    public AttributeCollection Attributes { get; set; }
    public List<Class> Classes { get; set; } = new();
    public Race? Race { get; set; }
}
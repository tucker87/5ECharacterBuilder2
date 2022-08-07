namespace _5ECharacterBuilder2.Main;

public class Feature
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<AttributeEffectCollection> AttributeEffects { get; set; } = new();
}
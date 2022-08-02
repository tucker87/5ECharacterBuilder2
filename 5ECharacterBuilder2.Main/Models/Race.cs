namespace _5ECharacterBuilder2.Main;

public class Race : EffectCollection<Race>
{
    public Race(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}
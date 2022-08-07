namespace _5ECharacterBuilder2.Main.Data;

public class CharacterSave
{
    public string Name { get; set; }
    public string Race { get; set; }
    public List<ClassSave> Classes { get; set; }
    public string Path { get; set; }
}
using Newtonsoft.Json;

namespace _5ECharacterBuilder2.Main;

public class Parser
{
    private readonly Dictionary<string, string> _libraries = new();

    public Parser(string? additionalLibrary = null)
    {
        var files = Directory.EnumerateFiles("Library")
            .Select(f => new KeyValuePair<string, string>(Path.GetFileNameWithoutExtension(f), f));

        IEnumerable<KeyValuePair<string, string>>? additionalFiles = null;
        if (!string.IsNullOrEmpty(additionalLibrary))
            additionalFiles = Directory.EnumerateFiles(additionalLibrary)
                .Select(f => new KeyValuePair<string, string>(Path.GetFileNameWithoutExtension(f), f));

        foreach (var file in files)
            _libraries.Add(file.Key, file.Value);

        if (additionalFiles == null) 
            return;

        foreach (var additionalFile in additionalFiles)
            _libraries.Add(additionalFile.Key, additionalFile.Value);
    }

    private static Class ReadClass(string fileAddress, int level) => (Class)JsonConvert.DeserializeObject<Class>(ReadFile(fileAddress))!.SetLevel(level).Compile();

    private static Race ReadRace(string fileAddress) => (Race)JsonConvert.DeserializeObject<Race>(ReadFile(fileAddress))!.Compile();

    private static string ReadFile(string fileAddress) => File.ReadAllText(fileAddress);

    public async Task<Character> LoadCharacter(string fileAddress)
    {
        var json = ReadFile(fileAddress);

        return await Compile(JsonConvert.DeserializeObject<Character>(json)!);
    }

    public async Task<Character> LoadCharacter(Character character) => await Compile(character);

    private async Task<Character> Compile(Character character)
    {
        character.Race = ReadRace(_libraries[character.Race!.Name]);

        for (var i = 0; i < character.Classes.Count; i++)
            character.Classes[i] = ReadClass(_libraries[character.Classes[i].Name], character.Classes[i].Level);

        foreach (var effect in character.Race.AllEffectsOfLevel(character.Classes.Sum(c => c.Level)))
            await effect.EffectFunc!.RunAsync(character);

        foreach (var characterClass in character.Classes)
        foreach (var effect in characterClass.AllEffectsOfLevel(characterClass.Level))
            await effect.EffectFunc!.RunAsync(character);

        return character;
    }
}
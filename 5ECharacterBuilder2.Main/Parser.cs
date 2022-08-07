using _5ECharacterBuilder2.Main.Data;
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

        return await Compile(JsonConvert.DeserializeObject<CharacterSave>(json)!);
    }

    public async Task<Character> LoadCharacter(CharacterSave character) => await Compile(character);

    private async Task<Character> Compile(CharacterSave characterSave)
    {
        var character = new Character
        {
            Race = ReadRace(_libraries[characterSave.Race]),
            Classes = characterSave.Classes.Select(c => ReadClass(_libraries[c.Name], c.Level)).ToList()
        };

        //Apply Race Effects
        foreach (var effect in character.Race.AllEffectsOfLevel(character.Classes.Sum(c => c.Level)))
            await effect.EffectFunc!.RunAsync(character);

        //Apply Class Effects
        foreach (var characterClass in character.Classes)
            foreach (var effect in characterClass.AllEffectsOfLevel(characterClass.Level))
                await effect.EffectFunc!.RunAsync(character);

        return character;
    }
}
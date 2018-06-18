using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using _5ECharacterBuilder2.Main;

namespace _5ECharacterBuilder2.ParserNS
{
    public class Parser
    {
        public Dictionary<string, string> Libraries { get; set; } = new Dictionary<string, string>();

        public Parser(string additionalLibrary = null)
        {
            var files = Directory.EnumerateFiles("Library")
                .Select(f => new KeyValuePair<string, string>(Path.GetFileNameWithoutExtension(f), f));

            IEnumerable<KeyValuePair<string, string>> additionalFiles = null;
            if (!string.IsNullOrEmpty(additionalLibrary))
                additionalFiles = Directory.EnumerateFiles(additionalLibrary)
                    .Select(f => new KeyValuePair<string, string>(Path.GetFileNameWithoutExtension(f), f));

            foreach (var file in files)
                Libraries.Add(file.Key, file.Value);

            if (additionalFiles == null) 
                return;

            foreach (var additionalFile in additionalFiles)
                Libraries.Add(additionalFile.Key, additionalFile.Value);
        }

        public Class ReadClass(string fileAddress, int level)
        {
            var json = ReadFile(fileAddress);

            return (Class)JsonConvert.DeserializeObject<Class>(json)
                .SetLevel(level)
                .Compile();
        }

        public Race ReadRace(string fileAddress)
        {
            var json = ReadFile(fileAddress);

            return (Race)JsonConvert.DeserializeObject<Race>(json)
                .Compile();
        }
        
        private string ReadFile(string fileAddress)
        {
            return File.ReadAllText(fileAddress);
        }

        public async Task<Character> LoadCharacter(string fileAddress)
        {
            var json = ReadFile(fileAddress);

            return await Compile(JsonConvert.DeserializeObject<Character>(json));
        }

        public async Task<Character> LoadCharacter(Character character)
        {
            return await Compile(character);
        }

        public async Task<Character> Compile(Character character)
        {
            character.Race = ReadRace(Libraries[character.Race.Name]);

            for (var i = 0; i < character.Classes.Count; i++)
                character.Classes[i] = ReadClass(Libraries[character.Classes[i].Name], character.Classes[i].Level);

            foreach (var effect in character.Race.AllEffectsOfLevel(character.Classes.Sum(c => c.Level)))
                await effect.EffectFunc.RunAsync(character);

            foreach (var characterClass in character.Classes)
            foreach (var effect in characterClass.AllEffectsOfLevel(characterClass.Level))
                await effect.EffectFunc.RunAsync(character);

            return character;
        }
    }
}

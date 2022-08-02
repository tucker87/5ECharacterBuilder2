namespace _5ECharacterBuilder2.Main
{
    public class Class : EffectCollection<Class>
    {
        public string Name { get; set; }
        public int Level { get; set; } = 1;
        public string Path { get; set; } = string.Empty;

        public Class(string name, int level = 1)
        {
            Name = name;
            Level = level;
        }

        public Class SetLevel(int level)
        {
            Level = level;
            return this;
        }
    }
}
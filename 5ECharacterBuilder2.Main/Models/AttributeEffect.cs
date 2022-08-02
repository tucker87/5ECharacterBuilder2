using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;

namespace _5ECharacterBuilder2.Main;

public class AttributeEffect
{
    public string Code { get; set; } = string.Empty;

    [JsonIgnore]
    public Script<int>? EffectFunc { get; set; }
}
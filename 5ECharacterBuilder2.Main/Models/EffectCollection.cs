using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace _5ECharacterBuilder2.Main;

public class EffectCollection<T>
{
    public List<Feature> Features { get; set; } = new();

    public IEnumerable<AttributeEffect> AllEffects =>
        Features.SelectMany(f => f.AttributeEffects)
            .SelectMany(aec => aec.Values)
            .SelectMany(ael => ael.Values)
            .SelectMany(ae => ae);

    public IEnumerable<AttributeEffect> AllEffectsOfLevel(int level) =>
        Features.SelectMany(f => f.AttributeEffects)
            .SelectMany(aec => aec.Values)
            .SelectMany(ael => ael.Where(ae => ae.Key <= level))
            .OrderByDescending(l => l.Key)
            .SelectMany(l => l.Value);

    public EffectCollection<T> Compile()
    {
        foreach (var effect in AllEffects)
        {
            effect.EffectFunc = CSharpScript.Create<int>(effect.Code, globalsType: typeof(Character));
            effect.EffectFunc.Compile();
        }

        return this;
    }
}
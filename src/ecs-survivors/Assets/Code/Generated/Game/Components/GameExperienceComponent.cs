//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherExperience;

    public static Entitas.IMatcher<GameEntity> Experience {
        get {
            if (_matcherExperience == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Experience);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherExperience = matcher;
            }

            return _matcherExperience;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Code.Gameplay.Features.Loot.LootComponents.Experience experience { get { return (Code.Gameplay.Features.Loot.LootComponents.Experience)GetComponent(GameComponentsLookup.Experience); } }
    public float Experience { get { return experience.Value; } }
    public bool hasExperience { get { return HasComponent(GameComponentsLookup.Experience); } }

    public GameEntity AddExperience(float newValue) {
        var index = GameComponentsLookup.Experience;
        var component = (Code.Gameplay.Features.Loot.LootComponents.Experience)CreateComponent(index, typeof(Code.Gameplay.Features.Loot.LootComponents.Experience));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceExperience(float newValue) {
        var index = GameComponentsLookup.Experience;
        var component = (Code.Gameplay.Features.Loot.LootComponents.Experience)CreateComponent(index, typeof(Code.Gameplay.Features.Loot.LootComponents.Experience));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveExperience() {
        RemoveComponent(GameComponentsLookup.Experience);
        return this;
    }
}

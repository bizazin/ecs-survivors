//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class MetaMatcher {

    static Entitas.IMatcher<MetaEntity> _matcherPurchased;

    public static Entitas.IMatcher<MetaEntity> Purchased {
        get {
            if (_matcherPurchased == null) {
                var matcher = (Entitas.Matcher<MetaEntity>)Entitas.Matcher<MetaEntity>.AllOf(MetaComponentsLookup.Purchased);
                matcher.componentNames = MetaComponentsLookup.componentNames;
                _matcherPurchased = matcher;
            }

            return _matcherPurchased;
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
public partial class MetaEntity {

    static readonly Code.Meta.UI.Shop.ShopComponents.Purchased purchasedComponent = new Code.Meta.UI.Shop.ShopComponents.Purchased();

    public bool isPurchased {
        get { return HasComponent(MetaComponentsLookup.Purchased); }
        set {
            if (value != isPurchased) {
                var index = MetaComponentsLookup.Purchased;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : purchasedComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

using System.Text;
using DesperateDevs.Extensions;
using Entitas;

namespace Code.Common.Entity.ToStrings
{
    public class EntityPrinter
    {
        private readonly INamedEntity _entity;
        private string _oldBaseToStringCache;
        private StringBuilder _toStringBuilder;
        private string _toStringCache;

        public EntityPrinter(INamedEntity entity)
        {
            _entity = entity;
        }

        public string BuildToString()
        {
            if (_toStringCache == null)
            {
                if (_toStringBuilder == null)
                    _toStringBuilder = new StringBuilder();

                _toStringBuilder.Length = 0;

                var components = _entity.GetComponents();

                if (components.Length ==
                    0) // do not set _toStringCache this time since components seem to be initialized later o_O
                    return "No components";

                _toStringBuilder.Append($"{_entity.EntityName(components)}(");

                var num = components.Length - 1;

                for (var index = 0; index < components.Length; ++index)
                {
                    var component = components[index];
                    var type = component.GetType();

                    _toStringBuilder.Append(
                        type.GetMethod(nameof(ToString)).DeclaringType.ImplementsInterface<IComponent>()
                            ? component.ToString()
                            : type.Name.RemoveComponentSuffix());

                    if (index < num)
                        _toStringBuilder.Append(", ");
                }

                _toStringBuilder.Append($")(*{_entity.retainCount})");
                _toStringCache = _toStringBuilder.ToString();

                _oldBaseToStringCache = _entity.BaseToString();
            }

            return _toStringCache;
        }

        public void InvalidateCache()
        {
            if (_oldBaseToStringCache != _entity.BaseToString())
                _toStringCache = null;
        }
    }
}
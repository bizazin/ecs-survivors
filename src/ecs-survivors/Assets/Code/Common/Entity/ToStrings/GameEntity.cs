using System;
using System.Linq;
using System.Text;
using Code.Common.Entity.ToStrings;
using Code.Common.Extensions;
using Code.Gameplay.Features.Enemies;
using Code.Gameplay.Features.Hero;
using Entitas;
using UnityEngine;

// ReSharper disable once CheckNamespace
public sealed partial class GameEntity : INamedEntity
{
    private EntityPrinter _printer;

    public string EntityName(IComponent[] components)
    {
        try
        {
            if (components.Length == 1)
                return components[0].GetType().Name;

            foreach (var component in components)
                switch (component.GetType().Name)
                {
                    case nameof(Hero):
                        return PrintHero();

                    case nameof(Enemy):
                        return PrintEnemy();
                }
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.Message);
        }

        return components.First().GetType().Name;
    }

    public string BaseToString()
    {
        return base.ToString();
    }

    public override string ToString()
    {
        if (_printer == null)
            _printer = new EntityPrinter(this);

        _printer.InvalidateCache();

        return _printer.BuildToString();
    }

    private string PrintHero()
    {
        return new StringBuilder("Hero ")
            .With(s => s.Append($"Id:{Id}"), hasId)
            .ToString();
    }

    private string PrintEnemy()
    {
        return new StringBuilder("Enemy ")
            .With(s => s.Append($"Id:{Id}"), hasId)
            .ToString();
    }
}
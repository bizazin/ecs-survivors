﻿using System;
using System.Linq;
using Code.Common.Entity.ToStrings;
using Entitas;
using UnityEngine;

// ReSharper disable once CheckNamespace3
public sealed partial class MetaEntity : INamedEntity
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
}
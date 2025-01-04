﻿using Code.Gameplay.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Windows
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IStaticDataService _staticData;
        private RectTransform _uiRoot;

        public WindowFactory(IStaticDataService staticData, IInstantiator instantiator)
        {
            _staticData = staticData;
            _instantiator = instantiator;
        }

        public void SetUIRoot(RectTransform uiRoot)
        {
            _uiRoot = uiRoot;
        }

        public BaseWindow CreateWindow(WindowId windowId)
        {
            return _instantiator.InstantiatePrefabForComponent<BaseWindow>(PrefabFor(windowId), _uiRoot);
        }

        private GameObject PrefabFor(WindowId id)
        {
            return _staticData.GetWindowPrefab(id);
        }
    }
}
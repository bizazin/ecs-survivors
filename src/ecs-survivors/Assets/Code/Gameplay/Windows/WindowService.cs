﻿using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Windows
{
    public class WindowService : IWindowService
    {
        private readonly List<BaseWindow> _openedWindows = new();
        private readonly IWindowFactory _windowFactory;

        public WindowService(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        public void Open(WindowId windowId)
        {
            _openedWindows.Add(_windowFactory.CreateWindow(windowId));
        }

        public void Close(WindowId windowId)
        {
            var window = _openedWindows.Find(x => x.Id == windowId);

            _openedWindows.Remove(window);

            GameObject.Destroy(window.gameObject);
        }
    }
}
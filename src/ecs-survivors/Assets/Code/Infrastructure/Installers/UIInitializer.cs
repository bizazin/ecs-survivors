using Code.Gameplay.Windows;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class UIInitializer : MonoBehaviour, IInitializable
    {
        public RectTransform UIRoot;
        private IWindowFactory _windowFactory;

        public void Initialize()
        {
            _windowFactory.SetUIRoot(UIRoot);
        }

        [Inject]
        private void Construct(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }
    }
}
using System;
using System.Collections;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.LevelUp.Behaviours
{
    public class AbilityCard : MonoBehaviour
    {
        private const float StampAnimationTime = 1f;
        public AbilityId Id;
        public Image Icon;
        public TextMeshProUGUI Description;
        public Button Button;
        public GameObject Stamp;
        private Action<AbilityId> _onSelected;

        public void Setup(AbilityId id, AbilityLevel level, Action<AbilityId> onSelected)
        {
            Id = id;
            Icon.sprite = level.Icon;
            Description.text = level.Description;
            
            _onSelected = onSelected;
            
            Button.onClick.AddListener(SelectCard);
        }

        private void OnDestroy()
        {
            Button.onClick.RemoveListener(SelectCard);
        }

        private void SelectCard()
        {
            StartCoroutine(StampAndReport());
        }

        private IEnumerator StampAndReport()
        {
            Stamp.SetActive(true);
            yield return new WaitForSeconds(StampAnimationTime);
            
            _onSelected?.Invoke(Id);
        }
    }
}
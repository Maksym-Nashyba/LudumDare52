using System;
using System.Linq;
using Asteroids;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Gameplay.Interactions.GameplayMenus.AsteroidCatcher
{
    public class AsteroidCatcherDisplay : MonoBehaviour
    {
        public Button NextButton => _nextButton;
        [SerializeField] private Button _nextButton;
        public Button CatchButton => _catchButton;
        [SerializeField] private Button _catchButton;

        [SerializeField] private TextMeshProUGUI _nameValue;
        [SerializeField] private TextMeshProUGUI _layersValue;
        [SerializeField] private TextMeshProUGUI _rarityValue;

        private Action nextButtonCallback;
        private Action catchButtonCallback;
        
        public void Show(Action onBackButton, Action onCatchButton)
        {
            gameObject.SetActive(true);
            nextButtonCallback = onBackButton;
            catchButtonCallback = onCatchButton;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void DisplayAsteroid(Asteroid asteroid)
        {
            _nameValue.SetText(RandomString(Random.Range(5, 9)));            
            _layersValue.SetText(""+(int)asteroid.Size);
            _rarityValue.SetText(asteroid.GetRarestMaterial().Rarity.ToString());
        }

        public void OnNextButton()
        {
            nextButtonCallback?.Invoke();
        }

        public void OnCatchButton()
        {
            catchButtonCallback?.Invoke();
        }
        
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Range(0, s.Length)]).ToArray());
        }
    }
}
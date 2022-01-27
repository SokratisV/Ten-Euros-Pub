using RoboRyanTron.SceneReference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pub
{
    public class InGameUi : MonoBehaviour
    {
        [SerializeField] private RectTransform coinParent;
        [SerializeField] private TextMeshProUGUI roundCounter;
        [SerializeField] private TextMeshProUGUI timeLeft;
        [SerializeField] private SceneReference menu;
        [SerializeField] private Button backButton;
        [SerializeField] private GameLoop gameLoop;
        [SerializeField] private RectTransform coinPrefab;
        [SerializeField] private CoinClickEvent clickEvent;
        [SerializeField] private AudioEngine audioEngine;
        [SerializeField] private SoundLibrary soundLibrary;

        private void Awake()
        {
            backButton.onClick.AddListener(() =>
            {
                audioEngine.Play(soundLibrary.Back);
                menu.LoadScene();
            });
            gameLoop.OnRoundChange += OnRoundChange;
            gameLoop.OnGameEnd += OnGameEnd;
        }

        private void OnGameEnd() => audioEngine.Play(soundLibrary.MatchEnd);

        private void OnRoundChange(Round round, float[] coins)
        {
            audioEngine.Play(soundLibrary.StageComplete);
            roundCounter.SetText(gameLoop.RoundNumber.ToString());
            foreach (var _ in coins)
            {
                var coinUi = Instantiate(coinPrefab, coinParent);
                if (coinUi.TryGetComponent(out Button button))
                {
                    button.onClick.AddListener(() =>
                    {
                        audioEngine.Play(soundLibrary.CoinCollect);
                        clickEvent.Raise();
                        Destroy(coinUi.gameObject);
                    });
                }
            }
        }

        private void OnDestroy()
        {
            gameLoop.OnRoundChange -= OnRoundChange;
            gameLoop.OnGameEnd -= OnGameEnd;
        }

        private void Update()
        {
            timeLeft.SetText(gameLoop.TimeRemaining.ToString("F0"));
        }
    }
}
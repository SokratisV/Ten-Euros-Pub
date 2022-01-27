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
        [SerializeField] private CoinClickEvent clickEvent;
        [SerializeField] private AudioEngine audioEngine;
        [SerializeField] private SoundLibrary soundLibrary;
        [SerializeField] private CoinLibrary coinLibrary;

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
            foreach (Transform child in coinParent)
            {
                Destroy(child.gameObject);
            }

            audioEngine.Play(soundLibrary.StageComplete);
            roundCounter.SetText(gameLoop.RoundNumber.ToString());
            foreach (var coin in coins)
            {
                var coinUi = Instantiate(coinLibrary.GetCoinPrefabWithValue(coin), coinParent);
                if (coinUi.TryGetComponent(out Button button))
                {
                    button.onClick.AddListener(() =>
                    {
                        audioEngine.Play(soundLibrary.CoinCollect);
                        clickEvent.Raise();
                        ReplaceWithEmptyCoin(coinUi, coinLibrary, coinParent);
                    });
                }
            }
        }

        private static void ReplaceWithEmptyCoin(GameObject coinUi, CoinLibrary library, Transform parent)
        {
            var emptyCoin = Instantiate(library.GetCoinPrefabWithValue(0), parent);
            var index = coinUi.transform.GetSiblingIndex();
            Destroy(coinUi);
            emptyCoin.transform.SetSiblingIndex(index);
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
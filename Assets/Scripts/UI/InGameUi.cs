using System.Collections.Generic;
using RoboRyanTron.SceneReference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pub
{
    public class InGameUi : MonoBehaviour
    {
        [SerializeField] private RectTransform coinParent, beerParent;
        [SerializeField] private TextMeshProUGUI roundCounter;
        [SerializeField] private TextMeshProUGUI timeLeft;
        [SerializeField] private SceneReference menu;
        [SerializeField] private Button backButton;
        [SerializeField] private GameLoop gameLoop;
        [SerializeField] private CoinClickEvent clickEvent;
        [SerializeField] private AudioEngine audioEngine;
        [SerializeField] private CoinLibrary coinLibrary;
        [SerializeField] private MatchEndEvent matchEndEvent;
        [SerializeField] private GameData gameData;
        [SerializeField] private List<Sprite> beerSprites;

        private bool _hasClockAudioBeenTrigger;

        private void Awake()
        {
            backButton.onClick.AddListener(() =>
            {
                audioEngine.Play(audioEngine.Library.Back);
                menu.LoadScene();
            });
            gameLoop.OnRoundChange += OnRoundChange;
            matchEndEvent.OnMatchEnd += OnMatchEnd;
        }

        private void OnMatchEnd(int _) => audioEngine.Play(audioEngine.Library.MatchEnd);

        private void OnRoundChange(Round round, float[] coins)
        {
            _hasClockAudioBeenTrigger = false;
            audioEngine.Play(audioEngine.Library.StageComplete);
            roundCounter.SetText(gameLoop.RoundNumber.ToString());
            RemovePreviousCoins();
            var newCoinsArray = CreateArrayAndFillWithEmptyCoins(coins);
            gameData.Rng.Shuffle(newCoinsArray);
            CreateNewCoinInstances(newCoinsArray);
            SetupBeers();
        }

        private void SetupBeers()
        {
            foreach (Transform child in beerParent)
            {
                if (child.TryGetComponent(out Image image))
                {
                    var randomNumber = Random.Range(0, beerSprites.Count * 2);
                    var selectedSprite = randomNumber < beerSprites.Count
                        ? beerSprites[Random.Range(0, beerSprites.Count)]
                        : null;
                    if (selectedSprite == null) image.enabled = false;
                    else
                    {
                        image.enabled = true;
                        image.sprite = selectedSprite;
                    }
                }
            }
        }

        private void CreateNewCoinInstances(IEnumerable<float> coins)
        {
            foreach (var coin in coins)
            {
                var coinUi = Instantiate(coinLibrary.GetCoinPrefabWithValue(coin), coinParent);
                if (coinUi.TryGetComponent(out Button button))
                {
                    button.onClick.AddListener(() =>
                    {
                        var pitch = Random.Range(1, 11) * .01f + 2; //random value from 2 to 2.1 with a .1 steps
                        audioEngine.Play(audioEngine.Library.CoinCollect, pitch: pitch);
                        clickEvent.Raise();
                        ReplaceWithEmptyCoin(coinUi, coinLibrary, coinParent);
                    });
                }
            }
        }

        private void RemovePreviousCoins()
        {
            foreach (Transform child in coinParent)
            {
                Destroy(child.gameObject);
            }
        }

        private float[] CreateArrayAndFillWithEmptyCoins(IReadOnlyList<float> coins)
        {
            var newCoinsArray = new float[gameData.MaxNumberOfCoins];
            for (var i = 0; i < newCoinsArray.Length; i++)
            {
                if (i < coins.Count) newCoinsArray[i] = coins[i];
                else newCoinsArray[i] = 0;
            }

            return newCoinsArray;
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
            matchEndEvent.OnMatchEnd -= OnMatchEnd;
        }

        private void Update()
        {
            timeLeft.SetText(gameLoop.TimeRemaining.ToString("F0"));
            if (_hasClockAudioBeenTrigger == false && gameLoop.TimeRemaining <= gameData.TimeRunningOutAudioCue)
            {
                _hasClockAudioBeenTrigger = true;
                audioEngine.Play(audioEngine.Library.ClockTick);
            }
        }
    }
}
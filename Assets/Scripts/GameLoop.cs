using System;
using System.Collections.Generic;
using RoboRyanTron.SceneReference;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pub
{
    public class GameLoop : MonoBehaviour
    {
        public event Action<Round, float[]> OnRoundChange;
        [SerializeField] private GameData gameData;
        [SerializeField] private SceneReference menu;
        [SerializeField] private CoinClickEvent coinEvent;
        [SerializeField] private MatchEndEvent matchEndEvent;

        public int RoundNumber => _rounds.Count;
        public float TimeRemaining => _currentRound.TimeLeft;

        private List<Round> _rounds = new List<Round>();
        private Round _currentRound;
        private readonly float[] _coinValues = { .01f, .02f, .05f, .10f, .20f, .50f, 1f, 2f };

        private void Start()
        {
            _currentRound = GenerateNewRound(_rounds, CalculateRoundTimer());
            coinEvent.OnCoinClicked += CoinClicked;
        }

        private void OnDestroy()
        {
            coinEvent.OnCoinClicked -= CoinClicked;
            _currentRound.OnRoundEnded -= CheckForGameEnd;
        }

        private void Update()
        {
            if (TimeRemaining <= 0)
            {
                Debug.Log("Game Over!");
                matchEndEvent.Raise(RoundNumber);
                menu.LoadScene();
                return;
            }

            _currentRound?.Update();
        }

        private void CheckForGameEnd(float timeLeft)
        {
            if (timeLeft > 0)
                _currentRound = GenerateNewRound(_rounds, CalculateRoundTimer());
            else
                menu.LoadScene();
        }

        private Round GenerateNewRound(IList<Round> rounds, float initialTimer)
        {
            if (rounds.Count > 0) rounds[rounds.Count - 1].OnRoundEnded -= CheckForGameEnd;
            var coins = GenerateCoins(_coinValues, gameData.MaxNumberOfCoins);
            var round = new Round(initialTimer, coins.Length);
            rounds.Add(round);
            round.OnRoundEnded += CheckForGameEnd;
            OnRoundChange?.Invoke(round, coins);
            return round;
        }

        private float CalculateRoundTimer() => gameData.InitialRoundTimer - _rounds.Count;
        private void CoinClicked() => _currentRound?.CoinCollected();

        private static float[] GenerateCoins(IReadOnlyList<float> coinValues, int maxNumber)
        {
            var randomNumber = Random.Range(1, maxNumber);
            var coins = new float[randomNumber];
            for (var i = 0; i < coins.Length; i++)
            {
                coins[i] = coinValues[Random.Range(0, coinValues.Count)];
            }

            return coins;
        }
    }
}
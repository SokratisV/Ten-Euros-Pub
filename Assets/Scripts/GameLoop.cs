using System;
using System.Collections.Generic;
using RoboRyanTron.SceneReference;
using UnityEngine;

namespace Pub
{
    public class GameLoop : MonoBehaviour
    {
        public event Action<Round, float[]> OnRoundChange;
        [SerializeField] private GameData gameData;
        [SerializeField] private SceneReference menu;
        [SerializeField] private CoinClickEvent coinEvent;
        [SerializeField] private MatchEndEvent matchEndEvent;
        [SerializeField] private CoinGenerationBase coinGeneration;

        public int RoundNumber => _rounds.Count;
        public float TimeRemaining => _currentRound?.TimeLeft ?? 0;

        private List<Round> _rounds = new List<Round>();
        private Round _currentRound;

        private void Start()
        {
            _currentRound = GenerateNewRound(_rounds, CalculateRoundTimer());
            if (_currentRound == null)
            {
                menu.LoadScene();
                return;
            }

            coinEvent.OnCoinClicked += CoinClicked;
        }

        private void OnDestroy()
        {
            if (_currentRound == null) return;
            coinEvent.OnCoinClicked -= CoinClicked;
            _currentRound.OnRoundEnded -= CheckForGameEnd;
        }

        private void Update()
        {
            if (_currentRound == null) return;
            if (TimeRemaining <= 0)
            {
                Debug.Log("Game Over!");
                matchEndEvent.Raise(RoundNumber);
                menu.LoadScene();
                return;
            }

            _currentRound.Update();
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
            float[] coins = null;
            var counter = 0;
            while (coins == null)
            {
                counter++;
                if (counter > 10000) break;
                coins = coinGeneration.GenerateCoins(gameData.Rng);
            }

            if (coins == null) return null;

            Debug.Log($"took {counter} times");
            gameData.Rng.Shuffle(coins);
            var round = new Round(initialTimer, coins.Length);
            rounds.Add(round);
            round.OnRoundEnded += CheckForGameEnd;
            OnRoundChange?.Invoke(round, coins);
            return round;
        }

        private float CalculateRoundTimer() => gameData.InitialRoundTimer - _rounds.Count;
        private void CoinClicked() => _currentRound?.CoinCollected();
    }
}
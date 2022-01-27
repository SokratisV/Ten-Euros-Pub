using System;
using System.Collections.Generic;
using RoboRyanTron.SceneReference;
using UnityEngine;

namespace Pub
{
    public class GameLoop : MonoBehaviour
    {
        public event Action<Round> OnRoundChange;
        [SerializeField] private GameData gameData;
        [SerializeField] private SceneReference menu;

        public int RoundNumber => _rounds.Count;
        public float TimeRemaining => _currentRound.TimeLeft;

        private List<Round> _rounds = new List<Round>();
        private Round _currentRound;

        private void Awake()
        {
            _currentRound = GenerateNewRound(_rounds, CalculateRoundTimer());
            _currentRound.OnRoundEnded += CheckForGameEnd;
        }

        private void CheckForGameEnd(float timeLeft)
        {
            if (timeLeft > 0)
                _currentRound = GenerateNewRound(_rounds, CalculateRoundTimer());
            else
                menu.LoadScene();
        }

        private static Round GenerateNewRound(ICollection<Round> rounds, float initialTimer)
        {
            var round = new Round(initialTimer, 0);
            rounds.Add(round);
            return round;
        }

        private float CalculateRoundTimer() => gameData.InitialRoundTimer - _rounds.Count;

        private void Update() => _currentRound?.Update();
    }
}
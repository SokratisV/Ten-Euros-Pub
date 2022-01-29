using System;
using UnityEngine;

namespace Pub
{
    public class Round
    {
        public event Action<float> OnRoundEnded;
        public float TimeLeft => _internalTimer;

        private float _internalTimer;
        private int _coinsRequired;

        public Round(float timer, int coinsRequired)
        {
            _internalTimer = timer;
            _coinsRequired = coinsRequired;
        }

        public void Update()
        {
            if (_internalTimer <= 0)
            {
                _internalTimer = 0;
                OnRoundEnded?.Invoke(_internalTimer);
                return;
            }

            _internalTimer -= Time.deltaTime;
        }

        public void CoinCollected()
        {
            _coinsRequired--;
            if (_coinsRequired <= 0)
            {
                OnRoundEnded?.Invoke(_internalTimer);
            }
        }
    }
}
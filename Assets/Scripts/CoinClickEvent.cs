using System;
using UnityEngine;

namespace Pub
{
    [CreateAssetMenu(fileName = "Event - Coin Click", menuName = "Pub/Coin Clicked Event")]
    public class CoinClickEvent : ScriptableObject
    {
        public event Action OnCoinClicked;

        public void Raise() => OnCoinClicked?.Invoke();
    }
}
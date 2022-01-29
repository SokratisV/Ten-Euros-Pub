using System;
using UnityEngine;

namespace Pub
{
    [CreateAssetMenu(fileName = "Event - Match End", menuName = "Pub/Match End Event")]
    public class MatchEndEvent : ScriptableObject
    {
        public event Action<int> OnMatchEnd;
        public void Raise(int score) => OnMatchEnd?.Invoke(score);
    }
}
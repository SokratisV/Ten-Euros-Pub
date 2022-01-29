using UnityEngine;

namespace Pub
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Pub/New Game Data")]
    public class GameData : ScriptableObject
    {
        [Min(1)] public float InitialRoundTimer = 30;
        [Min(5)] public int MaxNumberOfCoins = 20;
        [Min(5)] public int TotalCoinValue = 10;
        [Min(0)] public int LeaderboardEntriesShown = 5;

        public System.Random Rng = new System.Random();
    }
}
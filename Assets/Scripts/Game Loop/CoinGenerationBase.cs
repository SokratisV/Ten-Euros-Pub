using UnityEngine;
using Random = System.Random;

namespace Pub
{
    /// <summary>
    /// Base class for coin generation algorithms. Idea is that we can create many of these and then
    /// drag-n-drop them (as scriptable) to change the algorithm that's being used.
    /// </summary>
    public abstract class CoinGenerationBase : ScriptableObject
    {
        [SerializeField] protected GameData gameData;
        protected readonly float[] CoinValues = { .01f, .02f, .05f, .10f, .20f, .50f, 1f, 2f };

        public abstract float[] GenerateCoins(Random rng);

        protected bool IsAmountOfCoinsCorrect(int amountOfCoins) => amountOfCoins <= gameData.MaxNumberOfCoins;
    }
}
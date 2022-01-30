using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pub
{
    public class CoinGeneration : CoinGenerationBase
    {
        [SerializeField] private List<int> upperLimitPerCoinValue;

        private Dictionary<float, int> _limitPerCoin = new Dictionary<float, int>();

        //Definitely not the best algorithm, but I can't think of anything else
        public override float[] GenerateCoins(System.Random rng)
        {
            var coins = new List<float>(20);
            var amountOfCoins = 0;
            var totalValue = 0f;
            InitializeDict();
            rng.Shuffle(CoinValues);
            foreach (var coinValue in CoinValues.Reverse())
            {
                var upperBound = _limitPerCoin[coinValue];
                var randomAmountOfCoins = Random.Range(1, upperBound);
                for (var i = 0; i < randomAmountOfCoins; i++)
                {
                    if (IsAmountOfCoinsCorrect(amountOfCoins + 1) == false)
                        return null;

                    if (totalValue + coinValue > gameData.TotalCoinValue)
                        break;

                    if (Mathf.FloorToInt(totalValue + coinValue) == gameData.TotalCoinValue)
                    {
                        coins.Add(coinValue);
                        return coins.ToArray();
                    }

                    amountOfCoins++;
                    totalValue += coinValue;
                    coins.Add(coinValue);
                }
            }

            return null;
        }

        private void InitializeDict()
        {
            if (_limitPerCoin.Count > 0) return;
            for (var i = 0; i < CoinValues.Length; i++)
            {
                var coinValue = CoinValues[i];
                _limitPerCoin.Add(coinValue, upperLimitPerCoinValue[i]);
            }
        }
    }
}
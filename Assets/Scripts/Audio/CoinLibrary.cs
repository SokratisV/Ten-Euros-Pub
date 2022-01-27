using System.Collections.Generic;
using UnityEngine;

namespace Pub
{
    [CreateAssetMenu(fileName = "Coin Library", menuName = "Pub/New Coin Library")]
    public class CoinLibrary : ScriptableObject
    {
        [SerializeField] private GameObject emptyCoin;
        [SerializeField] private GameObject OneCentCoin;
        [SerializeField] private GameObject TwoCentCoin;
        [SerializeField] private GameObject FiveCentCoin;
        [SerializeField] private GameObject TenCentCoin;
        [SerializeField] private GameObject TwentyCentCoin;
        [SerializeField] private GameObject FiftyCentCoin;
        [SerializeField] private GameObject OneEuroCoin;
        [SerializeField] private GameObject TwoEuroCoin;

        private Dictionary<float, GameObject> _valueToPrefab = new Dictionary<float, GameObject>(8);

        public GameObject GetCoinPrefabWithValue(float value)
        {
            if (_valueToPrefab.Count == 0) InitDictionary();
            return _valueToPrefab.TryGetValue(value, out var coin) ? coin : emptyCoin;
        }

        //Would be much better with a Serializable Dictionary, but oh well
        private void InitDictionary()
        {
            _valueToPrefab.Add(0.01f, OneCentCoin);
            _valueToPrefab.Add(0.02f, TwoCentCoin);
            _valueToPrefab.Add(0.05f, FiveCentCoin);
            _valueToPrefab.Add(0.1f, TenCentCoin);
            _valueToPrefab.Add(0.2f, TwentyCentCoin);
            _valueToPrefab.Add(0.5f, FiftyCentCoin);
            _valueToPrefab.Add(1f, OneEuroCoin);
            _valueToPrefab.Add(2f, TwoEuroCoin);
        }
    }
}
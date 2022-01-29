using UnityEngine;
using Random = UnityEngine.Random;

namespace Pub
{
    public class CoinPositionRandomizer : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;

        private void OnEnable()
        {
            var range = Random.Range(-10, 10);
            // rectTransform.offsetMax = new Vector2(range, range);
            // rectTransform.offsetMin = new Vector2(-range, -range);
            rectTransform.anchoredPosition = new Vector2(range, range);
        }
    }
}
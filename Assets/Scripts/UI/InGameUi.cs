using TMPro;
using UnityEngine;

namespace Pub
{
    public class InGameUi : MonoBehaviour
    {
        [SerializeField] private RectTransform beerParent;
        [SerializeField] private RectTransform coinParent;
        [SerializeField] private TextMeshProUGUI roundCounter;
        [SerializeField] private TextMeshProUGUI timeLeft;

        [SerializeField] private GameLoop gameLoop;

        private void Start()
        {
            gameLoop.OnRoundChange += AdjustUi;
        }

        private void Update()
        {
            timeLeft.SetText(gameLoop.TimeRemaining.ToString("F0"));
        }

        private void AdjustUi(Round round)
        {
            roundCounter.SetText(gameLoop.RoundNumber.ToString());
        }
    }
}
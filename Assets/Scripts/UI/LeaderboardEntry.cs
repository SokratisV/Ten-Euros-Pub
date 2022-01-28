using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pub
{
    public class LeaderboardEntry : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void SetValues(string playerName, int score, Sprite sprite)
        {
            nameText.SetText(playerName);
            scoreText.SetText(score.ToString());
            image.sprite = sprite;
        }
    }
}
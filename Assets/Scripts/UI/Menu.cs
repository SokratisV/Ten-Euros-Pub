using UnityEngine;
using UnityEngine.UI;

namespace Pub
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button play, leaderBoards, close;
        [SerializeField] private Transform leaderboardParent;
        [SerializeField] private GameObject menuButtonsPanel, leaderboardsPanel;

        private void Start()
        {
            leaderBoards.onClick.AddListener(() => { ToggleLeaderBoard(true); });
            close.onClick.AddListener(() => { ToggleLeaderBoard(false); });
        }

        private void ToggleLeaderBoard(bool toggle)
        {
            menuButtonsPanel.SetActive(!toggle);
            leaderboardsPanel.SetActive(toggle);
        }
    }
}
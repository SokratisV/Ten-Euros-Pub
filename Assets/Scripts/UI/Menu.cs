using RoboRyanTron.SceneReference;
using UnityEngine;
using UnityEngine.UI;

namespace Pub
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button play, leaderBoards, close;
        [SerializeField] private GameObject menuButtonsPanel, leaderboardPanel;
        [SerializeField] private AudioEngine audioEngine;
        [SerializeField] private SceneReference inGame;

        private void Start()
        {
            play.onClick.AddListener(() =>
            {
                audioEngine.Play(audioEngine.Library.Confirm);
                inGame.LoadScene();
            });
            leaderBoards.onClick.AddListener(() =>
            {
                ToggleLeaderBoard(true);
                audioEngine.Play(audioEngine.Library.Confirm);
            });
            close.onClick.AddListener(() =>
            {
                ToggleLeaderBoard(false);
                audioEngine.Play(audioEngine.Library.Back);
            });
        }

        private void ToggleLeaderBoard(bool toggle)
        {
            menuButtonsPanel.SetActive(!toggle);
            leaderboardPanel.SetActive(toggle);
        }
    }
}
using RoboRyanTron.SceneReference;
using UnityEngine;
using UnityEngine.UI;

namespace Pub
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private SoundLibrary library;
        [SerializeField] private Button play, leaderBoards, close;
        [SerializeField] private Transform leaderboardListParent;
        [SerializeField] private GameObject menuButtonsPanel, leaderboardPanel;
        [SerializeField] private AudioEngine audioEngine;
        [SerializeField] private SceneReference inGame;

        private void Start()
        {
            play.onClick.AddListener(() =>
            {
                audioEngine.Play(library.Confirm);
                inGame.LoadScene();
            });
            leaderBoards.onClick.AddListener(() =>
            {
                ToggleLeaderBoard(true);
                audioEngine.Play(library.Confirm);
            });
            close.onClick.AddListener(() =>
            {
                ToggleLeaderBoard(false);
                audioEngine.Play(library.Back);
            });
        }

        private void ToggleLeaderBoard(bool toggle)
        {
            menuButtonsPanel.SetActive(!toggle);
            leaderboardPanel.SetActive(toggle);
        }
    }
}
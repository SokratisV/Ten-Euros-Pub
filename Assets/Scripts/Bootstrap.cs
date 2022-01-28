using RoboRyanTron.SceneReference;
using UnityEngine;

namespace Pub
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private SceneReference menuScene;
        [SerializeField] private Camera cameraPrefab;
        [SerializeField] private Canvas background;
        [SerializeField] private AudioSource globalAudioSource;
        [SerializeField] private ScoreTracker scoreTracker;

        private void Start()
        {
            PlayerPrefs.SetString("PlayerName", "Player");
            DontDestroyOnLoad(gameObject);
            menuScene.LoadSceneAsync().completed += GenerateInitialData;
        }

        private void GenerateInitialData(AsyncOperation _)
        {
            scoreTracker.Init();
            DontDestroyOnLoad(Instantiate(cameraPrefab));
            DontDestroyOnLoad(Instantiate(background));
            DontDestroyOnLoad(Instantiate(globalAudioSource));
        }

        private void OnApplicationQuit() => scoreTracker.SaveScore();
    }
}
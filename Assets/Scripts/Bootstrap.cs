using RoboRyanTron.SceneReference;
using UnityEngine;

namespace Pub
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private SceneReference menuScene;
        [SerializeField] private Camera cameraPrefab;
        [SerializeField] private Canvas background;
        [SerializeField] private AudioSource globalAudioSourcePrefab;
        [SerializeField] private ScoreTracker scoreTracker;
        [SerializeField] private MuteAudioEvent muteAudio;

        private AudioSource _globalAudioSource;

        private void Start()
        {
            PlayerPrefs.SetString("PlayerName", "Player");
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            DontDestroyOnLoad(gameObject);
            menuScene.LoadSceneAsync().completed += GenerateInitialData;
            muteAudio.OnEventRaised += ToggleGlobalAudio;
        }

        private void GenerateInitialData(AsyncOperation _)
        {
            scoreTracker.Init();
            DontDestroyOnLoad(Instantiate(cameraPrefab));
            DontDestroyOnLoad(Instantiate(background));
            _globalAudioSource = Instantiate(globalAudioSourcePrefab);
            DontDestroyOnLoad(_globalAudioSource);
        }

        private void ToggleGlobalAudio(bool toggle)
        {
            if (toggle) _globalAudioSource.UnPause();
            else _globalAudioSource.Pause();
        }

        private void OnApplicationQuit() => scoreTracker.SaveScore();
    }
}
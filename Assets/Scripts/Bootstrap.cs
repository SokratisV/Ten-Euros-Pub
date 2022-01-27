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
        [SerializeField] private AudioEngine audioEngine;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            menuScene.LoadSceneAsync().completed += GenerateInitialData;
        }

        private void GenerateInitialData(AsyncOperation obj)
        {
            DontDestroyOnLoad(Instantiate(cameraPrefab));
            DontDestroyOnLoad(Instantiate(background));
            var audioSource = Instantiate(globalAudioSource);
            DontDestroyOnLoad(audioSource);
            audioEngine.AudioSource = audioSource;
        }
    }
}
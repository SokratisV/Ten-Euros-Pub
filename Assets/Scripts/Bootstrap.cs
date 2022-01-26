using RoboRyanTron.SceneReference;
using UnityEngine;

namespace Pub
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private SceneReference menuScene;
        [SerializeField] private Camera cameraPrefab;
        [SerializeField] private Canvas background;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            menuScene.LoadSceneAsync().completed += GenerateInitialData;
        }

        private void GenerateInitialData(AsyncOperation obj)
        {
            DontDestroyOnLoad(Instantiate(cameraPrefab));
            DontDestroyOnLoad(Instantiate(background));
        }
    }
}
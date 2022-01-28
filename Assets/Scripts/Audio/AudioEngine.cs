using UnityEngine;

namespace Pub
{
    [CreateAssetMenu(fileName = "Audio Engine", menuName = "Pub/New Audio Engine")]
    public class AudioEngine : ScriptableObject
    {
        [SerializeField] private SoundLibrary soundLibrary;

        private AudioSource _audioSource;

        private AudioSource AudioSource
        {
            get
            {
                if (_audioSource == null)
                {
                    var gameObject = new GameObject("AudioEngine");
                    DontDestroyOnLoad(gameObject);
                    _audioSource = gameObject.AddComponent<AudioSource>();
                }

                return _audioSource;
            }
        }

        public SoundLibrary Library => soundLibrary;

        public void Play(AudioClip clip, float volume = 1) => AudioSource.PlayOneShot(clip, volume);
    }
}
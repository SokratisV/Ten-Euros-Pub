using UnityEngine;

namespace Pub
{
    [CreateAssetMenu(fileName = "Audio Engine", menuName = "Pub/New Audio Engine")]
    public class AudioEngine : ScriptableObject
    {
        [SerializeField] private SoundLibrary soundLibrary;
        [SerializeField] private MuteAudioEvent audioEvent;

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
                    _audioSource.enabled = audioEvent.CurrentAudioState;
                    audioEvent.OnEventRaised += ToggleMute;
                }

                return _audioSource;
            }
        }

        public void ToggleMute(bool toggle) => AudioSource.enabled = toggle;

        public SoundLibrary Library => soundLibrary;

        public void Play(AudioClip clip, float volume = 1, float pitch = 1)
        {
            AudioSource.pitch = pitch;
            AudioSource.PlayOneShot(clip, volume);
        }
    }
}
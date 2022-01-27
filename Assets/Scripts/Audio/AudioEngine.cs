using UnityEngine;

namespace Pub
{
    [CreateAssetMenu(fileName = "Audio Engine", menuName = "Pub/New Audio Engine")]
    public class AudioEngine : ScriptableObject
    {
        public AudioSource AudioSource { get; set; }

        public void Play(AudioClip clip, float volume = 1) => AudioSource.PlayOneShot(clip, volume);
    }
}
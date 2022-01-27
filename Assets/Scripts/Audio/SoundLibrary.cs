using UnityEngine;

namespace Pub
{
    [CreateAssetMenu(fileName = "Sound Library", menuName = "Pub/New Sound Library")]
    public class SoundLibrary : ScriptableObject
    {
        public AudioClip Confirm;
        public AudioClip Back;
        public AudioClip Select;
    }
}
using System;
using UnityEngine;

namespace Pub
{
    [CreateAssetMenu(fileName = "Event - Audio Mute", menuName = "Pub/Audio Mute Event")]
    public class MuteAudioEvent : ScriptableObject
    {
        public event Action<bool> OnEventRaised;
        public bool CurrentAudioState => _isAudioEnabled;

        private bool _isAudioEnabled = true;

        public void Raise()
        {
            _isAudioEnabled = !_isAudioEnabled;
            OnEventRaised?.Invoke(_isAudioEnabled);
        }
    }
}
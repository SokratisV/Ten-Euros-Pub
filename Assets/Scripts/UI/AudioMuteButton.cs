using UnityEngine;
using UnityEngine.UI;

namespace Pub
{
    public class AudioMuteButton : MonoBehaviour
    {
        [SerializeField] private MuteAudioEvent muteEvent;
        [SerializeField] private Button button;

        private void OnEnable() => button.onClick.AddListener(muteEvent.Raise);
    }
}
using CardGuess.Controllers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardGuess.View
{
    [RequireComponent(typeof(Slider))]
    public class SoundBar : MonoBehaviour
    {
        private Slider _slider;

        private IAudioVolumeController _audioVolumeController;

        [Inject]
        private void Construct(IAudioVolumeController audioVolumeController)
        {
            _audioVolumeController = audioVolumeController;
        }

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void Start()
        {
            _slider.value = _audioVolumeController.CurrentVolume;
            _slider.onValueChanged.AddListener(UpdateSlider);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(UpdateSlider);
        }

        private void UpdateSlider(float value)
        {
            _audioVolumeController.SetVolume(value);
        }
    }   
}
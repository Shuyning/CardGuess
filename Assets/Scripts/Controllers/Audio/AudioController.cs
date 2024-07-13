using System.Collections.Generic;
using CardGuess.Installers;
using CardGuess.Models;
using UnityEngine;
using Zenject;

namespace CardGuess.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour, IAudioPlayer, IAudioVolumeController
    {
        private const string SoundVolumeKey = nameof(SoundVolumeKey);

        [SerializeField, Range(0f, 1f)] private float defaultVolume = 0.5f;
        
        private Dictionary<SoundType, SoundData> _soundDictionary;
        private AudioSource _audioSource;

        private IDataSaver _dataSaver;

        public float CurrentVolume => _audioSource.volume;

        [Inject]
        private void Construct(SoundStorage soundStorage, IDataSaver dataSaver)
        {
            _soundDictionary = soundStorage.GetSoundDataDictionary();
            _dataSaver = dataSaver;
        }

        private void Awake()
        {
            Init();
        }

        public void PlayClip(SoundType type)
        {
            if (!_soundDictionary.TryGetValue(type, out SoundData soundData))
                return;

            _audioSource.PlayOneShot(soundData.SoundClip, soundData.Volume * CurrentVolume);
        }
        
        public void SetVolume(float value)
        {
            _audioSource.volume = Mathf.Clamp(value, 0f, 1f);
            _dataSaver.SaveData(new SaveAudioData() { AudioVolume = CurrentVolume }, SoundVolumeKey);
        }

        private void Init()
        {
            _audioSource = GetComponent<AudioSource>();
            LoadVolume();
        }

        private void LoadVolume()
        {
            SaveAudioData saveAudioData = _dataSaver.LoadData<SaveAudioData>(SoundVolumeKey);

            if (saveAudioData == null)
            {
                SetVolume(defaultVolume);
                return;
            }
            
            _audioSource.volume = saveAudioData.AudioVolume;
        }
    }
}
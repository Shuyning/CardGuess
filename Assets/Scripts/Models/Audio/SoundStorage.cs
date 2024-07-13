using System.Collections.Generic;
using CardGuess.Models;
using UnityEngine;

namespace CardGuess.Installers
{
    [CreateAssetMenu(fileName = "SoundStorage", menuName = "ScriptableObject/SoundStorage")]
    public class SoundStorage : ScriptableObject
    {
        [SerializeField] private List<SoundData> soundDataList;
        
        public Dictionary<SoundType, SoundData> GetSoundDataDictionary()
        {
            Dictionary<SoundType, SoundData> soundDataDictionary = new Dictionary<SoundType, SoundData>();
            foreach (var soundData in  soundDataList)
            {
                if (soundDataDictionary.ContainsKey(soundData.SoundType)) continue;

                soundDataDictionary.Add(soundData.SoundType, soundData);
            }

            return soundDataDictionary;
        } 
    }   
}
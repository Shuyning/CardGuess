using System;
using UnityEngine;

namespace CardGuess.Models
{
    [Serializable]
    public class SoundData
    {
        [SerializeField] private SoundType soundType;
        [SerializeField, Range(0f, 1f)] private float volume = 1f;
        [Space]
        [SerializeField] private AudioClip soundClip;

        public SoundType SoundType => soundType;
        public float Volume => volume;
        public AudioClip SoundClip => soundClip;

    }
}
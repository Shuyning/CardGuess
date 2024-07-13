using UnityEngine;

namespace CardGuess.Models
{
    [CreateAssetMenu(fileName = "TimerData", menuName = "ScriptableObject/TimerData")]
    public class TimerData : ScriptableObject
    {
        [field: SerializeField, Range(0, 1000)] public int TimerDuration { get; private set; } = 180;
        [field: SerializeField] public float TimeBetween { get; private set; } = 1f;
    }
}
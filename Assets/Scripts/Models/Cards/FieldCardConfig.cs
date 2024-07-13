using UnityEngine;

namespace CardGuess.Models
{
    [CreateAssetMenu(fileName = "FieldCardConfig", menuName = "ScriptableObject/FieldCardConfig")]
    public class FieldCardConfig : ScriptableObject
    {
        [field: SerializeField, Range(0.1f, 100f)] public float FirstShowTime { get; private set; } = 2f;
        [field: SerializeField, Range(0.1f, 100f)] public float OpenShowTime { get; private set; } = 1f;
    }   
}
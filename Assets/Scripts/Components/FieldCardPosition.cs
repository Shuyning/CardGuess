using UnityEngine;

namespace CardGuess.Components
{
    public class FieldCardPosition : MonoBehaviour, ICardPositionGetter
    {
        public Transform Transform => transform;
    }   
}
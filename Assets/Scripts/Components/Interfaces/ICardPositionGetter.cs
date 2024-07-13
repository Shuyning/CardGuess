using UnityEngine;

namespace CardGuess.Components
{
    public interface ICardPositionGetter
    {
        public Transform Transform { get; }
    }
}